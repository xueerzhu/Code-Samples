using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWishlistView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Button entryButton;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Color fillColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private Image background;
    [SerializeField] private Image platformLogo;

    [Title("Hover Parameters")] [SerializeField]
    private bool scaleAnimations = true;

    [SerializeField] private float scaleOnHover = 1.15f;
    [SerializeField] private float scaleTransition = .15f;
    [SerializeField] private float moveDistance = 50f;

    [NonSerialized] private bool _initialized;
    [NonSerialized] private Vector3 _initialLocalPosition;
    [NonSerialized] private ISupportPageViewModel _viewModel;

    private void OnDestroy()
    {
        entryButton?.onClick.RemoveListener(OnWishlistButtonClick);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        background.color = fillColor;
        label.color = Color.black;
        platformLogo.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        background.color = normalColor;
        label.color = Color.white;
        platformLogo.color = Color.white;
    }

    public void Init(ISupportPageViewModel viewModel)
    {
        _viewModel = viewModel;
        entryButton.onClick.AddListener(OnWishlistButtonClick);
    }

    private void OnWishlistButtonClick()
    {
        _viewModel.WishlistGame();
    }
}