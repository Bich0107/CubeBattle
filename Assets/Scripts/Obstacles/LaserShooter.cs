using System.Collections;
using UnityEngine;

public class LaserShooter : Obstacle
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject beam;
    [SerializeField] Vector3 baseScale;
    [SerializeField] Vector3 expandScale;
    [SerializeField] float expandTime;
    bool aiming = false;

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
        RotateToPlayer();
        SetUpAimLine();

        StartCoroutine(CR_Shoot());

        Deactive();
    }

    void SetUpAimLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = true;
        aiming = true;
    }

    void UpdateAimLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, playerTrans.position);
    }

    void Update()
    {
        if (aiming)
        {
            UpdateAimLine();
            RotateToPlayer();
        }
    }

    IEnumerator CR_Shoot()
    {
        yield return activeDelay;
        aiming = false;
        lineRenderer.enabled = false;
        beam.SetActive(true);

        float tick = 0f;
        Vector3 beamPosition = Vector3.up;
        while (beam.transform.localScale != expandScale)
        {
            tick += Time.deltaTime;
            beam.transform.localScale = Vector3.Lerp(baseScale, expandScale, tick / expandTime);

            // to expand the beam without change its relative position
            beamPosition.y = beam.transform.localScale.y;
            beam.transform.localPosition = beamPosition;
            yield return null;
        }
    }

    public override void Deactive()
    {
        base.Deactive();
        beam.transform.localScale = baseScale;
        beam.SetActive(false);
    }
}
