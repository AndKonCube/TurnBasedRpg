using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName ="CharacterData") ]
public class CharacterDataSO : ScriptableObject
{
    public string characterName;
    public int baseHP;
    public int baseMP;
    public int baseAttack;
    public int baseDefense;
    public int baseMagicPower;
    public int baseSpeed;
    public List<SkillDataSO> skills = new List<SkillDataSO>();
    public Sprite sprite;
    public AnimatorController battleAnimator;
    public AudioClip deathSound;
    public int expirienceReward;
    public int goldReward;
    public AIBehaviorSO aiProfile;


}
