using System.Collections.Generic;
using UnityEngine;
using static Item;

public class ButtonAddItem : MonoBehaviour
{
    // Ссылки на компоненты
    [SerializeField] private UpPanel upPanel;
    [SerializeField] private SaveGame saveGame;

    // Списки предметов разных типов
    [SerializeField] private List<GameObject> torsos;
    [SerializeField] private List<GameObject> heads;
    [SerializeField] private List<GameObject> weapons;

    private void Start()
    {
        saveGame = FindAnyObjectByType<SaveGame>();
    }

    public void AddItem()
    {
        List<GameObject> randomItems = GetRandomItems();

        upPanel.CheckAvailableSlots();

        for (int i = 0; i < randomItems.Count; i++)
        {
            LoadItem currentItem = FindItemOfType(randomItems[i].GetComponent<LoadItem>().Item.Type);

            if (currentItem != null)
            {
                currentItem.StackCount++;
                currentItem.AmountItem();
            }
            else
            {
                if (i < upPanel.availableSlots.Count && randomItems.Count <= upPanel.availableSlots.Count)
                {
                    // Создаем экземпляр предмета и помещаем его в слот
                    GameObject createItem = Instantiate(randomItems[i], upPanel.availableSlots[i].transform);
                    upPanel.availableSlots[i].CurrentItem = createItem.GetComponent<LoadItem>();
                }
                else
                {
                    print("Нет слотов для развещения всех Элементов");
                }
            }
        }
        upPanel.availableSlots.Clear();
        saveGame.Save();
    }


    private LoadItem FindItemOfType(TypeItem type)
    {
        foreach (Slot slot in upPanel.slots)
        {
            LoadItem loadItem = slot.CurrentItem;

            if (loadItem != null && loadItem.Item.Type == type && loadItem.StackCount < loadItem.Item.MaxStackCount)
            {
                return loadItem;
            }
        }

        return null;
    }

    private List<GameObject> GetRandomItems()
    {
        List<GameObject> randomItems = new();
        // Выбираем случайный предмет каждого типа

        GameObject randomTorso = torsos[Random.Range(0, torsos.Count)];
        randomItems.Add(randomTorso);

        GameObject randomHead = heads[Random.Range(0, heads.Count)];
        randomItems.Add(randomHead);

        GameObject randomWeapon = weapons[Random.Range(0, weapons.Count)];
        randomItems.Add(randomWeapon);

        return randomItems;
    }
}
