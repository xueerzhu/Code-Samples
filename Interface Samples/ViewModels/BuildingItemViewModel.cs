using System;
using System.Collections.Specialized;
using ObservableCollections;
using R3;
using UnityEngine;

public class BuildingItemViewModel : IBuildingItemViewModel
{
    [NonSerialized] private readonly IBuildingController _buildingController;
    [NonSerialized] private readonly IDisposable _disposable;
    [NonSerialized] private readonly HandWidgetViewModel _handWidgetViewModel;

    public BuildingItemViewModel(IBuildingController buildingController, IBuildingData buildingData,
        HandWidgetViewModel handWidgetViewModel, Card card)
    {
        BuildingData = buildingData;
        _buildingController = buildingController;
        _disposable = _buildingController.SelectedBuildingName.Subscribe(x => Select(x == BuildingData.Name));
        _handWidgetViewModel = handWidgetViewModel;
        Card = card;

        Bind();
    }

    public event Action CardCollectionChanged;
    public event Action MarkForDestroy;
    public Card Card { get; }

    public ReactiveProperty<bool> IsSelected { get; } = new();

    public Color SelectedOutlineColor => Color.white;

    public IBuildingData BuildingData { get; }

    public Sprite Icon => BuildingData.Icon;
    public string Name => BuildingData.Name;

    public void BuildingCardClicked()
    {
        _buildingController.SelectBuildingWithName(BuildingData.Name);
        _handWidgetViewModel.SelectCard(Card);
    }

    private void Select(bool isSelected)
    {
        IsSelected.Value = isSelected;
    }

    private void Bind()
    {
        _handWidgetViewModel.CardViewModels.CollectionChanged += FireHandCollectionChanged;
    }

    private void FireHandCollectionChanged(in NotifyCollectionChangedEventArgs<IBuildingItemViewModel> e)
    {
        CardCollectionChanged?.Invoke();
        if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            if (e.IsSingleItem)
            {
                IBuildingItemViewModel item;
                item = e.OldItem;
                if (item == this) OnMarkForDestroy();
            }
            else
            {
                foreach (var item in e.OldItems)
                    if (item == this)
                        OnMarkForDestroy();
            }
        }
    }

    private void OnMarkForDestroy()
    {
        MarkForDestroy?.Invoke();
        _disposable.Dispose();
    }
}