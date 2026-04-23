using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    private static PartyManager _instance;
    public static PartyManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogWarning("[PartyManager]: Instance is null");
            return _instance;
        }
    }

    private string playerName;
    private CharacterDataSO selectedClass;
    private List<CharacterDataSO> partyMembers;
    private List<SkillDataSO> unlockedSkills;

    public int currentLevel = 1;
    public int currentXP;
    public int currentHP;
    public int currentMP;
    public int bonusAttack;
    public int bonusDefense;
    public int bonusMagicPower;
    public int bonusSpeed;
    public int bonusStatPoints;

    [SerializeField] private LevelUpUI levelUpUI;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        partyMembers = new List<CharacterDataSO>();
        unlockedSkills = new List<SkillDataSO>();
    }

    public void SetPlayerClass(string name, CharacterDataSO characterDataSO)
    {
        playerName = name;
        selectedClass = characterDataSO;
        partyMembers.Clear();
        partyMembers.Add(characterDataSO);
    }

    public string GetPlayerName() => playerName;
    public List<CharacterDataSO> GetParty() => partyMembers;

    public void AddXP(int amount)
    {
        currentXP += amount;
        int newLevel = ExperienceSystem.GetLevelFromXP(currentXP);
        if (newLevel > currentLevel)
            ProcessLevelUp(newLevel);
    }

    private void ProcessLevelUp(int newLevel)
    {
        SkillDataSO lastUnlocked = null;

        for (int level = currentLevel + 1; level <= newLevel; level++)
        {
            if (level - 1 >= selectedClass.levelProgression.Count) break;

            LevelUpDataSO levelData = selectedClass.levelProgression[level - 1];
            currentHP += levelData.autoHPGrowth;
            currentMP += levelData.autoMPGRowth;
            bonusStatPoints += levelData.bonusStatPoints;

            if (levelData.skillUnlocked != null)
            {
                unlockedSkills.Add(levelData.skillUnlocked);
                lastUnlocked = levelData.skillUnlocked;
            }
        }

        currentLevel = newLevel;

        LevelUpUI levelUpUI = FindObjectOfType<LevelUpUI>();
        if(levelUpUI != null)
        levelUpUI.Show(newLevel, bonusStatPoints, lastUnlocked);
        else
        Debug.Log($"[PartyManager]: Level up UI doenst exists");
    }
}