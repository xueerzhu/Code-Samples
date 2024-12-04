using System;
using System.Collections.Generic;

public interface IPerkManager : IDisposable
{
    public List<PerkData> OwnedPerks { get; }
    public List<PerkData> Perks { get; }
    public void CollectPerk(PerkData perk);
    public PerkData GetPerk();
    public float GetStatModifier(PerkData.StatChanged statChanged);
}