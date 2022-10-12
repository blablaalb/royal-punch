using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PER.Common;

namespace Managers
{
    public abstract class InputManager : Singleton<InputManager>
    {
        public Action<Vector2> Move;
        public Action JoystickReleaed;
    }
}
