using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OpenDoors
{
    public bool Left;
    public bool Right;
    public bool Top;
    public bool Bottom;

    public OpenDoors(Room.PlayerExit exit)
    {
        //Doing multi-line assignment because I am lazy
        switch (exit)
        {
            case Room.PlayerExit.TOP:
                Top = true;
                Right = Left = Bottom = false;
                break;
            case Room.PlayerExit.BOTTOM:
                Bottom = true;
                Right = Left = Top = false;
                break;
            case Room.PlayerExit.LEFT:
                Left = true;
                Right = Top = Bottom = false;
                break;
            case Room.PlayerExit.RIGHT:
                Right = true;
                Top = Left = Bottom = false;
                break;
            default:
                Left = Right = Top = Bottom = false;
                break;
        }
    }

    public void OpenDoor(Room.PlayerExit door)
    {
        switch (door)
        {
            case Room.PlayerExit.TOP:
                Top = true;
                break;
            case Room.PlayerExit.BOTTOM:
                Bottom = true;
                break;
            case Room.PlayerExit.LEFT:
                Left = true;
                break;
            case Room.PlayerExit.RIGHT:
                Right = true;
                break;
            default:
                break;
        }
    }

    public bool IsOpen(Room.PlayerExit door)
    {
        switch (door)
        {
            case Room.PlayerExit.TOP:
                return Top;
            case Room.PlayerExit.BOTTOM:
                return Bottom;
            case Room.PlayerExit.LEFT:
                return Left;
            case Room.PlayerExit.RIGHT:
                return Right;
            default:
                return false;
        }
    }

    public override string ToString()
    {
        //Disgusting...No time to think of a better way
        if (Left && Right && Top && Bottom) //1111
        {
            return "╬";
        }
        else if (Left && Right && Top && !Bottom) //1110
        {
            return "╩";
        }
        else if (Left && Right && !Top && Bottom) //1101
        {
            return "╦";
        }
        else if (Left && Right && !Top && !Bottom) //1100
        {
            return "═";
        }
        else if (Left && !Right && Top && Bottom) //1011
        {
            return "╣";
        }
        else if (Left && !Right && Top && !Bottom) //1010
        {
            return "╝";
        }
        else if (Left && !Right && !Top && Bottom) //1001
        {
            return "╗";
        }
        else if (Left && !Right && !Top && !Bottom) //1000
        {
            return "╡";
        }
        else if (!Left && Right && Top && Bottom) //0111
        {
            return "╠";
        }
        else if (!Left && Right && Top && !Bottom) //0110
        {
            return "╚";
        }
        else if (!Left && Right && !Top && Bottom) //0101
        {
            return "╔";
        }
        else if (!Left && Right && !Top && !Bottom) //0100
        {
            return "╞";
        }
        else if (!Left && !Right && Top && Bottom) //0011
        {
            return "║";
        }
        else if (!Left && !Right && Top && !Bottom) //0010
        {
            return "╨";
        }
        else if (!Left && !Right && !Top && Bottom) //0001
        {
            return "╥";
        }
        else //0000
        {
            return "W";
        }
    }
}
