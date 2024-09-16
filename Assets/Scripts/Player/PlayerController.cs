using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Mover mover;
    [SerializeField] Rotater rotater;
    ITransformAffector[] affectors;
    Vector3 clickPos;
    Vector3 releasePos;
    float direction;
    bool released = false;
    [SerializeField] bool disableControl = false;

    void Start()
    {
        affectors = GetComponents<ITransformAffector>();
    }

    void Update()
    {
        if (!disableControl)
        {
            GetClickPos();
            GetReleasePos();
            ProcessMouseMovement();
        }

        CheckMovingStatus();
    }

    void GetClickPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPos = Input.mousePosition;
        }
    }

    void GetReleasePos()
    {
        if (Input.GetMouseButtonUp(0))
        {
            releasePos = Input.mousePosition;
            released = true;
        }
    }

    void ProcessMouseMovement()
    {
        if (!released) return;

        disableControl = true;

        Vector2 movementVector = (releasePos - clickPos).normalized;

        if (Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.y))
        {
            direction = movementVector.x > 0f ? -1f : 1f;
            rotater.Rotate(direction * Vector3.forward);
            mover.Move(-direction * Vector3.right);
        }
        else
        {
            direction = movementVector.y > 0f ? 1f : -1f;
            rotater.Rotate(direction * Vector3.right);
            mover.Move(direction * Vector3.forward);
        }
        
        releasePos = Vector3.zero;
        clickPos = Vector3.zero;
        released = false;
    }

    void CheckMovingStatus()
    {
        for (int i = 0; i < affectors.Length; i++)
        {
            if (affectors[i].IsBusy()) return;
        }

        disableControl = false;
    }
}
