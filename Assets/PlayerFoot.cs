using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ITriggerByPlayer target = other.GetComponent<ITriggerByPlayer>();
        if (target != null)
        {
            target.Triggered();
        }
    }
}
