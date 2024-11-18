using System;
using System.Collections;
using UnityEngine;

public class Rotater : MonoBehaviour, ITransformAffector
{
    [SerializeField] Transform trans;
    [SerializeField] float rotateTime;
    [SerializeField] float angle;
    [Tooltip("Object will rotate angle degree numOfTurn time")]
    [SerializeField] int numOfTurn = 1;

    Quaternion rotation;
    [SerializeField] bool rotating = false;

    public void Rotate(Vector3 _rotateVector, Action _endAction = null)
    {
        if (rotating) return;

        rotation = Quaternion.Euler(_rotateVector * angle);
        StartCoroutine(CR_Rotate(rotation, _endAction));
    }

    IEnumerator CR_Rotate(Quaternion rotation, Action _endAction = null)
    {
        rotating = true;

        for (int i = 0; i < numOfTurn; i++)
        {
            float tick = 0f;
            Quaternion startRotaion = trans.rotation;
            Quaternion endRotation = rotation * trans.rotation;
            while (tick < rotateTime)
            {
                tick += Time.deltaTime;
                trans.rotation = Quaternion.Slerp(startRotaion, endRotation, tick / rotateTime);
                yield return null;
            }
        }

        rotating = false;

        _endAction?.Invoke();
    }

    public bool IsBusy() => rotating;
}
