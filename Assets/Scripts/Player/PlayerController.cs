using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Mover mover;
    [SerializeField] Rotater rotater;
    [SerializeField] Rotater turnRotater;
    ITransformAffector[] affectors;
    float direction;
    bool isFaceForward = true;

    void Start()
    {
        affectors = GetComponents<ITransformAffector>();
    }

    public void Move(Vector2 _movementVector, bool _moveable)
    {
        // compare x and y abs value, if x < y,  player is moving forward/backward, else player is moving left/right
        if (Mathf.Abs(_movementVector.x) < Mathf.Abs(_movementVector.y))
        {
            direction = _movementVector.y > 0f ? 1f : -1f;
            if (!isFaceForward) // turn player if was facing diferrent direction
            {
                isFaceForward = true;
                turnRotater.Rotate(direction * Vector3.up);
            }
            else if (_moveable)
            {
                direction = _movementVector.y > 0f ? 1f : -1f;
                rotater.Rotate(direction * Vector3.right);
                mover.Move(direction * Vector3.forward);
            }
        }
        else
        {
            direction = _movementVector.x > 0f ? -1f : 1f;
            if (isFaceForward) // turn player if was facing diferrent direction
            {
                isFaceForward = false;
                turnRotater.Rotate(-direction * Vector3.up);
            }
            else if (_moveable)
            {
                direction = _movementVector.x > 0f ? -1f : 1f;
                rotater.Rotate(direction * Vector3.forward);
                mover.Move(-direction * Vector3.right);
            }
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
