using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public enum Direction
    {
        NORTH, SOUTH, EAST, WEST, NORTH_SOUTH, NORTH_EAST, ALL
    }

    public List<Direction> Directions = new List<Direction>() { Direction.ALL };
}
