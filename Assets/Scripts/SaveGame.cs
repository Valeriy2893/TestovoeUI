using System.Collections.Generic;
using UnityEngine;
using static Item;

[System.Serializable]
public class SaveGameJS
{
    public int CurrentMoneyJS;

    public List<int> StackCountJS = new();
    public List<bool> IsOpenJS = new();
    public List<TypeItem> CurrentItemJS = new();

}
public class SaveGame : MonoBehaviour
{
    public SaveGameJS SaveGameJS;

    [SerializeField] private MoneyManager moneyManager;

    [SerializeField] private List<Slot> slot = new();

    [SerializeField] private List<GameObject> loadItemPrefabs;

    private string jsonString;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("First") == 1)
        {
            Load();
        }

    }
    public void Save()
    {
        print("Игра сохранена");
        PlayerPrefs.SetInt("First", 1);
        GetSaveGameInfo();
        jsonString = JsonUtility.ToJson(SaveGameJS);
        PlayerPrefs.SetString("SaveData", jsonString);
    }
    public void Load()
    {
        jsonString = PlayerPrefs.GetString("SaveData");
        SaveGameJS = JsonUtility.FromJson<SaveGameJS>(jsonString);
        UpdateSaveGameData();
    }

    public void GetSaveGameInfo() 
    {
        SaveGameJS.CurrentMoneyJS = moneyManager.CurrentMoney;

        SaveGameJS.IsOpenJS.Clear();
        SaveGameJS.CurrentItemJS.Clear();
        SaveGameJS.StackCountJS.Clear();

        for (int i = 0; i < slot.Count; i++)
        {
            SaveGameJS.IsOpenJS.Add(slot[i].IsOpen);

            if (slot[i].transform.childCount > 0)
            {
                SaveGameJS.CurrentItemJS.Add(slot[i].CurrentItem.Item.Type);
                SaveGameJS.StackCountJS.Add(slot[i].gameObject.GetComponentInChildren<LoadItem>().StackCount);
            }
            else
            {
                // Добавляем пустой слот
                SaveGameJS.CurrentItemJS.Add(TypeItem.None);
                SaveGameJS.StackCountJS.Add(0);
            }
        }
    }
    public void UpdateSaveGameData()
    {
        moneyManager.CurrentMoney = SaveGameJS.CurrentMoneyJS;
        moneyManager.DisplayTextMoney();

        for (int i = 0; i < slot.Count; i++)
        {
            slot[i].IsOpen = SaveGameJS.IsOpenJS[i];

            if (i < SaveGameJS.CurrentItemJS.Count)
            {
                // Определяем тип префаба
                TypeItem itemType = SaveGameJS.CurrentItemJS[i];

                // Ищем префаб с нужным типом
                GameObject prefab = loadItemPrefabs.Find(j => j.GetComponent<LoadItem>().Item.Type == itemType);

                // Если префаб найден, создаем его
                if (prefab != null)
                {
                    GameObject CreateItem = Instantiate(prefab, slot[i].transform.position, Quaternion.identity, slot[i].transform);
                    CreateItem.transform.localPosition = new Vector2(-40, -40);
                    CreateItem.GetComponent<LoadItem>().StackCount = SaveGameJS.StackCountJS[i];
                }
            }
        }

    }

}
