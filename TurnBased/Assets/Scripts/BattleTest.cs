using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    [SerializeField] List<CharacterDataSO> playerData;
    [SerializeField] List<CharacterDataSO> enemyData;

    void Start()
    {
        battleManager.StartBattle(playerData, enemyData);
    }
}