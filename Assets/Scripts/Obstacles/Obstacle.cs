using System.Collections;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected Transform playerTrans;
    [SerializeField] protected float activeDelayTime;
    [SerializeField] protected float deactiveDelayTime;
    [SerializeField] protected bool playerHit = false;

    protected WaitForSeconds activeDelay;
    protected WaitForSeconds deactiveDelay;

    protected virtual void Awake()
    {
        playerTrans = FindObjectOfType<PlayerController>().transform;
        activeDelay = new WaitForSeconds(activeDelayTime);
        deactiveDelay = new WaitForSeconds(deactiveDelayTime);
    }

    public virtual void Active() { }
    public virtual void Deactive()
    {
        StartCoroutine(CR_Deactive());
    }

    protected IEnumerator CR_Deactive()
    {
        yield return deactiveDelay;
        gameObject.SetActive(false);
    }

    protected Vector3 GetPlayerDirection()
    {
        return (playerTrans.position - transform.position).normalized;
    }

    protected void RotateToPlayer()
    {
        transform.rotation = Quaternion.LookRotation(GetPlayerDirection());
    }
}
