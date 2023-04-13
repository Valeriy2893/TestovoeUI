using System.Collections.Generic;
using UnityEngine;
using static Item;

public class ButtonFire : MonoBehaviour
{
    // —сылки на компоненты
    [SerializeField] private UpPanel upPanel;
    [SerializeField] private SaveGame saveGame;

    private void Start()
    {
        saveGame = FindAnyObjectByType<SaveGame>();
    }
    public List<GameObject> GetAvailableBullets()
    {
        List<GameObject> availableBullets = new List<GameObject>();

        foreach (Slot slot in upPanel.slots)
        {
            LoadItem loadItem = slot.CurrentItem;

            if (loadItem != null && (loadItem.Item.Type == TypeItem.Bullet1 || loadItem.Item.Type == TypeItem.Bullet2))
            {
                availableBullets.Add(loadItem.gameObject);
            }
        }

        return availableBullets;
    }
    public void FireRandomBullet()
    {
        List<GameObject> availableBullets = GetAvailableBullets();

        if (availableBullets.Count != 0)
        {
            GameObject bullet = availableBullets[Random.Range(0, availableBullets.Count)];

            LoadItem bulletLoadItem = bullet.GetComponent<LoadItem>();

            if (bulletLoadItem.StackCount == 1)
            {
                Destroy(bullet);
            }
            else
            {
                bulletLoadItem.StackCount--;
                bulletLoadItem.AmountItem();
            }

        }
        else
        {
            print("ѕатронов не осталось");
        }
        saveGame.Save();
    }
}
