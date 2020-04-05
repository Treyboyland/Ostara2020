using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickup : MonoBehaviour
{
    protected virtual bool CheckCollision(Collider2D other)
    {
        return true;
    }

    protected virtual void CompleteAction(Collider2D other)
    {
        //TODO: Override in subsequent classes
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckCollision(other))
        {
            CompleteAction(other);
        }
    }
}
