using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    // Ссылка на компонент
    [SerializeField] private TMP_Text textMoney;

    // Переменная для настройки стартового значения из редактора
    [SerializeField] private int startingMoney;

    // Текущая сумма
    public int CurrentMoney;

    private void Start()
    {
        if (PlayerPrefs.GetInt("First") == 0)
        {
            CurrentMoney = startingMoney;
        }

        DisplayTextMoney();
    }

    public void IncreaseMoney(int amount)
    {
        CurrentMoney += amount;
        DisplayTextMoney();
    }

    public bool DecreaseMoney(int amount)
    {
        if (CurrentMoney >= amount)
        {
            CurrentMoney -= amount;

            DisplayTextMoney();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DisplayTextMoney()
    {
        textMoney.text = CurrentMoney.ToString();
    }
}

