using System;

public interface ILocalizationController : IDisposable
{
    public void SetLanguage(string language);
    public string[] GetLanguageOptions();
}
