using System;
using System.Collections;
using System.Collections.Generic;
using PER.Common;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action PlayerLost;


    public void OnPlayerLost()
    {
        PlayerLost?.Invoke();
    }
}
