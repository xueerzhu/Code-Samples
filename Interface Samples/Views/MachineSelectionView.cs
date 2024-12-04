using System;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;

public class MachineSelectionView : View<IMachineSelectViewModel>
{
    [SerializeField] private ButtonManager continueButton;
    [SerializeField] private ButtonManager settingsButton;
    [SerializeField] private ButtonManager backButton;
    [SerializeField] private WindowManager selectWindow;
    [SerializeField] private SettingsScreenView settingsScreenView;

    [NonSerialized] private IMachineSelectViewModel _viewModel;

    public override void Init(IMachineSelectViewModel viewModel)
    {
        _viewModel = viewModel;
        
        AddListeners();
        settingsScreenView.Init(_viewModel.SettingsScreenViewModel);
    }
    
    private void AddListeners()
    {
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        settingsButton.onClick.AddListener(OpenSettingsMenu);
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnContinueButtonClicked()
    {
        _viewModel.Continue();
    }

    private void OpenSettingsMenu()
    {
        settingsScreenView.SetActive(true);
    }
    
    private void OnBackButtonClicked()
    {
        _viewModel.Back();
    }

    public override void Dispose()
    {
        continueButton?.onClick.RemoveListener(OnContinueButtonClicked);
        settingsButton?.onClick.RemoveListener(OpenSettingsMenu);
        backButton?.onClick.RemoveListener(OnBackButtonClicked);
    }
}