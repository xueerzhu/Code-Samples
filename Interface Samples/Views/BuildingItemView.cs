using System;
using PrimeTween;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class BuildingItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;

    [SerializeField] private Button button;
    [SerializeField] private Image selectedOutlineImage;

    [Title("Hover Parameters")] [SerializeField]
    private bool scaleAnimations = true;

    [SerializeField] private float scaleOnHover = 1.15f;
    [SerializeField] private float scaleTransition = .15f;
    [SerializeField] private float moveDistance = 50f;
    [NonSerialized] private GameView _gameView;
    [NonSerialized] private bool _initialized;
    [NonSerialized] private Vector3 _initialLocalPosition;

    [NonSerialized] private IBuildingItemViewModel _viewModel;
    [NonSerialized] private BuildingItemVisualHandler _visualHandler;

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_initialized)
        {
            _initialLocalPosition = transform.localPosition;
            _initialized = true;
        }

        if (scaleAnimations)
        {
            Tween.Scale(transform, scaleOnHover, scaleTransition, Ease.OutBack);
            if (_gameView.UseVerticalBuildingBar)
                Tween.LocalPosition(transform, _initialLocalPosition + new Vector3(moveDistance, 0, 0), scaleTransition,
                    Ease.OutBack);
            else
                Tween.LocalPosition(transform, _initialLocalPosition + new Vector3(0, moveDistance, 0), scaleTransition,
                    Ease.OutBack);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tween.Scale(transform, 1, scaleTransition, Ease.OutBack);
        Tween.LocalPosition(transform, _initialLocalPosition, scaleTransition, Ease.OutBack);
    }

    public void Bind(IBuildingItemViewModel viewModel, GameView gameView)
    {
        _viewModel = viewModel;
        _gameView = gameView;
        button.onClick.AddListener(_viewModel.BuildingCardClicked);
        _viewModel.IsSelected.Subscribe(SetActiveSelectedOutline);
        InitView();
    }

    /*public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        PointerDownEvent.Invoke(this);
        pointerDownTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        pointerUpTime = Time.time;

        PointerUpEvent.Invoke(this, pointerUpTime - pointerDownTime > .2f);

        if (pointerUpTime - pointerDownTime > .2f)
            return;

        if (wasDragged)
            return;

        selected = !selected;
        SelectEvent.Invoke(this, selected);

        if (selected)
            transform.localPosition += (cardVisual.transform.up * selectionOffset);
        else
            transform.localPosition = Vector3.zero;
    }*/

    private void InitView()
    {
        icon.sprite = _viewModel.Icon;
        selectedOutlineImage.color = _viewModel.SelectedOutlineColor;
    }

    private void SetActiveSelectedOutline(bool enable)
    {
        selectedOutlineImage.gameObject.SetActive(enable);
    }
}