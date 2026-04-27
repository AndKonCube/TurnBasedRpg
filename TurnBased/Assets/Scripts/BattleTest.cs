using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    [SerializeField] EncounterManager encounterManager;
    [SerializeField] List<CharacterDataSO> playerData;
    [SerializeField] List<CharacterDataSO> enemyData;

void Start()
{
    EncounterManager.Instance.GenerateEncounter();
    battleManager.StartBattle(
        PartyManager.Instance.GetParty(),
        EncounterManager.Instance.GetCurrrentEnemies()
    );
}
}