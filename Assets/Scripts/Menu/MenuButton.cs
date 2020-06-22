using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    TextMeshProUGUI buttonText;

    void Awake()
    {
        buttonText = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.fontStyle |= FontStyles.Underline;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.fontStyle ^= FontStyles.Underline;
    }
}