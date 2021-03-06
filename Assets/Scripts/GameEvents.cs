﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Unity Events are abstract classes, so this has to be done

public class NullEvent : UnityEvent { }

public class FloatEvent : UnityEvent<float> { }

public class IntEvent : UnityEvent<int> { }

public class Vector3Event : UnityEvent<Vector3> { }

public class PlayerExitEvent : UnityEvent<Room.PlayerExit> { }