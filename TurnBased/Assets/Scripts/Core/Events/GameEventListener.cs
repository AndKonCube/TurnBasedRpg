using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    GameEventSO gameEvent;
    UnityEvent response;

    public void OEnable()
    {
        gameEvent.RegisterListener(this);
    }

    public void OnDisable()
    {
        gameEvent.UnRegisterListener(this);
    }

    public void onEventRaised()
    {
        response.Invoke();
    }
}
