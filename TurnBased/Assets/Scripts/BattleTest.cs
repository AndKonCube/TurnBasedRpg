using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    [SerializeField] List<CharacterDataSO> playerData;
    [SerializeField] bool useDummyData = true;

    void Start()
    {
        if (useDummyData)
        {
            // skip PartyManager and EncounterManager entirely
            EncounterManager.Instance.GenerateEncounter();
            battleManager.StartBattle(
                playerData,
                EncounterManager.Instance.GetCurrrentEnemies()
            );
        }
        else
        {
            // normal flow through PartyManager
            EncounterManager.Instance.GenerateEncounter();
            battleManager.StartBattle(
                PartyManager.Instance.GetParty(),
                EncounterManager.Instance.GetCurrrentEnemies()
            );
        }
    }
}