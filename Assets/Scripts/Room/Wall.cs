using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    float openY;

    [SerializeField]
    float closedY;

    public void SetOpen(bool open)
    {
        var localScale = transform.localScale;
        localScale.y = open ? openY : closedY;
        transform.localScale = localScale;
    }
}
