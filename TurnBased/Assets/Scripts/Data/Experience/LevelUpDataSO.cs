using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(fileName = "Experience", menuName ="ExperienceData") ]
public class LevelUpDataSO : ScriptableObject
{
    [SerializeField] public int level;
    [SerializeField] public int autoHPGrowth;
    [SerializeField] public int autoMPGRowth;
    [SerializeField] public int bonusStatPoints;
    [SerializeField] public SkillDataSO skillUnlocked;
}
