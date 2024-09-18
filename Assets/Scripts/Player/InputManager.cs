using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Vector3 clickPos;
    [SerializeField] Vector3 releasePos;
    bool released = false;
    [SerializeField] bool disableControl = false;

    void Update()
    {
        if (!disableControl)
        {
            GetClickPos();
            GetReleasePos();
            ProcessMouseMovement();
        }
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

        Vector2 movementVector = (releasePos - clickPos).normalized;

        ICommand command = new MoveCommand(player, movementVector);
        CommandInvoker.ExecuteCommand(command);

        releasePos = Vector3.zero;
        clickPos = Vector3.zero;

        released = false;
    }
}