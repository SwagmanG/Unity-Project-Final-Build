using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Serializeing fields, and initiating and assigning variables
    [SerializeField] private RoomBase _startingRoom;
    [SerializeField] private RoomBase[] _roomPrefabs;
    [SerializeField] private float _roomSize = 1;
    public int _mapSize = 1;
    public RoomBase[,] Rooms;

    
    public PlayerController PController;
    public GameManager GManager;

    public void SetManager(GameManager newGManager, PlayerController newPController)
    {
        GManager = newGManager;
        PController = newPController;
    }

    public void CreateMap()
    {
        Rooms = new RoomBase[_mapSize,_mapSize];
        // Code to create the map size, and to place a room at each coordinate 
        for (int x = 0; x < _mapSize; x++)
        {
            for (int z = 0; z < _mapSize; z++)
            {
                Vector2 coordinates = new Vector2(x * _roomSize, z * _roomSize);

                var roomInstance = Instantiate(_roomPrefabs[Random.Range(0, _roomPrefabs.Length)]);

                roomInstance.SetRoomLocation(coordinates);
                roomInstance.SetManager(GManager, PController);

                // Code to check if there is an adjacent room, and to open a door if there is
                if(x > 0)
                {
                    roomInstance.WestDoorway.SetActive(false);
                }
                if (x < _mapSize - 1)
                {
                    roomInstance.EastDoorway.SetActive(false);
                }
                if (z > 0)
                {
                    roomInstance.SouthDoorway.SetActive(false);
                }
                if (z < _mapSize - 1)
                {
                    roomInstance.NorthDoorway.SetActive(false);
                }                           
            }
        }
    }
}