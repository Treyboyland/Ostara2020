using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomExitTrigger : MonoBehaviour
{
    [SerializeField]
    Room room;

    [SerializeField]
    Room.PlayerExit exitType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            room.OnPlayerExit.Invoke(exitType);
        }
    }

}
