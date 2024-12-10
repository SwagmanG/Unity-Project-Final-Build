using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UIManager UIManager;
    public RoomBase ERoom;
    public GameManager GManager;
    //public PlayerController PController;

    public Dictionary<Direction, int> _rotationByDirection = new()
    {
       
        { Direction.North, 0 }, // also 360
        { Direction.East, 90 },
        { Direction.South, 180 },
        { Direction.West, 270 }
    };

    private Direction _facingDirection;//enum for 4 directions
    private bool _isRotating = false; //bool for rotating
    private bool _isWalking = false;// bool for walking
    // Smooth rotation, and walking
    private float _distanceToWalk = 3.0f;//distance to walk
    private float _rotateTime = 0.5f; //rotate time limit
    private float _walkTime = 2.0f; // walk time limit
    private float _rotateTimer = 0.0f;// rotate timer
    private float _walkTimer = 0.0f;// walk timer
    public int _pHealth = 700;// player health

    
   
    private Quaternion _previousRotation;
    private Vector3 _previousPosition;
    SearchMenu PSearchReference;
    private void Start()
    {
        PSearchReference = GameObject.FindAnyObjectByType<SearchMenu>(); // referencing player
        //ERoom.SetManager(GManager, PController);
        _pHealth = 700; // setting health on start
    }
    public void Setup()
    {
        // Simple array of all directions
        Direction[] directions = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West };
        //roll a random direction
        _facingDirection = directions[UnityEngine.Random.Range(0, directions.Length)];
        // Update the transform
        SetFacingDirection();        
    }

    private void SetFacingDirection()
    {
        // Note: transform.rotation is type "Quaternion", we hate working with these, and i mean straight up despise
        // Get the transorm's rotation, use eulerAnglers for easier math (Vector3)
        Vector3 facing = transform.rotation.eulerAngles;
        // Set the y value
        facing.y = _rotationByDirection[_facingDirection];
        // Save the rotation back, converting it to a Quaternion first
        transform.rotation = Quaternion.Euler(facing);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && ERoom != null) // checking for key pressed and chekcingg if eroom is not null
        {
            
            ERoom.OnRoomSearched();//call the onsearched fucntion
            ERoom.Update(); //call the update function
        }

        if (_isRotating)
        {
            // Continue the movement until it is finished
            //Qyaternion.Lerp for linear movement, Quanternion.Slerp for smoothed movement
            Quaternion currentRotation = Quaternion.Slerp(
                _previousRotation,
                Quaternion.Euler(new Vector3(0, _rotationByDirection[_facingDirection])),
                _rotateTimer / _rotateTime);

            transform.rotation = currentRotation;
            _rotateTimer += Time.deltaTime;
            if (_rotateTimer > _rotateTime)
            {
                _isRotating = false;
                _rotateTimer = 0.0f;
                SetFacingDirection(); // snap to final rotation
            }
        }
        else if (_isWalking)
        {   
            // Setting the endPosition to where the player will be after moving
            Vector3 endPosition = _distanceToWalk * transform.forward + _previousPosition;
            // Vector3.Lerp controls the smooth transtion between _previousPostion, and the endPosition
            // _walkTimer / _walkTime, tells code what percentage of the way through the movement the player is
            Vector3 currentPosition = Vector3.Lerp(_previousPosition,
                endPosition,
                _walkTimer / _walkTime);
            //telling player to move a certain amount
            transform.localPosition = currentPosition;
            //timer 
            _walkTimer += Time.deltaTime;

            //setting isWalking to false, and telling _walkTimer, to not exceed _walkTime
            if(_walkTimer > _walkTime)
            {
                _isWalking = false;
                _walkTimer = 0;
                //Snaps player to stop position after the lerp
                transform.localPosition = endPosition;
            }
        }
       
        else
        {
            // GetKeyDown is per-press basis. "was the button pressed *this* frame?"
            bool rotateLeft = Input.GetKeyDown(KeyCode.A);
            bool rotateRight = Input.GetKeyDown(KeyCode.D);
            //checking if the player pressed W
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartWalking();
            }
            // ensure one or the other is true, not both
            if (rotateLeft && !rotateRight)
            {
                TurnLeft();
            }
            if (!rotateLeft && rotateRight)
            {
                TurnRight();
            }
        }

      

    }
    private void TurnLeft()
    {
        switch (_facingDirection)// swithc case to turn left
        {
            case Direction.South:
                _facingDirection = Direction.East;
            break;
            case Direction.North:
                _facingDirection = Direction.West;
            break;
            case Direction.East:
                _facingDirection = Direction.North;
            break;
            case Direction.West:
                _facingDirection = Direction.South;
            break;
        }
        StartRotating();
    }
    //clockwise
    private void TurnRight()
    {
        switch (_facingDirection)// switch case to turn right
        {
            case Direction.South:
                _facingDirection = Direction.West;
                break;
            case Direction.North:
                _facingDirection = Direction.East;
                break;
            case Direction.East:
                _facingDirection = Direction.South;
                break;
            case Direction.West:
                _facingDirection = Direction.North;
            break;
        }
        StartRotating();
    }
    
    private void StartWalking()
    {
        //assigning _previousPosition to the local position
        _previousPosition = transform.localPosition;
        //Setting _isWalking to true while the player is moving.
        _isWalking = true;
    }
    private void StartRotating() // rotates the player
    {
        _previousRotation = transform.rotation;
        _isRotating = true;
    }

   


    
}
