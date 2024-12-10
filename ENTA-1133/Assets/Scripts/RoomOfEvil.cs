using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class RoomOfEvil : RoomBase
{
    public GameObject Opponent;
    public GameObject ScreenText;
    public GameObject SpiritAngered;
    public GameObject FightButtons;
    public GameObject ScatterRound;
    public GameObject ExplosiveRound;
    public GameObject BasicRound;
    [SerializeField] private Text _healthDisplayText;
    

    List<int> BulletBase = new List<int>();
    private int _enemyDamageHealth = 4;//enemy damage
    private int _eHealth = 20;//enemy health
    private bool _hasBaRounds = true;// checks if the player has basic bullets, if treasure room is not implemented, will always be true
    public float UITimer = 0.0f;   // timer variable for when player enters room
    private bool _startTimer = false; // timer bool for player enter room
    private bool _combatRoomSearch = false; // checks for if in the combat room, wopnt really matter until code in other rooms implemented
    private bool _enemyAlive = true; //checks for enemy alive in a room
    private string _playerHealth; // for displaying player healthbar in game
    private int _playerMaxHealth = 20; // Player Max health
    private int _playerHealthDisplay = 20; //player health to check in health bar
   





    private void Start()
    {
        SpiritAngered.SetActive(false);//sets the flavor text after search to be not visible

        int minDamage = 5;//min damage for basic bullet
        int maxDamage = 5;//max damage for basic bullet
        
        //adds them to the item
        BulletBase.Add(minDamage);
        BulletBase.Add(maxDamage);
    }
    public override void Update()
    {
        if (_hasBaRounds) //checks if the player has basic rounds, always yes, unless treasure room is working.
        {
            BasicRound.SetActive(true);// enables the text info for basic bullets

            if (Input.GetKeyDown(KeyCode.Z))// calls functions when z key pressed
            {
                DamageCalc(); //calculates damage done by player
                
                EnemyDamagePlayer();//calculates damage done to player
            }
        }
       
        if (_startTimer) //timer for displaying text when entering combat room.
        {
            UITimer -= Time.deltaTime;
            if (UITimer <= 0.0f)
            {
                TimerEnded();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.I)) // press I to skip spirit angered text after search
        { 
            SpiritAngered.SetActive(false); 
            FightButtons.SetActive(true);
           
        }       
    }
    private void TimerEnded() //runs code to disable text for after the display text timer ends.
    {
        ScreenText.SetActive(false);
        if (UITimer >= 0.0f)
        {
            UITimer = 0.0f;
            _startTimer = false;
        }
    }   
    private void OnTriggerEnter(Collider otherObject) 
    {
        //referencing player to roombase to make sure that the player hitbox is detected
        var player = otherObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ERoom = this;
        }
        
        //checks if the enemy is alive if true display presence text and setting the timer to run
        if (_enemyAlive)
        {
            ScreenText.SetActive(true);
            UITimer = 3.0f;
            _startTimer = true;
        }
        _eHealth = 20; //setting enemy health
        _playerHealthDisplay = 20; //setting player health
        CurrentHp(); //runs health bar display code

    }
    private void OnTriggerStay(Collider otherObject)
    {
        
        _combatRoomSearch = true; // checks if player is in the combat room when searching,  use if treasure room works.
       
        if (_eHealth <= 0) //checks for the enemy health then disables enemy when it dies
        {
            Opponent.SetActive(false);
            _enemyAlive = false;
        }
        
        CurrentHp();// keeps player health updated 






    }
    private void OnTriggerExit(Collider otherObject)
    {
        //referencing player to roombase to make sure that the player hitbox is detected
        var player = otherObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ERoom = null;
        }
        
        _combatRoomSearch = false;// sets the search to false
        _playerHealthDisplay = 20; //resets player health, remove if healing items are in game
        CurrentHp();//updates health display
        ResetText();//turns health display off when leaving combat room               
    }
    public override void OnRoomSearched()
    {
        if (_combatRoomSearch) //checks for the value of combat room searched
        {   if (_enemyAlive)
            {
                SpiritAngered.SetActive(true); //displays spirit anger text
            }            
        } 
    }
    private void KillEnemy()
    {
        Opponent.SetActive(false);//when enemy dies, turn them off
        _enemyAlive = false; // sets enemyAlive to false       
    }
    private void DamageCalc()
    {
        int damageRange = UnityEngine.Random.Range(BulletBase[0], BulletBase[1]); //gets random value between min and max damage of basic bullet
        
        _eHealth -= damageRange; //subtracts bullet damage from enemy health                
    }  
    private void EnemyDamagePlayer()
    {        
        //Damage the player after they use their item to shoot.

        //(Player damage doesnt work, cant get a game over after losing health)
    
        PController._pHealth -= _enemyDamageHealth;
        _playerHealthDisplay -= _enemyDamageHealth;                      
    }
    public void CurrentHp()
    {
        //code to display players current health
        _playerHealth = $"Health: {_playerHealthDisplay} / {_playerMaxHealth}";
        _healthDisplayText.text = _playerHealth;     
    }
    public void ResetText()
    {
        //code to disable player healthbar upon leaving the combat room
        _healthDisplayText.text = "";
    }      
}
