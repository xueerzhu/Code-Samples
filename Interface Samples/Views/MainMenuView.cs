using System;
using Michsky.MUIP;
using UnityEngine;

public class MainMenuView : View<IMainMenuViewModel>
{
    [SerializeField] private ButtonManager newGameButton;
    [SerializeField] private ButtonManager settingsButton;
    [SerializeField] private ButtonManager quitButton;
    [SerializeField] private ButtonManager wishlistButton;
    [SerializeField] private ModalWindowManager quitConfirmationModal;
    [SerializeField] private SettingsScreenView settingsScreen;

    [NonSerialized] private IMainMenuViewModel _viewModel;

    public override void Init(IMainMenuViewModel viewModel)
    {
        _viewModel = viewModel;
        settingsScreen.Init(_viewModel.SettingsScreenViewModel);
        AddListeners();
    }
    
    private void AddListeners()
    {
        newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        wishlistButton.onClick.AddListener(OnWishlistButtonClick);
        
        quitConfirmationModal.onConfirm.AddListener(OnQuitConfirmed);
        quitConfirmationModal.onCancel.AddListener(OnQuitCancelled);
    }


    private void OnNewGameButtonClicked()
    {
        _viewModel.StartGame();
    }

    private void OnSettingsButtonClicked()
    {
        settingsScreen.SetActive(true);
    }

    private void OnQuitButtonClicked()
    {
       quitConfirmationModal.Open();
    }
    
    private void OnQuitConfirmed()
    {
        _viewModel.QuitGame();
    }
    
    private void OnQuitCancelled()
    {
        quitConfirmationModal.Close();
    }
    
    private void OnWishlistButtonClick()
    {
        _viewModel.WishlistGame();
    }

    public override void Dispose()
    {
        newGameButton?.onClick.RemoveAllListeners();
        settingsButton?.onClick.RemoveAllListeners();
        quitButton?.onClick.RemoveAllListeners();
        wishlistButton?.onClick.RemoveAllListeners();
    }
}