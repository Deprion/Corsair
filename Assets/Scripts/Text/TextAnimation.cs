using UnityEngine;
using UnityEngine.EventSystems;

public class TextAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Vector2 offset;
    private Transform childText;

    public void OnPointerDown(PointerEventData eventData)
    {
        childText.transform.localPosition -= (Vector3)offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        childText.transform.localPosition += (Vector3)offset;
    }

    private void Awake()
    {
        childText = transform.GetChild(0);

        if (offset == Vector2.zero)
        {
            offset.y = GetComponent<RectTransform>().rect.height * 0.05f;
        }
    }
}
