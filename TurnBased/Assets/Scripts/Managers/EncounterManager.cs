using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    private static EncounterManager _instance;
    [SerializeField] private List<CharacterDataSO> enemyPool;
    [SerializeField] private List<CharacterDataSO> currentEnemies;
    private int minEnemies = 1;
    private int maxEnemies = 3;
    public static EncounterManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogWarning("[EncounterManager]: Instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GenerateEncounter()
    {
        currentEnemies.Clear();
        int count = Random.Range(minEnemies,maxEnemies+1);
        for(int i = 0; i < count; i++)
        {
            CharacterDataSO randomEnemy = enemyPool[Random.Range(0,enemyPool.Count)];
            currentEnemies.Add(randomEnemy);
        }
    }

    public List<CharacterDataSO> GetCurrrentEnemies()
    {
        return currentEnemies;
    }

    private int SetMaxEnemies(int max)
    {
        return maxEnemies = max;
    }
}
