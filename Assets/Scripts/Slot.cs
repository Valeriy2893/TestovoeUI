using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    // ������ �� ����������
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private SaveGame saveGame;

    // ������ �� ���� ��� �������������
    public bool IsOpen;

    // �������, ������� ��������� � �����
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

            // ���������, ��� �������, ������� �� �������, �� �������� ������� ��������� � �����
            if (draggedItem != CurrentItem)
            {
                // ���� ������� ���� ������, ������ ��������� ������� � ����
                if (CurrentItem == null && IsOpen)
                {
                    AddItem(draggedItem);
                }
                // ����� ���������, ����� �� �������� ������� � ��� ���������� �����
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
