using System;
using System.Collections.Generic;
using R3;

public interface IBuildingController : IDisposable
{
    public Dictionary<int, BuildingData> KeyBindToUnlockedBuildingDataDict { get; }
    public ReactiveProperty<string> SelectedBuildingName { get; }
    public ReactiveProperty<bool> IsMachineActivated { get; }
    public void SelectBuildingWithKeyBindIdWrapper(int buildingKeyBindId);
    public void PlaceBuilding();
    public void CancelAction();
    public void RotateBuilding(float angle, float duration);
    public void SelectBuildingWithName(string name);
    public void ToggleMachineMode();
    public void Fire();
}