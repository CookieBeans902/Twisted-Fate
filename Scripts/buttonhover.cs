using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform buttonTransform;
    private Vector3 originalScale;

    void Start()
    {
        buttonTransform = GetComponent<RectTransform>();
        originalScale = buttonTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonTransform.localScale = originalScale * 1.1f; // Enlarge on hover
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonTransform.localScale = originalScale; // Reset on exit
    }
}
