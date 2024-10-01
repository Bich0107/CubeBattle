using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    static Stack<ICommand> undoStack = new Stack<ICommand>();
    static Queue<ICommand> commandQueue = new Queue<ICommand>();
    static ICommand currentCommand;
    static int maxCommandQueue = 3;

    public static void ExecuteCommand(ICommand command)
    {
        if (currentCommand == null || currentCommand.IsFinished())
        {
            command.Execute();
            currentCommand = command;
            undoStack.Push(command);
        }
        else if (commandQueue.Count < maxCommandQueue)
        {
            commandQueue.Enqueue(command);
        }
    }

    void Update()
    {
        if (commandQueue.Count > 0 && currentCommand.IsFinished())
        {
            currentCommand = commandQueue.Dequeue();
            if (currentCommand != null)
            {
                currentCommand.Execute();
            }
        }
    }
}
