using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler
{
    public event System.Action<PointerEventData> OnDragged;

    public void OnDrag(PointerEventData eventData)
    {
        OnDragged?.Invoke(eventData);
    }
}
