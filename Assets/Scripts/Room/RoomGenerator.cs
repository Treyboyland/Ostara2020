using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField]
    List<Room> possibleRooms = new List<Room>();


    Dictionary<Vector2Int, OpenDoors> map = new Dictionary<Vector2Int, OpenDoors>();

    Room currentRoom;

    [SerializeField]
    int maxDistance = 0;


    // Start is called before the first frame update
    void Start()
    {
        //TODO: We arent going to worry about pooling, but that should be something done in the future...
        GenerateMap();
    }

    void GenerateMap()
    {
        Vector2Int pos = new Vector2Int();
        List<Room.PlayerExit> exits = new List<Room.PlayerExit>((Room.PlayerExit[])Enum.GetValues(typeof(Room.PlayerExit)));
        List<Room.PlayerExit> path = new List<Room.PlayerExit>(maxDistance);
        for (int i = 0; i < maxDistance; i++)
        {
            var chosen = exits[UnityEngine.Random.Range(0, exits.Count)];
            path.Add(chosen);
            exits = chosen.GetMeaningfulExits();
            pos = map.AddRoom(pos, chosen);
        }

        Debug.Log("\r\n" + map.AsMap());
        Debug.Log(path.AsString());
    }
}
