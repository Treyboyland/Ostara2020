using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransport : MonoBehaviour
{
    Room room;

    [SerializeField]
    float secondsBetweenRoomMoves;

    float elapsed;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = Object.FindObjectOfType<Player>();
        StartCoroutine(RoomCheck());
    }

    private void Update()
    {
        elapsed += Mathf.Min(elapsed + Time.deltaTime, secondsBetweenRoomMoves);
    }

    IEnumerator RoomCheck()
    {
        bool actionAdded = false;
        while (true)
        {
            if (room == null)
            {
                while (room == null)
                {
                    yield return null;
                    room = Object.FindObjectOfType<Room>();
                    actionAdded = false;
                }
            }
            if (!room.gameObject.activeInHierarchy)
            {
                room.OnPlayerExit.RemoveListener(MovePlayer);
                actionAdded = false;
                room = null;
                yield return null;
                continue;
            }
            else if (!actionAdded)
            {
                actionAdded = true;
                room.OnPlayerExit.AddListener(MovePlayer);
            }
            yield return null;
        }
    }

    void MovePlayer(Room.PlayerExit exit)
    {
        if (elapsed >= secondsBetweenRoomMoves)
        {
            elapsed = 0;
            var pos = player.transform.position;
            if (exit == Room.PlayerExit.TOP || exit == Room.PlayerExit.BOTTOM)
            {
                pos.y *= -1;
            }
            else if (exit == Room.PlayerExit.LEFT || exit == Room.PlayerExit.RIGHT)
            {
                pos.x *= -1;
            }
            player.transform.position = pos;
        }
    }
}
