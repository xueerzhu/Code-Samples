using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMachineSelectionView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image buttonBg;
    [SerializeField] private Color enabledBgColor;
    [SerializeField] private Color disabledBgColor;

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        label.color = enabledBgColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        label.color = Color.white;
    }

    public void SetEnabledState(bool enabled)
    {
        buttonBg.color = enabled ? enabledBgColor : disabledBgColor;
    }
}