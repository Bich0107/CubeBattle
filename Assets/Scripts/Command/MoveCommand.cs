using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    PlayerController player;
    Vector2 moveVector;
    bool movable;

    public MoveCommand(PlayerController _player, Vector2 _moveVector, bool _movable)
    {
        player = _player;
        moveVector = _moveVector;
        movable = _movable;
    }

    public void Execute()
    {
        player.Move(moveVector, movable);
    }

    public bool IsFinished() => !player.IsBusy();
}
