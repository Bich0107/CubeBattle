using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    Transform playerTrans;
    [SerializeField] protected float activeDelayTime;
    [SerializeField] protected bool playerHit = false;

    protected WaitForSeconds activeDelay;

    protected virtual void Awake()
    {
        playerTrans = FindObjectOfType<PlayerController>().transform;
        activeDelay = new WaitForSeconds(activeDelayTime);
    }

    public virtual void Active() { }
    public virtual void Deactive() { }

    protected Vector3 GetPlayerDirection()
    {
        return (playerTrans.position - transform.position).normalized;
    }

    protected void RotateToPlayer()
    {
        transform.rotation = Quaternion.LookRotation(GetPlayerDirection());
    }
}
