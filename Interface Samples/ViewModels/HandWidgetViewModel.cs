using System;
using System.Collections.Specialized;
using System.Linq;
using ObservableCollections;

public interface IHandWidgetViewModel : IViewModel
{
    ObservableList<IBuildingItemViewModel> CardViewModels { get; }
}

public class HandWidgetViewModel : IHandWidgetViewModel
{
    [NonSerialized] private readonly IHandController _handController;

    public HandWidgetViewModel(IHandController handController)
    {
        _handController = handController;
        _handController.CardsInHand.CollectionChanged += OnCardsChanged;

        Init();
    }

    public ObservableList<IBuildingItemViewModel> CardViewModels { get; } = new();

    public void Dispose()
    {
    }

    void IViewModel.Init()
    {
        Init();
    }

    public void SelectCard(Card card)
    {
        _handController.SelectCard(card);
    }

    private void OnCardsChanged(in NotifyCollectionChangedEventArgs<Card> e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.IsSingleItem)
                    CardViewModels.Add(CreateBuildingViewModelFromCard(e.NewItem));
                else
                    foreach (var item in e.NewItems)
                        CardViewModels.Add(CreateBuildingViewModelFromCard(item));
                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.IsSingleItem)
                {
                    Card item;
                    item = e.OldItem;
                    CardViewModels.Remove(CardViewModels.First(t => t.Card == item));
                }
                else
                {
                    foreach (var item in e.OldItems)
                        CardViewModels.Remove(CardViewModels.First(t => t.Card == item));
                }

                break;
            case NotifyCollectionChangedAction.Reset:
                CardViewModels.Clear();
                break;
        }
    }

    private BuildingItemViewModel CreateBuildingViewModelFromCard(Card card)
    {
        var vm = new BuildingItemViewModel(
            _handController.BuildingController, card.BuildingData, this, card);

        return vm;
    }

    private void Init()
    {
        foreach (var card in _handController.CardsInHand) CardViewModels.Add(CreateBuildingViewModelFromCard(card));
    }
}