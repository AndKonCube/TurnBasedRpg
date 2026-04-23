using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text statPointsText;
    [SerializeField] TMP_Text newSkillText;
    [SerializeField] Button attackButton;
    [SerializeField] Button defenseButton;
    [SerializeField] Button magicButton;
    [SerializeField] Button speedButton;
    [SerializeField] Button continueButton;

    void Start()
    {
        attackButton.onClick.AddListener(OnAttackClicked);
        defenseButton.onClick.AddListener(OnDefenseClicked);
        magicButton.onClick.AddListener(OnMagicClicked);
        speedButton.onClick.AddListener(OnSpeedClicked);
        continueButton.onClick.AddListener(OnContinueClicked);
        gameObject.SetActive(false);
    }

    public void Show(int newLevel, int bonusPoints, SkillDataSO unlockedSkill)
    {
        gameObject.SetActive(true);
        levelText.text = "Level Up! Now level " + newLevel;

        if (unlockedSkill != null)
            newSkillText.text = "New Skill Unlocked: " + unlockedSkill.skillName;
        else
            newSkillText.text = "";

        UpdateButtons();
    }

    private void OnAttackClicked()
    {
        if (PartyManager.Instance.bonusStatPoints > 0)
        {
            PartyManager.Instance.bonusAttack++;
            PartyManager.Instance.bonusStatPoints--;
            UpdateButtons();
        }
    }

    private void OnDefenseClicked()
    {
        if (PartyManager.Instance.bonusStatPoints > 0)
        {
            PartyManager.Instance.bonusDefense++;
            PartyManager.Instance.bonusStatPoints--;
            UpdateButtons();
        }
    }

    private void OnMagicClicked()
    {
        if (PartyManager.Instance.bonusStatPoints > 0)
        {
            PartyManager.Instance.bonusMagicPower++;
            PartyManager.Instance.bonusStatPoints--;
            UpdateButtons();
        }
    }

    private void OnSpeedClicked()
    {
        if (PartyManager.Instance.bonusStatPoints > 0)
        {
            PartyManager.Instance.bonusSpeed++;
            PartyManager.Instance.bonusStatPoints--;
            UpdateButtons();
        }
    }

    private void OnContinueClicked()
    {
        if (PartyManager.Instance.bonusStatPoints > 0)
        {
            Debug.Log("Distribute all points first");
            return;
        }
        gameObject.SetActive(false);
    }

    private void UpdateButtons()
    {
        int points = PartyManager.Instance.bonusStatPoints;
        statPointsText.text           = "Points remaining: " + points;
        attackButton.interactable     = points > 0;
        defenseButton.interactable    = points > 0;
        magicButton.interactable      = points > 0;
        speedButton.interactable      = points > 0;
    }
}