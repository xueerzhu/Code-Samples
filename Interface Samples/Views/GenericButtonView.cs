using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GenericButtonView : MonoBehaviour
{
    [SerializeField] private Button entryButton;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Color fillColor;

    public void Bind(UnityAction onClick)
    {
        entryButton.onClick.RemoveAllListeners();
        entryButton.onClick.AddListener(onClick);
    }
}