using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Mover mover;
    [SerializeField] Rotater rotater;
    ITransformAffector[] affectors;
    float direction;

    void Start()
    {
        affectors = GetComponents<ITransformAffector>();
    }

    public void Move(Vector2 _movementVector)
    {
        if (Mathf.Abs(_movementVector.x) < Mathf.Abs(_movementVector.y))
        {
            direction = _movementVector.y > 0f ? 1f : -1f;
            rotater.Rotate(direction * Vector3.right);
            mover.Move(direction * Vector3.forward);
        }
        else
        {
            direction = _movementVector.x > 0f ? -1f : 1f;
            rotater.Rotate(direction * Vector3.forward);
            mover.Move(-direction * Vector3.right);
        }
    }

    public bool IsBusy()
    {
        for (int i = 0; i < affectors.Length; i++)
        {
            if (affectors[i].IsBusy()) return true;
        }

        return false;
    }
}
