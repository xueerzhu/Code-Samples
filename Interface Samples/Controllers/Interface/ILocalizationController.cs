using System;

public interface ILocalizationController : IDisposable
{
    public void SetLanguage(string language);
    public string[] GetLanguageOptions();
}

// TODO: do I need this? other screen controllers don't have this