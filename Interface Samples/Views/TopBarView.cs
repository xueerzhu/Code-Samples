using System;
using System.Collections.Generic;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBarView : View<ITopBarViewModel>
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI matterText;
    [SerializeField] private SettingsScreenView settingsScreen;
    [SerializeField] private List<GameObject> helpers;
    [NonSerialized] private bool _showingHelpers;

    [NonSerialized] private ITopBarViewModel _viewModel;

    public override void Init(ITopBarViewModel viewModel)
    {
        _viewModel = viewModel;

        InitView();
        Bind();
    }

    private void InitView()
    {
        healthText.text = $"{_viewModel.HealthAmount.Value} / {_viewModel.HealthTotalAmount.Value}";
        settingsScreen.Init(_viewModel.SettingsScreenViewModel);
        ToggleHelpersVisibility(false);
    }

    private void Bind()
    {
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        helpButton.onClick.AddListener(OnHelpButtonClick);

        _viewModel.HealthAmount.Subscribe(x => OnHealthChange(x));
        _viewModel.HealthTotalAmount.Subscribe(x => OnHealthTotalChange(x));
        _viewModel.MatterAmount.Subscribe(x => OnMatterChange(x));
    }

    private void OnSettingsButtonClick()
    {
        settingsScreen.SetActive(true);
    }

    private void OnHelpButtonClick()
    {
        _showingHelpers = !_showingHelpers;
        ToggleHelpersVisibility(_showingHelpers);
    }

    private void ToggleHelpersVisibility(bool show)
    {
        foreach (var helper in helpers) helper.SetActive(show);
    }


    private void OnHealthChange(int newValue)
    {
        healthText.text = $"{newValue} / {_viewModel.HealthTotalAmount.Value}";
    }

    private void OnHealthTotalChange(int newValue)
    {
        healthText.text = $"{_viewModel.HealthAmount.Value} / {newValue}";
    }

    private void OnMatterChange(int newValue)
    {
        matterText.text = newValue.ToString();
    }

    public override void Dispose()
    {
        settingsButton?.onClick.RemoveAllListeners();
    }
}