using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [NonSerialized] private DeckWidgetView _widgetView;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _widgetView.OnDrawPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _widgetView.OnDrawPointerExit(eventData);
    }

    public void Init(DeckWidgetView widgetView)
    {
        _widgetView = widgetView;
    }
}