using System.Collections;
using UnityEngine;

public class ScrollingPillar : Obstacle
{
    static string s_triggerActive = "active";
    MovingObject moveObject;
    RotateObject rotateObject;
    Animator animator;

    protected override void Awake()
    {
        base.Awake();

        moveObject = GetComponent<MovingObject>();
        rotateObject = GetComponent<RotateObject>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (playerHit) return;

        IHitByObstacle hit = other.GetComponentInParent<IHitByObstacle>();
        if (hit != null)
        {
            hit.Hit();
            playerHit = true;
        }
    }

    public override void Active()
    {
        // play animation and start moving sequence
        animator.SetTrigger(s_triggerActive);
        StartCoroutine(CR_MoveSequence());

        Deactive();
    }

    IEnumerator CR_MoveSequence()
    {
        RotateToPlayer();

        // store the player direction when obstacle first detect player
        Vector3 playerDirection = GetPlayerDirection();

        yield return activeDelay;

        rotateObject.Rotate();
        moveObject.Move(playerDirection);
    }
}
