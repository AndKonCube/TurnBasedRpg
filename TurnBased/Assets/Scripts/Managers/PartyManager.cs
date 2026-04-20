using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    private static PartyManager _instance;
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
        if (Instance != null)
        {
            Destroy(this);
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayerClass(CharacterDataSO characterDataSO)
    {
        selectedClass = characterDataSO;
        partyMembers.Clear();
        partyMembers.Add(characterDataSO);
    }

    public List<CharacterDataSO> GetParty()
    {
        return partyMembers;
    }
}
