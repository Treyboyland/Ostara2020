﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public static class DictionaryExtensions
{
    public static string AsMap(this Dictionary<Vector2Int, OpenDoors> grid)
    {
        //O(x*y)
        int minX = int.MaxValue;
        int maxX = int.MinValue;
        int maxY = int.MinValue;
        int minY = int.MaxValue;
        foreach (var key in grid.Keys)
        {
            minX = Mathf.Min(minX, key.x);
            minY = Mathf.Min(minY, key.y);
            maxX = Mathf.Max(maxX, key.x);
            maxY = Mathf.Max(maxY, key.y);
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("Map: (" + minX + ", " + minY + ") → (" + maxX + ", " + maxY + ")\r\n");
        for (int y = maxY; y >= minY; y--)
        {
            for (int x = minX; x <= maxX; x++)
            {
                var pos = new Vector2Int(x, y);

                if (!grid.ContainsKey(pos))
                {
                    sb.Append("█");
                }
                else
                {
                    sb.Append(grid[pos]);
                }
            }

            sb.Append("\r\n");
        }

        return sb.ToString();
    }

    public static string AsMap(this Dictionary<Vector2Int, OpenDoors> grid, Vector2Int goal)
    {
        //O(x*y)
        int minX = int.MaxValue;
        int maxX = int.MinValue;
        int maxY = int.MinValue;
        int minY = int.MaxValue;
        foreach (var key in grid.Keys)
        {
            minX = Mathf.Min(minX, key.x);
            minY = Mathf.Min(minY, key.y);
            maxX = Mathf.Max(maxX, key.x);
            maxY = Mathf.Max(maxY, key.y);
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("Map: (" + minX + ", " + minY + ") → (" + maxX + ", " + maxY + ")\r\n");
        for (int y = maxY; y >= minY; y--)
        {
            for (int x = minX; x <= maxX; x++)
            {
                var pos = new Vector2Int(x, y);

                if (!grid.ContainsKey(pos))
                {
                    sb.Append("█");
                }
                else if (pos == new Vector2Int())
                {
                    sb.Append("╳");
                }
                else if (pos == goal)
                {
                    sb.Append('╳');
                }
                else
                {
                    sb.Append(grid[pos]);
                }
            }

            sb.Append("\r\n");
        }

        return sb.ToString();
    }

    public static Vector2Int GetAddition(this Room.PlayerExit direction)
    {
        Vector2Int move = new Vector2Int(0, 0);
        if (direction == Room.PlayerExit.TOP)
        {
            move.y = 1;
        }
        else if (direction == Room.PlayerExit.BOTTOM)
        {
            move.y = -1;
        }
        else if (direction == Room.PlayerExit.LEFT)
        {
            move.x = -1;
        }
        else if (direction == Room.PlayerExit.RIGHT)
        {
            move.x = 1;
        }

        return move;
    }

    public static Vector2Int AddRoom(this Dictionary<Vector2Int, OpenDoors> grid, Vector2Int position, Room.PlayerExit direction)
    {
        if (!grid.ContainsKey(position))
        {
            OpenDoors doors = new OpenDoors();
            doors.OpenDoor(direction);
            grid.Add(position, doors);
        }
        else
        {
            var doors = grid[position];
            doors.OpenDoor(direction);
            grid[position] = doors;
        }

        var newPosition = position + GetAddition(direction);
        var oppositeDirection = direction.GetOppositeExit();

        if (!grid.ContainsKey(newPosition))
        {
            OpenDoors newDoors = new OpenDoors(oppositeDirection);
            grid.Add(newPosition, newDoors);
        }
        else
        {
            var doors = grid[newPosition];
            doors.OpenDoor(oppositeDirection);
            grid[newPosition] = doors;
        }

        //Debug.Log("" + position + ": " + direction + ": " + newPosition);
        return newPosition;
    }

    public static void Traverse(this Dictionary<Vector2Int, OpenDoors> grid, string path)
    {
        Vector2Int origin = new Vector2Int(0, 0);
        StringBuilder sb = new StringBuilder("Errors\r\n");
        foreach (var dir in path)
        {
            var exit = dir.GetExit();
            if (!grid.ContainsKey(origin))
            {
                sb.Append("Grid does not have " + origin + "\r\n");
            }
            else if (!grid[origin].IsOpen(exit))
            {
                sb.Append("" + origin + "Does not have open door: " + exit + "\r\n");
            }
            else if (!grid.ContainsKey(origin + exit.GetAddition()))
            {
                sb.Append("Connecting room: " + (origin + exit.GetAddition()) + " does not exist.\r\n");
            }
            else if (!grid[origin + exit.GetAddition()].IsOpen(exit.GetOppositeExit()))
            {
                sb.Append("Connecting room: " + (origin + exit.GetAddition()) + " does not connect with room " + origin + "\r\n");
            }

            origin += exit.GetAddition();
        }
        sb.Append("Path: " + path);
        Debug.Log(sb.ToString());
    }

    public static Vector2Int Adjust(this Vector2Int origin, string path)
    {
        Vector2Int toReturn = new Vector2Int(origin.x, origin.y);
        foreach (var dir in path)
        {
            toReturn = toReturn.Adjust(dir.GetExit());
        }

        return toReturn;
    }

    public static Vector2Int Adjust(this Vector2Int origin, Room.PlayerExit direction)
    {
        return origin + direction.GetAddition();
    }

    public static Vector2Int Adjust(this Vector2Int origin, List<Room.PlayerExit> path)
    {
        Vector2Int toReturn = new Vector2Int(origin.x, origin.y);
        foreach (var dir in path)
        {
            toReturn = toReturn.Adjust(dir);
        }
        return toReturn;
    }
}

public static class ExitExtensions
{
    public static Room.PlayerExit GetOppositeExit(this Room.PlayerExit exit)
    {
        switch (exit)
        {
            case Room.PlayerExit.TOP:
                return Room.PlayerExit.BOTTOM;
            case Room.PlayerExit.BOTTOM:
                return Room.PlayerExit.TOP;
            case Room.PlayerExit.LEFT:
                return Room.PlayerExit.RIGHT;
            case Room.PlayerExit.RIGHT:
                return Room.PlayerExit.LEFT;
            default:
                return Room.PlayerExit.TOP;
        }
    }

    public static char GetLetter(this Room.PlayerExit exit)
    {
        switch (exit)
        {
            case Room.PlayerExit.TOP:
                return 'U';
            case Room.PlayerExit.BOTTOM:
                return 'D';
            case Room.PlayerExit.LEFT:
                return 'L';
            case Room.PlayerExit.RIGHT:
                return 'R';
            default:
                return '*';
        }
    }

    public static Room.PlayerExit GetExit(this char dir)
    {
        switch (dir)
        {
            case 'U':
                return Room.PlayerExit.TOP;
            case 'D':
                return Room.PlayerExit.BOTTOM;
            case 'L':
                return Room.PlayerExit.LEFT;
            case 'R':
                return Room.PlayerExit.RIGHT;
            default:
                throw new Exception("invalid direction");
        }
    }

    public static List<Room.PlayerExit> GetOtherExits(this Room.PlayerExit exit)
    {
        //TODO: Return here if you add a NONE direction
        List<Room.PlayerExit> toReturn = new List<Room.PlayerExit>();
        foreach (var temp in (Room.PlayerExit[])Enum.GetValues(typeof(Room.PlayerExit)))
        {
            if (temp != exit)
            {
                toReturn.Add(temp);
            }
        }

        return toReturn;
    }

    public static List<Room.PlayerExit> GetMeaningfulExits(this Room.PlayerExit exit)
    {
        List<Room.PlayerExit> toReturn = new List<Room.PlayerExit>();
        var opposite = exit.GetOppositeExit();
        foreach (var temp in (Room.PlayerExit[])Enum.GetValues(typeof(Room.PlayerExit)))
        {
            if (temp != opposite)
            {
                toReturn.Add(temp);
            }
        }

        return toReturn;
    }



    public static string AsString(this List<Room.PlayerExit> list)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var dir in list)
        {
            sb.Append(dir.GetLetter());
        }

        return sb.ToString();
    }
}