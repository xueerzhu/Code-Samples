using System;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckWidgetView : MonoBehaviour
{
    [Required] [SerializeField] private Button drawButton;
    [Required] [SerializeField] private DeckView deckView;

    [NonSerialized] private IDeckViewModel _viewModel;

    private void OnDestroy()
    {
        drawButton.onClick.RemoveAllListeners();
    }

    public void Init(IDeckViewModel viewModel, GameView gameView)
    {
        _viewModel = viewModel;
        drawButton.onClick.AddListener(OnDraw);
        _viewModel.CanDraw.Subscribe(x => SetActiveDrawButton(x));
        InitView();
    }

    public void OnDrawPointerEnter(PointerEventData eventData)
    {
    }

    public void OnDrawPointerExit(PointerEventData eventData)
    {
    }

    private void InitView()
    {
        deckView.Init(this);
    }

    private void OnDraw()
    {
        _viewModel.OnDraw();
    }

    private void SetActiveDrawButton(bool canDraw)
    {
        drawButton.interactable = canDraw;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }
}