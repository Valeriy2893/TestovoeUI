using System.Collections.Generic;
using UnityEngine;

public class ButtonAddAmmo : MonoBehaviour
{
    // Ссылки на компоненты
    [SerializeField] private UpPanel upPanel;
    [SerializeField] private SaveGame saveGame;

    //Список пуль
    [SerializeField] private List<GameObject> bullets;

    private void Start()
    {
        saveGame = FindAnyObjectByType<SaveGame>();
    }

    public void AddAmmo()
    {
        upPanel.CheckAvailableSlots();

        for (int i = 0; i < upPanel.availableSlots.Count && i < bullets.Count; i++)
        {
            // Создаем экземпляр патрона и помещаем его в слот
            GameObject createBullet = Instantiate(bullets[i], upPanel.availableSlots[i].transform);
            upPanel.availableSlots[i].CurrentItem = createBullet.GetComponent<LoadItem>();
            createBullet.GetComponent<LoadItem>().StackCount = bullets[i].GetComponent<LoadItem>().Item.MaxStackCount;
        }
        upPanel.availableSlots.Clear();
        saveGame.Save();
    }
}
