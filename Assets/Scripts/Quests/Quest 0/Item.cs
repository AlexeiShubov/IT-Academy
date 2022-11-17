using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;
    
    public int ID { get; set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _rectTransform.parent.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
    }
}
