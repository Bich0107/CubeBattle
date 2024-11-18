using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Mover mover;
    [SerializeField] Rotater verticalRotater;
    [SerializeField] Rotater horizontalRotater;
    [SerializeField] RuneDisplayer runeDisplayer;
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
                if (direction >= 0f) runeDisplayer.TurnRight();
                else runeDisplayer.TurnLeft();

                isFaceForward = true;
                horizontalRotater.Rotate(direction * Vector3.up);
            }
            else if (_moveable)
            {
                if (direction >= 0f) runeDisplayer.RotateForward();
                else runeDisplayer.RotateBackward();

                verticalRotater.Rotate(direction * Vector3.right);
                horizontalRotater.Rotate(direction * Vector3.right);

                Swap(ref verticalRotater, ref horizontalRotater);

                mover.Move(direction * Vector3.forward);
            }
        }
        else
        {   // moving left/right
            direction = _movementVector.x > 0f ? -1f : 1f;
            if (isFaceForward) // turn player if was facing diferrent direction
            {
                if (direction >= 0f) runeDisplayer.TurnLeft();
                else runeDisplayer.TurnRight();

                isFaceForward = false;
                horizontalRotater.Rotate(-direction * Vector3.up);
            }
            else if (_moveable)
            {
                if (direction >= 0f) runeDisplayer.RotateForward();
                else runeDisplayer.RotateBackward();

                verticalRotater.Rotate(direction * Vector3.forward);
                horizontalRotater.Rotate(direction * Vector3.forward);

                Swap(ref verticalRotater, ref horizontalRotater);

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

    void Swap(ref Rotater _rotater1, ref Rotater _rotater2)
    {
        Rotater temp = _rotater1;
        _rotater1 = _rotater2;
        _rotater2 = temp;
    }
}
