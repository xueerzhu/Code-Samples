using System;
using Shapes;
using UnityEngine;

public class BuildAreaPreviewRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f; // units per second

    [NonSerialized] private Rectangle _rectangle;

    private void Start()
    {
        _rectangle = GetComponent<Rectangle>();
        if (_rectangle == null)
        {
            Debug.LogError("Disc component not found!");
            enabled = false;
        }
    }

    private void Update()
    {
        // Update the dash offset to simulate rotation
        _rectangle.DashOffset += rotationSpeed * Time.deltaTime;
        _rectangle.DashOffset %= 1f; // Keep the offset within 0-1 range
    }
}