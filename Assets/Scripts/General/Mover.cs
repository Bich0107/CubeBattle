using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour, ITransformAffector
{
    [SerializeField] Transform trans;
    [SerializeField] Vector3 direction;
    [SerializeField] float distance;
    [SerializeField] float moveTime;
    [SerializeField] bool moving = false;

    public void Move(Vector3 _direction)
    {
        direction = _direction;
        StartCoroutine(CR_Move());
    }

    IEnumerator CR_Move()
    {
        moving = true;

        float tick = 0f;
        Vector3 startPos = trans.position;
        while (tick < moveTime)
        {
            tick += Time.deltaTime;
            trans.position = Vector3.Lerp(startPos, startPos + direction * distance, tick / moveTime);
            yield return null;
        }

        moving = false;
    }

    public bool IsBusy() => moving;

    public void Stop()
    {
        StopAllCoroutines();
    }
}
