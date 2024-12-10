using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomBase : MonoBehaviour
{
    [SerializeField] public GameObject NorthDoorway, EastDoorway, SouthDoorway, WestDoorway; 

    public GameManager GManager;
    public PlayerController PController;

    public void SetManager(GameManager newGManager, PlayerController newPController)// referencing gamemanager and player controller
    {
        GManager = newGManager; //setting to new instance
        PController = newPController; //setting to new instance
    }

    public void SetRoomLocation(Vector2 coordinates)
    {   // Sets the room locations
        transform.position = new Vector3(coordinates.x, 0, coordinates.y);
    }

    public virtual void OnRoomSearched()
    {

    }

    public virtual void Update()
    {

    }

}
