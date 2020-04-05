using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    List<Wall> leftWalls;

    [SerializeField]
    List<Wall> rightWalls;

    [SerializeField]
    List<Wall> topWalls;

    [SerializeField]
    List<Wall> bottomWalls;

    [SerializeField]
    bool leftOpen;

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
    bool rightOpen;

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
    bool topOpen;

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
    bool bottomOpen;

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
