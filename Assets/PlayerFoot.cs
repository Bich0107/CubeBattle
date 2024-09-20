using UnityEngine;

public class PlayerFoot : MonoBehaviour, IHitByObstacle
{
    

    public void Hit() {
        Debug.Log("hit", gameObject);
    }
}
