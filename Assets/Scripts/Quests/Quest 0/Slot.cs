using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Transform Child;
    
    public int ID { get; set; }

    public void OnDrop(PointerEventData eventData)
    {
        Child.transform.SetParent(eventData.pointerDrag.transform.parent);
        eventData.pointerDrag.transform.parent.GetComponent<Slot>().Child = Child;
        Child.transform.localPosition = Vector3.zero;

        Child = eventData.pointerDrag.transform;
        Child.transform.SetParent(transform);
        Child.transform.localPosition = Vector3.zero;
    }
}
