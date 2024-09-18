using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    PlayerController player;
    Vector2 moveVector;

    public MoveCommand(PlayerController _player, Vector2 _moveVector)
    {
        player = _player;
        moveVector = _moveVector;
    }

    public void Execute()
    {
        player.Move(moveVector);
    }

    public void Undo()
    {
        player.Move(-moveVector);
    }

    public bool IsFinished() => !player.IsBusy();
}
