using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegendItemView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI name;

    public void Init(Sprite sprite, string name)
    {
        icon.sprite = sprite;
        this.name.text = name;
    }
}