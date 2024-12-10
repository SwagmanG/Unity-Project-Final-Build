using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    
    private bool _isPaused = false;//boolean to check if pause menuu is active
    public GameObject PauseMenu;
    public GameObject MenuButtons;
   

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);// sets pause menu off by default
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))// checks fior the p button press, and pauses or unpaused the game
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.Quit();
        }
    }
    private void PauseGame()//code to pause game
    {
        PauseMenu.SetActive(true);
        _isPaused = true;
        Time.timeScale = 0.0f;   
    }
    private void ResumeGame()//code to resume game
    {
        PauseMenu.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1.0f;
    }    
}

