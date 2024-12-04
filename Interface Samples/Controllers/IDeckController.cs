using System;
using ObservableCollections;
using R3;

public interface IDeckController : IDisposable
{
    public ReactiveProperty<bool> CanDraw { get; }
    public ObservableList<Card> Hand { get; }
    public void DrawCard();
    public void ConsumeCard(Card card);
}