using UnityEngine;

public class ItemCommand : ActionCommand
{
    private ItemDataSO itemData;
    private Inventory inventory;

    public ItemCommand(CombatUnit source, System.Collections.Generic.List<CombatUnit> targets,
                       ItemDataSO item, Inventory inventory) : base(source, targets)
    {
        this.itemData  = item;
        this.inventory = inventory;
    }

    public override void Execute()
    {
        if (!inventory.HasItem(itemData))
            return;

        foreach (CombatUnit target in targets)
        {
            if (target.isAlive)
            {
                switch (itemData.effectType)
                {
                    case ItemEffectType.RestoreHP:
                        target.HealHP(itemData.potency);
                        break;

                    case ItemEffectType.RestoreMP:
                        target.RestoreMP(itemData.potency);
                        break;

                    case ItemEffectType.CureStatus:
                        StatusEffectHandler.RemoveAll(target);
                        break;

                    case ItemEffectType.RaiseAttack:
                        target.attackModifier += itemData.potency;
                        break;

                    case ItemEffectType.RaiseDefense:
                        target.defenseModifier += itemData.potency;
                        break;
                }
            }
        }

        inventory.RemoveItem(itemData);
    }
}