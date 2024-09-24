using UnityEngine;

public class ObstacleTrigger : MonoBehaviour, ITriggerByPlayer
{
    [SerializeField] Obstacle obstacle;
    bool triggered = false;

    public void Triggered()
    {
        if (triggered) return;

        triggered = true;
        obstacle.Active();
    }
}
