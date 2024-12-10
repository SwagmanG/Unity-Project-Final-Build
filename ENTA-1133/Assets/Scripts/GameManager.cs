using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    //field to link mapmanager to gamemanager
    [SerializeField] private MapManager GameMapPrefab;

    public int swagVariable;
    private MapManager _gameMap;
    [SerializeField] private PlayerController PlayerPrefab;
    private PlayerController _playerController;
   

    public void Start()
    {
        Debug.Log("GameManager Start");
        
        SetupMap();
        SpawnPlayer();
        StartGame();
        PlayerSetup();


    }

    private void SetupMap()
    {
        // zero our manager position
        transform.position = Vector3.zero;

        // create an instance of the map manager
        _gameMap = Instantiate(GameMapPrefab, transform);
        _gameMap.transform.position = Vector3.zero;
        // create the map
        _gameMap.CreateMap();

       
        Debug.Log("GameManager Map Created");
    }
    private void StartGame()
    {
        Debug.Log("GameManager StartGame Begins");
        Debug.Log("GameManager StartGame Complete");
    }

     //ASK NOAH ABOUT THE KEYS THING

    private void SpawnPlayer()
    {
        // Intro
        Debug.Log("GameManager SpawnPlayer Begins");
        // Pick a random starting room - this must be done after map is made
        //var randomStartingoom = _playerController._rotationByDirection.ElementAt(Random.Range(0, _playerController._rotationByDirection.Keys.Count));
        // Create the player
        _playerController = Instantiate(PlayerPrefab, transform);
        // Set their initial position
        _playerController.transform.position = new Vector3(7.259f,1,7.391f); // hard coded to spawn in the middle of the rooms.
        _playerController.Setup();
        Debug.Log("GameManager SpawnPlayer Complete");
    }

    private void PlayerSetup() // referencing the layer after creation
    {
        _gameMap.SetManager(this, _playerController);
    }
}