using System;
using Shapes;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(MapLineAnimator))]
[RequireComponent(typeof(Line))]
public class MapNodeLineView : MonoBehaviour
{
    [FormerlySerializedAs("_animateColor")] [SerializeField]
    private Color animateColor;

    [FormerlySerializedAs("_normalColor")] [SerializeField]
    private Color normalColor;

    [NonSerialized] private Line _line;

    [NonSerialized] private MapLineAnimator _mapLineAnimator;
    [NonSerialized] private RectTransform _rectTransform;

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mapLineAnimator = GetComponent<MapLineAnimator>();
        _line = GetComponent<Line>();
    }

    private void OnDestroy()
    {
    }

    public void Init(Vector2 start, Vector2 end)
    {
        // calc transfrom 
        //transform.position = Vector3.Lerp(start, end, 0.5f);
        //_rectTransform.anchoredPosition = Vector3.Lerp(start, end, 0.5f);
        // position
        //_line.Start = transform.InverseTransformPoint(start);
        //_line.End = transform.InverseTransformPoint(end);
        _line.Start = start;
        _line.End = end;
    }

    public void SetAnimates(bool animates)
    {
        _mapLineAnimator.Animates = animates;
        _line.Color = animates ? animateColor : normalColor;
    }
}