using UnityEngine;
using UnityEngine.UI;

public class FleeButton : MonoBehaviour
{
    [SerializeField] Button fleeButton;
    [SerializeField] BattleManager battleManager;

    void Start()
    {
        fleeButton.onClick.AddListener(() => battleManager.Flee());
    }
}