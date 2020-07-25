using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    RoomDifficulty roomDifficulty = RoomDifficulty.TRIVIAL;

    public RoomDifficulty Difficulty
    {
        get
        {
            return roomDifficulty;
        }
    }

    [SerializeField]
    List<Wall> leftWalls = null;

    [SerializeField]
    List<Wall> rightWalls = null;

    [SerializeField]
    List<Wall> topWalls = null;

    [SerializeField]
    List<Wall> bottomWalls = null;

    [SerializeField]
    bool leftOpen = false;

    public bool LeftOpen
    {
        get
        {
            return leftOpen;
        }
        set
        {
            leftOpen = value;
        }
    }

    [SerializeField]
    bool rightOpen = false;

    public bool RightOpen
    {
        get
        {
            return rightOpen;
        }
        set
        {
            rightOpen = value;
        }
    }

    [SerializeField]
    bool topOpen = false;

    public bool TopOpen
    {
        get
        {
            return topOpen;
        }
        set
        {
            topOpen = value;
        }
    }

    [SerializeField]
    bool bottomOpen = false;

    public bool BottomOpen
    {
        get
        {
            return bottomOpen;
        }
        set
        {
            bottomOpen = value;
        }
    }


    public enum PlayerExit
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT
    }

    public enum RoomDifficulty
    {
        /// <summary>
        /// Rooms with little to no obstacles (e.g. an empty room)
        /// </summary>
        /// <returns></returns>
        TRIVIAL,
        /// <summary>
        /// Rooms with perhaps one obstacle
        /// </summary>
        /// <returns></returns>
        EASY,
        /// <summary>
        /// Rooms with a few obstacles that the "avarage" player should
        /// be able to navigate with little difficulty
        /// </summary>
        /// <returns></returns>
        NORMAL,
        /// <summary>
        /// Rooms with perhaps a lot of obstacles, 
        /// </summary>
        /// <returns></returns>
        HARD,
        /// <summary>
        /// A room that truly tests the player's mastery of the game.
        /// Riddled with enemies, traps, or obstacles.
        /// </summary>
        /// <returns></returns>
        EXTREME,
        /// <summary>
        /// A distinction for rooms that have interesting things inside of them
        /// that might not fit into any of the other categories
        /// </summary>
        /// <returns></returns>
        SPECIAL,
    }

    public PlayerExitEvent OnPlayerExit = new PlayerExitEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        CheckDoors();
    }

    private void OnValidate()
    {
        CheckDoors();
    }

    public void SetDoors(OpenDoors openDoors)
    {
        LeftOpen = openDoors.Left;
        RightOpen = openDoors.Right;
        TopOpen = openDoors.Top;
        BottomOpen = openDoors.Bottom;

        CheckDoors();
    }

    void CheckDoors()
    {
        SetWalls(leftWalls, LeftOpen);
        SetWalls(rightWalls, RightOpen);
        SetWalls(topWalls, TopOpen);
        SetWalls(bottomWalls, BottomOpen);
    }

    void SetWalls(List<Wall> walls, bool open)
    {
        foreach (var wall in walls)
        {
            wall.SetOpen(open);
        }
    }

    public void InvokePlayerExit(PlayerExit exit)
    {
        OnPlayerExit.Invoke(exit);
    }
}
