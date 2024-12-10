using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject StartGameButton;
    public GameObject DisplayControls;

    public void Start()
    {
        DisplayControls.SetActive(false);// turn off controls
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGameButton.SetActive(false); // close main menu
            DisplayControls.SetActive(true); //open controls
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlayLoop"); //load game scene

        }


    }
}
