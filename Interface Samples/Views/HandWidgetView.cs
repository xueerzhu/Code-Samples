using System;
using ObservableCollections;
using R3;
using UnityEngine;

public class HandWidgetView : View<IHandWidgetViewModel>
{
    [SerializeField] private GameObject container;
    [SerializeField] private CardView cardPrefab;

    [NonSerialized] private IHandWidgetViewModel _viewModel;

    private void AddCardView(IBuildingItemViewModel buildingItemViewModel)
    {
        var cardView = Instantiate(cardPrefab, container.transform);
        cardView.Bind(buildingItemViewModel);
    }

    public override void Init(IHandWidgetViewModel viewModel)
    {
        _viewModel = viewModel;
        _viewModel.CardViewModels.ObserveAdd()
            .Subscribe(x => { AddCardView(x.Value); });
        _viewModel.CardViewModels.ObserveRemove()
            .Subscribe(x =>
            {
                var cardViewModel = x.Value;
                var cardView = container.GetComponentInChildren<CardView>(true);
                Destroy(cardView.gameObject);
            });
        _viewModel.CardViewModels.ObserveReset()
            .Subscribe(x =>
            {
                foreach (Transform child in container.transform) Destroy(child.gameObject);
            });

        foreach (var cardViewModel in _viewModel.CardViewModels) AddCardView(cardViewModel);
    }

    public override void Dispose()
    {
    }
}