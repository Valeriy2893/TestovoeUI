using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon", fileName = "New Weapon")]
public class WeaponItem : Item
{
    [SerializeField] private TypeBullet gunBulletType;
    [SerializeField] private int damage;

    public TypeBullet GunBulletType => gunBulletType;
    public int Damage => damage;

    
}
