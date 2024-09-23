using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InputManager : MonoBehaviour
{
    [SerializeField] TileManager tileManager;
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

    bool CheckClickPos()
    {
        if (Mathf.Approximately(clickPos.x, releasePos.x) && Mathf.Approximately(clickPos.y, releasePos.y))
        {
            return false;
        }
        return true;
    }
}
