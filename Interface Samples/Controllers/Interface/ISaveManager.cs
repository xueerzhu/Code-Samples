using System;
using R3;

public interface ISaveManager : IDisposable
{
    bool IsNewGame { get; }
    ReactiveProperty<int> LastFinishedFloorId { get; }
}