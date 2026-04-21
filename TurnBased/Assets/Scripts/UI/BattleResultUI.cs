using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleResultUI : MonoBehaviour
{
    [SerializeField] GameObject resultPanel;
    [SerializeField] TMP_Text resultTitleText;
    [SerializeField] TMP_Text xpGainedText;
    [SerializeField] TMP_Text goldGainedText;
    [SerializeField] Button continueButton;

    void Start()
    {
        continueButton.onClick.AddListener(()=> SceneManager.LoadScene("CharacterCreation"));
        resultPanel.SetActive(false);
    }

    public void ShowVictory(int xpGained, int goldGained)
    {
        resultPanel.SetActive(true);
        resultTitleText.text = "Victory";
        xpGainedText.text = $"Xp Gained: {xpGained}";
        goldGainedText.text = $"Gold Gained: {goldGained}";
    }

    public void ShowDefeat()
    {
        resultPanel.SetActive(true);
        resultPanel.SetActive(true);
        resultTitleText.text = "Defeat";
        xpGainedText.text = $"Xp Gained: 0";
        goldGainedText.text = $"Gold Gained: 0";
    }
}
