using System;
using R3;

public interface IDeckViewModel : IViewModel
{
    public ReactiveProperty<bool> CanDraw { get; }
    public void OnDraw();
}

public class DeckViewModel : IDeckViewModel
{
    [NonSerialized] private readonly IDeckController _deckController;

    public DeckViewModel(IDeckController deckController)
    {
        _deckController = deckController;
        Bind();
    }

    public ReactiveProperty<bool> CanDraw => _deckController.CanDraw;


    public void OnDraw()
    {
        _deckController.DrawCard();
    }

    public void Init()
    {
    }

    public void Dispose()
    {
    }

    private void Bind()
    {
    }
}