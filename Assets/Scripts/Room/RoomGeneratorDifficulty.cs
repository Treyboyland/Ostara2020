using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGeneratorDifficulty : RoomGeneratorAbstract
{
    [SerializeField]
    List<Room> possibleRooms = new List<Room>();

    [SerializeField]
    List<RoomDifficultyAndCount> roomAndCounts = null;

    List<Room.RoomDifficulty> roomCountsNormal = new List<Room.RoomDifficulty>();

    Queue<Room.RoomDifficulty> roomQueue = new Queue<Room.RoomDifficulty>();

    Dictionary<Room.RoomDifficulty, List<Room>> roomsByDifficulty = new Dictionary<Room.RoomDifficulty, List<Room>>();

    [Serializable]
    public struct RoomDifficultyAndCount
    {
        public Room.RoomDifficulty Difficulty;
        public int Count;
    }


    [SerializeField]
    Room goalRoom = null;

    [SerializeField]
    Room startRoom = null;

    Dictionary<Vector2Int, OpenDoors> map = new Dictionary<Vector2Int, OpenDoors>();

    Dictionary<Vector2Int, Room> chosenRooms = new Dictionary<Vector2Int, Room>();

    Room currentRoom;

    [SerializeField]
    Vector2Int roomNumRange;

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
        SortRooms();

        CreateDungeon();
        GetNewRoom(currentLocation);
        transport.OnPlayerTransported.AddListener(InstantiateNewRoom);
        //StartCoroutine(RoomLoop());
    }

    void SortRooms()
    {
        foreach (var room in possibleRooms)
        {
            if (!roomsByDifficulty.ContainsKey(room.Difficulty))
            {
                roomsByDifficulty.Add(room.Difficulty, new List<Room>());
            }
            roomsByDifficulty[room.Difficulty].Add(room);
        }

        foreach (var roomCount in roomAndCounts)
        {
            for (int i = 0; i < roomCount.Count; i++)
            {
                roomCountsNormal.Add(roomCount.Difficulty);
            }
        }
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

    Room GetNextRoomFromQueue()
    {
        if (roomQueue.Count == 0)
        {
            roomCountsNormal.Shuffle();
            foreach(var difficulty in roomCountsNormal)
            {
                roomQueue.Enqueue(difficulty);
            }
        }

        var chosenRooms = roomsByDifficulty[roomQueue.Dequeue()];

        return chosenRooms[UnityEngine.Random.Range(0, chosenRooms.Count)];
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
                currentRoom = Instantiate(GetNextRoomFromQueue());
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
