using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public abstract class Item : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private int maxStackCount;
    [SerializeField] private float mass;
    [SerializeField] private TypeItem type;

    public Sprite Icon => icon;
    public int MaxStackCount => maxStackCount;
    public float Mass => mass;
    public TypeItem Type => type;
   
    public enum TypeItem
    {
        Bullet1,
        Bullet2,
        Gun1,
        Gun2,
        Cap,
        Helm,
        BodyArmor,
        Jacket,
        None
    }

    public enum TypeBullet
    {
        Bullet1,
        Bullet2,
    }
}






