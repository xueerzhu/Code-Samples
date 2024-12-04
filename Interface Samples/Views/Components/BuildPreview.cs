using System;
using Shapes;
using UnityEngine;

public class BuildPreview : MonoBehaviour
{
    [SerializeField] private Color buildableColor;
    [SerializeField] private Color notBuildableColor;
    [NonSerialized] private Building _building;

    [NonSerialized] private Renderer _renderer;
    [NonSerialized] private Rectangle _towerAreaRectangle;

    public void OnEnable()
    {
        _building = GetComponentInParent<Building>();
        _renderer = GetComponent<Renderer>();
        _towerAreaRectangle = GetComponentInChildren<Rectangle>();
        if (_towerAreaRectangle == null) Debug.LogError("BuildPreview: No tower area rectangle found");
        SetActive(true);
        UpdateView(false, false);
        _building.OnPreviewUpdate += UpdateView;
    }

    private void OnDisable()
    {
        _building.OnPreviewUpdate -= UpdateView;
    }

    private void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void UpdateView(bool isPlaced, bool buildable)
    {
        //_renderer.material.SetColor("_BaseColor", buildable ? buildableColor: notBuildableColor);
        SetActive(!isPlaced);
        _towerAreaRectangle.Color = buildable ? buildableColor : notBuildableColor;
    }
}