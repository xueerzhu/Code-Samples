using System;
using UnityEngine;

public interface IView<T> : IDisposable where T : IViewModel
{
    void Init(T viewModel);
}

public abstract class View<T> : MonoBehaviour, IView<T> where T : IViewModel
{
    [NonSerialized] protected T ViewModel;

    protected virtual void OnDestroy()
    {
        Dispose();
    }

    public abstract void Init(T viewModel);
    public abstract void Dispose();
}