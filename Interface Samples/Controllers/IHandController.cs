using System;
using ObservableCollections;

public interface IHandController : IDisposable
{
    public ObservableList<Card> CardsInHand { get; }
    public IBuildingController BuildingController { get; }
    public void SelectCard(Card card);
}