using System.Collections.Generic;
using UnityEngine;

public class UpPanel : MonoBehaviour
{
    // Все слоты 
    public List<Slot> slots;

    //Свободные слоты
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

