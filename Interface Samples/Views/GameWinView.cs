using UnityEngine;
using UnityEngine.UI;

public class GameWinView : View<IGameWinViewModel>
{
    [SerializeField] private Button titleButton;

    public override void Init(IGameWinViewModel viewModel)
    {
        titleButton.onClick.AddListener(() => viewModel.BackToTitle());
    }

    public override void Dispose()
    {
        titleButton?.onClick.RemoveAllListeners();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}