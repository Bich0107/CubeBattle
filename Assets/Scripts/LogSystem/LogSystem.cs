using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSystem : MonoSingleton<LogSystem>
{
    public void Log(string _message)
    {
        Debug.Log(_message);
    }
}
