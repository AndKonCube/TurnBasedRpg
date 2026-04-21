using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassCardUI : MonoBehaviour
{
    public CharacterDataSO classData;
    [SerializeField] TMP_Text ClassName;
    [SerializeField] TMP_Text classStats;
    [SerializeField] Button selectButton;
    [SerializeField] GameObject selectedOutline;

    public void Setup(CharacterDataSO data, ClassSelectionUI classSelection)
    {
        classData = data;
        ClassName.text = data.characterName;
        classStats.text = "Atk: " + data.baseAttack + "\n Def :" + data.baseDefense + "\n Spd: " + data.baseSpeed + "\n Mag: " + data.baseMagicPower;
        selectButton.onClick.AddListener(() => classSelection.OnClassSelected(this));
        SetSelected(false);
    }

    public void SetSelected(bool selected)
    {
        selectedOutline.SetActive(selected);
    }
}
