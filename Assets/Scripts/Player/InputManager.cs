using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class InputManager : MonoBehaviour
{
    [SerializeField] TileManager tileManager;
    [SerializeField] PlayerController player;
    [SerializeField] bool disableControl = false;
    Vector3 clickPos;
    Vector3 releasePos;
    Vector2 movementVector;
    bool released = false;

#if UNITY_EDITOR_WIN
    void Update()
    {
        if (!disableControl)
        {
            ProcessKeyboardInput();
        }
    }

    void ProcessKeyboardInput()
    {
        if (Input.anyKeyDown)
        {
            float xDir = Input.GetAxis("Horizontal");
            float zDir = Input.GetAxis("Vertical");

            if (xDir != 0f)
            {
                movementVector = Vector2.right * xDir;
            }
            else if (zDir != 0f)
            {
                movementVector = Vector2.up * zDir;
            }

            if (CheckMoveTile(movementVector))
            {
                ICommand command = new MoveCommand(player, movementVector);
                CommandInvoker.ExecuteCommand(command);
            }
        }
    }

#else
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

        movementVector = (releasePos - clickPos).normalized;

        if (CheckClickPos())
        {
            if (CheckMoveTile(movementVector))
            {
                ICommand command = new MoveCommand(player, movementVector);
                CommandInvoker.ExecuteCommand(command);

                releasePos = Vector3.zero;
                clickPos = Vector3.zero;
            }
        }

        released = false;
    }

    bool CheckClickPos()
    {
        if (Mathf.Approximately(clickPos.x, releasePos.x) && Mathf.Approximately(clickPos.y, releasePos.y))
        {
            return false;
        }
        return true;
    }
#endif

    bool CheckMoveTile(Vector2 moveVector)
    {
        Tile playerTile = tileManager.GetTile(player.transform.position);
        Tile moveTile;

        if (Mathf.Abs(moveVector.x) < Mathf.Abs(moveVector.y))
        {
            // get front or back tile
            if (moveVector.y > 0f) moveTile = tileManager.GetFrontTile(playerTile);
            else moveTile = tileManager.GetBackTile(playerTile);
        }
        else
        {
            // get left or right tile
            if (moveVector.x > 0f) moveTile = tileManager.GetRightTile(playerTile);
            else moveTile = tileManager.GetLeftTile(playerTile);
        }

        if (moveTile == null) return false;

        return true;
    }
}
