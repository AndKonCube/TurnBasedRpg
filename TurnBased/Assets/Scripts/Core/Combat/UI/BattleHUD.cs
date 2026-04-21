using System.Collections.Generic;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] private GameObject unitCardPrefab;
    [SerializeField] private Transform playerHudContainer;
    [SerializeField] private Transform enemyHudContainer;

    private List<UnitHUDCard> playerCards = new List<UnitHUDCard>();
    private List<UnitHUDCard> enemyCards = new List<UnitHUDCard>();

    public void Initialize(List<CombatUnit> playerUnits, List<CombatUnit> enemyUnits)
    {
        foreach (CombatUnit unit in playerUnits)
        {
            GameObject card = Instantiate(unitCardPrefab, playerHudContainer);
            UnitHUDCard cardUI = card.GetComponent<UnitHUDCard>();
            cardUI.SetUp(unit);
            playerCards.Add(cardUI);
        }

        foreach (CombatUnit unit in enemyUnits)
        {
            GameObject card = Instantiate(unitCardPrefab, enemyHudContainer);
            UnitHUDCard cardUI = card.GetComponent<UnitHUDCard>();
            cardUI.SetUp(unit);
            enemyCards.Add(cardUI);
        }
    }

    public void RefreshAllCards()
    {
        foreach (UnitHUDCard card in playerCards)
            card.UpdateHP();
        foreach (UnitHUDCard card in enemyCards)
            card.UpdateHP();
    }
}