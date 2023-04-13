using UnityEngine;
using UnityEngine.UI;

public class SlotUnlocker : MonoBehaviour
{
    // Ссылки на компоненты
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private Slot slot;
    [SerializeField] private Image image;
    [SerializeField] private SaveGame saveGame;

    // Стоимость открытия слота
    [SerializeField] private int Price;

    private void Start()
    {
        slot=GetComponent<Slot>();
        moneyManager = FindAnyObjectByType<MoneyManager>();
        image = GetComponent<Image>();
        saveGame = FindAnyObjectByType<SaveGame>();

        UpdateSlotColor();
    }
    public void OpenSlot()
    {
        if (moneyManager.DecreaseMoney(Price))
        {
            image.color = new Color(0.7924528f, 0.6280511f, 0.3999644f);
            slot.IsOpen = true;
            GetComponent<Button>().enabled = false;
        }
        saveGame.Save();
    }

    private void UpdateSlotColor()
    {
        if (!slot.IsOpen)
        {
            image.color = Color.black;
        }
        else
        {
            image.color = new Color(0.7924528f, 0.6280511f, 0.3999644f);
        }
    }
}
