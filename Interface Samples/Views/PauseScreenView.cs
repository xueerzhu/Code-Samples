using System;
using UnityEngine;

public class PauseScreenView : MonoBehaviour
{
    [SerializeField] private GenericButtonView continueButton;
    [SerializeField] private GenericButtonView titleButton;

    [NonSerialized] private IPauseScreenViewModel _viewModel;
    public bool IsActive => gameObject.activeSelf;


    private void OnDestroy()
    {
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Bind(IPauseScreenViewModel viewModel, GameView gameView)
    {
        _viewModel = viewModel;
        continueButton.Bind(OnContinue);
        titleButton.Bind(OnBackToTitle);
        InitView();
    }

    private void InitView()
    {
        SetActive(false);
    }

    private void OnContinue()
    {
        _viewModel.ContinueGame();
    }

    private void OnBackToTitle()
    {
        _viewModel.BackToTitle();
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}