using System.Collections.Generic;
using UnityEngine;

public class UpPanel : MonoBehaviour
{
    // ��� ����� 
    public List<Slot> slots;

    //��������� �����
    public List<Slot> availableSlots;

    public void CheckAvailableSlots()
    {
        foreach (Slot slot in slots)
        {
            if (slot.CurrentItem == null && slot.IsOpen)
            {
                availableSlots.Add(slot);
            }
        }
    }
}

