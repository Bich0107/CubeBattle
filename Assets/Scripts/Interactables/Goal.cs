using UnityEngine;

public class Goal : MonoBehaviour, ITriggerByPlayer
{
    bool triggered = false;

    void OnEnable()
    {
        triggered = false;
    }

    public void Triggered()
    {
        if (triggered) return;

        triggered = true;
        Debug.Log("Player reached goal");
    }
}
