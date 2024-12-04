using System;
using R3;
using UnityEngine;

public interface IBuildingItemViewModel
{
    public ReactiveProperty<bool> IsSelected { get; }
    public Color SelectedOutlineColor { get; }
    public Sprite Icon { get; }
    public string Name { get; }
    public IBuildingData BuildingData { get; }
    public Card Card { get; }
    public event Action CardCollectionChanged;
    public event Action MarkForDestroy;
    void BuildingCardClicked();
}