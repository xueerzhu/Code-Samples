using System;

public interface ITimeManager : IDisposable
{
    int CurrentSecond { get; }
    event Action OnSecondChange;
    void Update();
}