using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Ссылки на компоненты
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform tempParent;
    

    // Переменные для хранения предыдущего положения объекта
    private Vector2 oldLocalPosition;
    private Transform oldParent;
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        tempParent = GetComponentInParent<ScrollRect>().gameObject.GetComponent<RectTransform>();
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetDraggableObjectState(true);

        SaveOldPositionAndParent();

        transform.parent.GetComponent<Slot>().CurrentItem = null;

        transform.SetParent(tempParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       

        SetDraggableObjectState(false);

        if (transform.parent == tempParent)
        {
            RestoreOldPositionAndParent();
        }
    }

    private void SetDraggableObjectState(bool isDragging)
    {
        canvasGroup.blocksRaycasts = !isDragging;
        canvasGroup.alpha = isDragging ? 0.5f : 1f;
    }
    private void SaveOldPositionAndParent()
    {
        oldParent = transform.parent;
        oldLocalPosition = rectTransform.anchoredPosition;
    }

    private void RestoreOldPositionAndParent()
    {
        transform.SetParent(oldParent);
        rectTransform.anchoredPosition = oldLocalPosition;

        oldParent.GetComponent<Slot>().CurrentItem = gameObject.GetComponent<LoadItem>();
    }

}

