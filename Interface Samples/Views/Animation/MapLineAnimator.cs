using System;
using Shapes;
using UnityEngine;

public class MapLineAnimator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f; // units per second

    [NonSerialized] private Line _line;

    public bool Animates { get; set; } = true;

    private void Start()
    {
        _line = GetComponent<Line>();
        if (_line == null)
        {
            Debug.LogError("Line component not found!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (!Animates) return;

        // Update the dash offset to simulate rotation
        _line.DashOffset += rotationSpeed * Time.deltaTime;
        _line.DashOffset %= 1f; // Keep the offset within 0-1 range
    }
}