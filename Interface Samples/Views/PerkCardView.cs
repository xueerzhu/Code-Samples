using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkCardView : View<IPerkCardViewModel>
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Button button;

    [NonSerialized] private IPerkCardViewModel _viewModel;

    private void Refresh()
    {
        icon.sprite = _viewModel.Icon;
        title.text = _viewModel.Name;
        description.text = _viewModel.Description;
    }

    public override void Init(IPerkCardViewModel viewModel)
    {
        _viewModel = viewModel;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(_viewModel.PerkCardClicked);
        Refresh();
    }

    public override void Dispose()
    {
    }
}