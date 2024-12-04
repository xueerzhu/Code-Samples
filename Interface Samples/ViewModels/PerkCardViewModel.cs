using System;
using UnityEngine;

public interface IPerkCardViewModel : IViewModel
{
    public Sprite Icon { get; }
    public string Name { get; }
    public string Description { get; }
    public void PerkCardClicked();
}

public class PerkCardViewModel : IPerkCardViewModel
{
    [NonSerialized] private readonly PerkData _perk;
    [NonSerialized] private readonly IPerkManager _perkManager;

    public PerkCardViewModel(IPerkManager perkManager)
    {
        _perkManager = perkManager;
        _perk = _perkManager.GetPerk();
    }

    public Sprite Icon => _perk.icon;
    public string Name => _perk.Name;
    public string Description => _perk.description;

    public void PerkCardClicked()
    {
        _perkManager.CollectPerk(_perk);
    }

    public void Init()
    {
    }

    public void Dispose()
    {
    }
}