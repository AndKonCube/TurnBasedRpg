using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    private static PartyManager _instance;
    private string playerName;
    public static PartyManager Instance{
        get{
        if(_instance == null)
            Debug.Log("[PartyManager]: PartyManager is null");    
        
        return _instance;
        }
    }

    [SerializeField] CharacterDataSO selectedClass;
    [SerializeField] List<CharacterDataSO> partyMembers;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayerClass(string name, CharacterDataSO characterDataSO)
    {
        playerName = name;
        selectedClass = characterDataSO;
        partyMembers.Clear();
        partyMembers.Add(characterDataSO);
    }

    public string GetPlayerName()
    {
        return playerName;
    }
    public List<CharacterDataSO> GetParty()
    {
        return partyMembers;
    }
}
