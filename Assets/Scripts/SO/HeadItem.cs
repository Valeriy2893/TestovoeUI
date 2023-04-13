using UnityEngine;

[CreateAssetMenu(menuName = "Item/Head", fileName = "New HeadItem")]
public class HeadItem : Item
{
    [SerializeField] private int protection;
    public int Protection => protection;
}
