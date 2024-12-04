using UnityEngine;
using UnityEngine.UI;

public class SupportPageView : View<ISupportPageViewModel>
{
    [SerializeField] private Button noButton;
    [SerializeField] private ButtonWishlistView wishlistButton;
    [SerializeField] private GameObject supportWidget;

    public override void Init(ISupportPageViewModel viewModel)
    {
        ViewModel = viewModel;
        wishlistButton.Init(viewModel);
        noButton.onClick.AddListener(OnNoButtonClick);
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        supportWidget.SetActive(active);
    }

    private void OnNoButtonClick()
    {
        ViewModel.QuitToDesktop();
    }

    public override void Dispose()
    {
        noButton?.onClick.RemoveListener(OnNoButtonClick);
    }
}