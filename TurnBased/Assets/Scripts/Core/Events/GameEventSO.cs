using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "States/GameStates", order = 1)]
public class GameEventSO : ScriptableObject
{
    List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        foreach(GameEventListener listerner in listeners)
        {
            listerner.onEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

}


