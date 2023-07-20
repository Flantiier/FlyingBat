using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    //Free listeners
    public UnityEvent responses { get; set; }
    //Events with GameEventListenerClass
    private List<GameEventListener> listeners = new List<GameEventListener>();

    //Raise the event method in the listeners
    [ContextMenu("Raise")]
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();

        responses?.Invoke();
    }

    //Add a listener to the list
    public void RegisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            return;

        listeners.Add(listener);
    }

    //Remove a listener from the list
    public void UnregisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            return;

        listeners.Remove(listener);
    }
}
