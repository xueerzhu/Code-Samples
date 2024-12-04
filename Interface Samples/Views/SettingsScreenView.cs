using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreenView : View<ISettingsScreenViewModel>
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextSelectionSliderView languageSelectionSlider;
    [SerializeField] private SliderManager sfxSlider;

    public override void Init(ISettingsScreenViewModel viewModel)
    {
        ViewModel = viewModel;
        closeButton.onClick.AddListener(OnCloseButtonClick);
        languageSelectionSlider.Init(ViewModel);
        languageSelectionSlider.OnSelectionChanged += OnLanguageSelectionChanged;

        // Load previously saved volume level or set default value
        var savedVolume = PlayerPrefs.GetFloat("SFXVolume", 75f); // Default at 75%
        sfxSlider.mainSlider.minValue = 0; // Change min value
        sfxSlider.mainSlider.maxValue = 100; // Change max value
        sfxSlider.mainSlider.value = savedVolume; // Change current slider value
        sfxSlider.usePercent = false; // Enabling/disabling percent
        sfxSlider.useRoundValue = true; // Show simplified value
        sfxSlider.mainSlider.onValueChanged.AddListener(OnSfxValueChanged);
    }

    private void OnSfxValueChanged(float value)
    {
        ViewModel.SetSfxVolume(value);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public override void Dispose()
    {
    }

    private void OnCloseButtonClick()
    {
        ViewModel.CloseSettingsPage();
        SetActive(false);
    }

    private void OnLanguageSelectionChanged(string language)
    {
        ViewModel.SetLanguage(language);
    }
}