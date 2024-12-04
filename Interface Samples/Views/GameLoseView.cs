using UnityEngine;
using UnityEngine.UI;

public class GameLoseView : View<IGameLoseViewModel>
{
    [SerializeField] private Button titleButton;

    public override void Init(IGameLoseViewModel viewModel)
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