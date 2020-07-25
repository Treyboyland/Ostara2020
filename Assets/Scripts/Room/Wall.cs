using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    float openY = 0;

    [SerializeField]
    float closedY = 0;

    public bool IsDestructable = false;

    public void SetOpen(bool open)
    {
        var localScale = transform.localScale;
        localScale.y = open ? openY : closedY;
        transform.localScale = localScale;
    }
}
