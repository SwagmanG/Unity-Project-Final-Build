using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager 
{
    // todo make them all lambdo like north
    private RoomManager north, East, South, West;
    public RoomManager North => north;
    private int roomNumber;

    public RoomManager(int num)
    {
        roomNumber = num;
        Console.WriteLine("Room " + num + " Created!");
    }

    public void SetRooms(RoomManager roomToTheNorth, RoomManager east, RoomManager south, RoomManager west)
    {
        north = roomToTheNorth;
        East = east;
        South = south;
        West = west;
    }
}