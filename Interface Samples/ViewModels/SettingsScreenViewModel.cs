using System;

public interface ISettingsScreenViewModel : IViewModel
{
    string[] LanguageOptions { get; }
    public void CloseSettingsPage();
    public void SetLanguage(string language);
    public void SetSfxVolume(float value);
}

public class SettingsScreenViewModel : ISettingsScreenViewModel
{
    [NonSerialized] private readonly ILocalizationController _localizationController;

    public SettingsScreenViewModel(ILocalizationController localizationController)
    {
        _localizationController = localizationController;
    }

    public void Init()
    {
    }

    public void Dispose()
    {
    }

    public void CloseSettingsPage()
    {
        Dispose();
    }

    public void SetLanguage(string language)
    {
        _localizationController.SetLanguage(language);
    }

    public void SetSfxVolume(float value)
    {
        SfxController.Instance.SetSfxVolume(value);
    }

    public string[] LanguageOptions => _localizationController.GetLanguageOptions();

    public void QuitToDesktop()
    {
    }
}