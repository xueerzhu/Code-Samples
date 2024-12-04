using System;
using Cysharp.Threading.Tasks;
using PrimeTween;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Image selected;
    [SerializeField] private Button button;

    [Title("Hover Parameters")] [SerializeField]
    private float scaleOnHover = 1.15f;

    [SerializeField] private float scaleTransition = .15f;
    [SerializeField] private float moveDistance = 50f;

    [NonSerialized] private bool _initializedFlag;
    [NonSerialized] private Vector3 _initialLocalPosition;
    [NonSerialized] private bool _isCardInAnimation;
    [NonSerialized] private IBuildingItemViewModel _viewModel;

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
        _viewModel.CardCollectionChanged -= OnCardsCollectionChange;
        _viewModel.MarkForDestroy -= OnMarkDestroy;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!UIStateManager.Instance.HandWidgetGroupUIState) return;
        OnPointerEnterAnimation().Forget();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!UIStateManager.Instance.HandWidgetGroupUIState) return;
        OnPointerExitAnimation().Forget();
    }

    public void Bind(IBuildingItemViewModel buildingItemViewModel)
    {
        _viewModel = buildingItemViewModel;
        _viewModel.CardCollectionChanged += OnCardsCollectionChange;
        _viewModel.MarkForDestroy += OnMarkDestroy;
        _viewModel.IsSelected.Subscribe(x => selected.gameObject.SetActive(x));
        InitView();
    }

    private void OnMarkDestroy()
    {
        Destroy(gameObject);
    }

    private async UniTask OnPointerEnterAnimation()
    {
        if (!_initializedFlag)
        {
            _initialLocalPosition = transform.localPosition;
            _initializedFlag = true;
        }

        if (_isCardInAnimation) return;
        try
        {
            _isCardInAnimation = true;
            Tween.Scale(transform, scaleOnHover, scaleTransition);
            Tween.LocalPosition(transform, _initialLocalPosition + new Vector3(0, moveDistance, 0), scaleTransition);
        }
        finally
        {
            _isCardInAnimation = false;
        }
    }

    private async UniTask OnPointerExitAnimation()
    {
        if (_isCardInAnimation) return;
        try
        {
            _isCardInAnimation = true;
            Tween.Scale(transform, 1, scaleTransition);
            Tween.LocalPosition(transform, _initialLocalPosition, scaleTransition);
        }
        finally
        {
            _isCardInAnimation = false;
        }
    }

    private void InitView()
    {
        icon.sprite = _viewModel.Icon;
        button.onClick.AddListener(_viewModel.BuildingCardClicked);
        selected.gameObject.SetActive(_viewModel.IsSelected.Value);
    }

    private void OnCardsCollectionChange()
    {
        // set layout to dirty
        _initializedFlag = false;
    }
}