using System;
using System.Collections.Generic;
using R3;
using TMPro;
using TowerLab.Map;
using UnityEngine;
using UnityEngine.UI;

public class MapScreenView : View<IMapScreenViewModel>
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private SettingsScreenView settingsScreenView;
    [SerializeField] private MapView mapView;
    [SerializeField] private TextMeshProUGUI helperText;
    [SerializeField] private GameObject legendContainer;
    [SerializeField] private LegendItemView legendItemPrefab;

    [NonSerialized] private IMapScreenViewModel _viewModel;
    private List<MapNodeView> MapNodeViews => mapView.NodeViews;

    public override void Init(IMapScreenViewModel viewModel)
    {
        _viewModel = viewModel;
        settingsButton.onClick.AddListener(OpenSettingsMenu);
        _viewModel.PlayerState.Subscribe(OnPlayerStateChanged);
        //_viewModel.LastFinishedLevelId.Subscribe(UpdatePathsAnimation);

        InitView();
    }

    public void Select(int selected)
    {
        foreach (var node in MapNodeViews)
            node.Select(node.FloorId == selected || _viewModel.FinishedLevels.Contains(node.FloorId));
        //UpdatePathsAnimation(_viewModel.LastFinishedLevelId.Value);
    }


    private void InitView()
    {
        mapView.Init(_viewModel);

        settingsScreenView.Init(_viewModel.SettingsScreenViewModel);
        InitLegend();
    }

    private void InitLegend()
    {
        foreach (var legendItem in _viewModel.MapData.RoomData)
        {
            var newLegendItem = Instantiate(legendItemPrefab, legendContainer.transform);
            newLegendItem.Init(legendItem.Legend, legendItem.Name);
        }
    }

    private void OpenSettingsMenu()
    {
        settingsScreenView.SetActive(true);
        _viewModel.OpenSettingsMenu();
    }

    private void OnPlayerStateChanged(State state)
    {
        helperText.text = state.HelperText;
    }

    public override void Dispose()
    {
        settingsButton?.onClick.RemoveAllListeners();

        for (var i = 0; i < MapNodeViews.Count; i++) MapNodeViews[i]?.Dispose();
    }
}