using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadItem : MonoBehaviour
{   
    // ������ �� ����������
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amount;

    // S.O. ������
    public Item Item;

    //������� ���������� ��������� � �����
    public int StackCount;

    private void Start()
    {
        icon.sprite = Item.Icon;

        AmountItem();
    }

    public void AmountItem()
    {
        if (StackCount <= 1)
        {
            StackCount = 1;
            amount.text = null;
        }
        else
        {
            amount.text = StackCount.ToString();
        }

    }
}

