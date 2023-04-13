using UnityEngine;

[CreateAssetMenu(menuName = "Item/Torso", fileName = "New TorsoItem")]
public class TorsoItem : Item
{
    [SerializeField] private int protection;

    public int Protection => protection;
}
