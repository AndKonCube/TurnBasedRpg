using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClassSelectionUI : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] Transform classContainer;
    [SerializeField] GameObject classCardPrefab;
    [SerializeField] Button ConfirmButton;
    [SerializeField]List<CharacterDataSO> availableClasses;
    ClassCardUI selectedCard;
    CharacterDataSO selectedClass;

    void Start()
    {
        ConfirmButton.onClick.AddListener(OnConfirmClicked);
        SpawnClassCards();
    }

private void SpawnClassCards()
{
    Debug.Log("availableClasses count: " + availableClasses.Count);
    Debug.Log("classCardPrefab: " + classCardPrefab);
    Debug.Log("classContainer: " + classContainer);

    foreach (CharacterDataSO classData in availableClasses)
    {
        GameObject card = Instantiate(classCardPrefab, classContainer);
        ClassCardUI cardUI = card.GetComponent<ClassCardUI>();
        Debug.Log("cardUI: " + cardUI);
        cardUI.Setup(classData, this);
    }
}

    public void OnClassSelected(ClassCardUI card)
    {
        if (selectedCard != null)
            selectedCard.SetSelected(false);


        selectedCard = card;
        selectedClass = card.classData;
        selectedCard.SetSelected(true);
    }

    private void OnConfirmClicked()
    {
        if(nameInputField.text == string.Empty)
        {
            Debug.Log("Please enter a name");
            return;
        }

        if(selectedCard == null)
        {
            Debug.Log("Please select a class");
            return;
        }

        PartyManager.Instance.SetPlayerClass(nameInputField.text,selectedClass);
        SceneManager.LoadScene("BattleScene");
    }
}
