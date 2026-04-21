using TMPro;
using UnityEngine;

public class BattleLogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;

    public void Log(string message)
    {
        logText.text += message + "\n";
    }

    public void Clear()
    {
        logText.text = "";
    }
}