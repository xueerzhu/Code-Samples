using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSelectionSliderView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI optionText;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [NonSerialized] private string[] _options;

    [NonSerialized] private int _selectedOption;
    [NonSerialized] private ISettingsScreenViewModel _viewModel;
    [NonSerialized] public Action<string> OnSelectionChanged;

    public void Init(ISettingsScreenViewModel viewModel)
    {
        _viewModel = viewModel;
        prevButton.onClick.AddListener(OnPrevButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
        _options = _viewModel.LanguageOptions;

        InitView();
    }

    public void OnPrevButtonClicked()
    {
        _selectedOption--;
        if (_selectedOption < 0) _selectedOption = _options.Length - 1;

        ChangeSelection();
    }

    public void OnNextButtonClicked()
    {
        _selectedOption = (_selectedOption + 1) % _options.Length;

        ChangeSelection();
    }

    private void ChangeSelection()
    {
        optionText.text = _options[_selectedOption];
        OnSelectionChanged?.Invoke(_options[_selectedOption]);
    }

    private void InitView()
    {
        ChangeSelection();
    }
}