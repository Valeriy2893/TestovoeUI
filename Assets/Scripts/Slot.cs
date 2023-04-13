using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    // Ссылки на компоненты
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private SaveGame saveGame;

    // Открыт ли слот для использования
    public bool IsOpen;

    // Предмет, который находится в слоту
    public LoadItem CurrentItem;

    private void Start()
    {
        UpdateCurrentItem();

        saveGame = FindAnyObjectByType<SaveGame>();

    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            LoadItem draggedItem = eventData.pointerDrag.GetComponent<LoadItem>();

            // проверяем, что предмет, который мы бросаем, не является текущим предметом в слоту
            if (draggedItem != CurrentItem)
            {
                // если текущий слот пустой, просто добавляем предмет в слот
                if (CurrentItem == null && IsOpen)
                {
                    AddItem(draggedItem);
                }
                // иначе проверяем, можно ли добавить предмет к уже имеющемуся стаку
                else if (CurrentItem?.Item.Type == draggedItem?.Item.Type && draggedItem?.StackCount + CurrentItem?.StackCount <= CurrentItem?.Item.MaxStackCount)
                {
                    AddItem(draggedItem);
                    CurrentItem.AmountItem();
                }
            }

        }
        saveGame.Save();
    }
    private void AddItem(LoadItem item)
    {
        if (CurrentItem == null)
        {
            CurrentItem = item;
            item.transform.SetParent(transform, false);
            item.transform.localPosition = new Vector2(10, 10);
        }
        else
        {
            CurrentItem.StackCount += item.StackCount;
            Destroy(item.gameObject);
        }
    }

    private void UpdateCurrentItem()
    {
        if (transform.childCount == 0)
        {
            CurrentItem = null;
        }
        else
        {
            CurrentItem = transform.GetChild(0).GetComponent<LoadItem>();
        }
    }

}
