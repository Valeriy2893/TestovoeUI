using System.Collections.Generic;
using UnityEngine;

public class ButtonDeliteItem : MonoBehaviour
{
    // —сылки на компоненты
    [SerializeField] private UpPanel upPanel;
    [SerializeField] private SaveGame saveGame;

    private void Start()
    {
        saveGame = FindAnyObjectByType<SaveGame>();
    }
    public void DeliteItem()
    {
        // »щем случайный непустой слот и удал€ем все предметы из него
        List<GameObject> nonEmptySlots = new();

        foreach (Slot slot in upPanel.slots)
        {
            LoadItem loadItem = slot.CurrentItem;

            if (loadItem != null)
            {
                nonEmptySlots.Add(slot.gameObject);
            }
        }

        if (nonEmptySlots.Count > 0)
        {
            GameObject randomSlot = nonEmptySlots[Random.Range(0, nonEmptySlots.Count)];

            Destroy(randomSlot.transform.GetChild(0).gameObject);
        }
        else
        {
            print("ѕусто");
        }

        saveGame.Save();
    }
}
