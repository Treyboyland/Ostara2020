﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    [SerializeField]
    bool showCursor = false;

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            Cursor.visible = showCursor;
        }
    }
}
