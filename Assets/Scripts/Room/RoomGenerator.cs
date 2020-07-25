using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGenerator : RoomGeneratorAbstract
{
    [SerializeField]
    List<Room> possibleRooms = new List<Room>();

    [SerializeField]
    Room goalRoom = null;

    [SerializeField]
    Room startRoom = null;

    Dictionary<Vector2Int, OpenDoors> map = new Dictionary<Vector2Int, OpenDoors>();

    Dictionary<Vector2Int, Room> chosenRooms = new Dictionary<Vector2Int, Room>();

    Room currentRoom;

    [SerializeField]
    Vector2Int roomNumRange = new Vector2Int();

    [SerializeField]
    int maxIterations = 0;

    [SerializeField]
    float radialProbability = 0;

    [SerializeField]
    PlayerTransport transport = null;

    Vector2Int currentLocation = new Vector2Int();

    Vector2Int goalLocation = new Vector2Int();

    // Start is called before the first frame update
    void Start()
    {
        //TODO: We arent going to worry about pooling, but that should be something done in the future...

        CreateDungeon();
        GetNewRoom(currentLocation);
        transport.OnPlayerTransported.AddListener(InstantiateNewRoom);
        //StartCoroutine(RoomLoop());
    }

    string GenerateMap(Vector2Int pos, int distance)
    {
        List<Room.PlayerExit> exits = new List<Room.PlayerExit>((Room.PlayerExit[])Enum.GetValues(typeof(Room.PlayerExit)));
        List<Room.PlayerExit> path = new List<Room.PlayerExit>(distance);
        for (int i = 0; i < distance; i++)
        {
            var chosen = exits[UnityEngine.Random.Range(0, exits.Count)];
            path.Add(chosen);
            exits = chosen.GetMeaningfulExits();
            pos = map.AddRoom(pos, chosen);
        }

        //Debug.Log("\r\n" + map.AsMap());
        //Debug.Log(path.AsString());
        //map.Traverse(path.AsString());
        return path.AsString();
    }

    void CreateDungeon()
    {
        Vector2Int origin = new Vector2Int();
        int goalIteration = UnityEngine.Random.Range(maxIterations / 2, maxIterations); //Further away from start
        for (int i = 0; i < maxIterations; i++)
        {
            string path = GenerateMap(origin, UnityEngine.Random.Range(roomNumRange.x, roomNumRange.y));
            if (i == goalIteration)
            {
                goalLocation = origin.Adjust(path);
            }
            if (!(UnityEngine.Random.Range(0.0f, 1.0f) < radialProbability))
            {
                //Move origin of stuff, or stay
                origin = origin.Adjust(path.Substring(0, UnityEngine.Random.Range(1, path.Length)));
            }
        }

        Debug.Log("\r\n" + map.AsMap());
        Debug.Log("\r\n" + map.AsMap(goalLocation));
    }

    void GetNewRoom(Vector2Int location)
    {
        currentLocation = location;
        //Debug.Log(currentLocation);
        if (currentRoom != null)
        {
            //Debug.Log("Room is not null");
            currentRoom.gameObject.SetActive(false);
        }

        if (chosenRooms.ContainsKey(currentLocation))
        {
            currentRoom = chosenRooms[currentLocation];
        }
        else
        {
            if (currentLocation == new Vector2Int())
            {
                currentRoom = Instantiate(startRoom);
            }
            else if (currentLocation == goalLocation)
            {
                currentRoom = Instantiate(goalRoom);
            }
            else
            {
                currentRoom = Instantiate(possibleRooms[UnityEngine.Random.Range(0, possibleRooms.Count)]);
            }
            chosenRooms.Add(currentLocation, currentRoom);
            OnNewRoomDiscovered.Invoke(map, chosenRooms);
        }

        currentRoom.SetDoors(map[currentLocation]);
        currentRoom.gameObject.SetActive(true);
    }

    void InstantiateNewRoom(Room.PlayerExit direction)
    {
        //Debug.Log(direction);
        GetNewRoom(currentLocation.Adjust(direction));
    }
}
