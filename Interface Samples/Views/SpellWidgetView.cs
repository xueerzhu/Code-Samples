using System;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpellWidgetView : MonoBehaviour
{
    [FormerlySerializedAs("aimButton")] [SerializeField]
    private Button activateButton;

    [SerializeField] private Image spellBackground;

    [NonSerialized] private ISpellWidgetViewModel _viewModel;

    private void OnDestroy()
    {
        if (activateButton != null) activateButton.onClick.RemoveAllListeners();
    }

    public void Init(ISpellWidgetViewModel viewModel)
    {
        _viewModel = viewModel;
        if (activateButton != null) activateButton.onClick.AddListener(OnActivateButtonClicked);
        _viewModel.IsActivated.Subscribe(x => SetSpellActivateStatus(x));
    }


    private void OnActivateButtonClicked()
    {
        _viewModel.OnActivateButtonClicked();
    }


    private void SetSpellActivateStatus(bool isActivated)
    {
        spellBackground.enabled = !isActivated;
    }
}