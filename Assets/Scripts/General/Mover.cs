using System;
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

    public void Move(Vector3 _direction, Action _endAction = null)
    {
        direction = _direction;
        StartCoroutine(CR_Move(_endAction));
    }

    IEnumerator CR_Move(Action _endAction = null)
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

        _endAction?.Invoke();
    }

    public bool IsBusy() => moving;

    public void Stop()
    {
        StopAllCoroutines();
    }
}
