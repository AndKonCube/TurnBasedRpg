using UnityEngine;

public enum ItemCategory
{
    Cosnumable,
    Equipment
}
public enum ItemEffectType
{
    RestoreHP,
    RestoreMP,
    RestoreHPPercent,
    CureStatus,
    RaiseAttack,
    RaiseDefense
}
[CreateAssetMenu(fileName = "Item", menuName = "ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public ItemEffectType effectType;
    public int potency;
    public int attackBonus;
    public int defenseBonus;
    public int magicBonus;
    public int speedBonus;
    public int sellPrice;
}
