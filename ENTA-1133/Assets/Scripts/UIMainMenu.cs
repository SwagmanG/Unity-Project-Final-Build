using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{

    private bool _isPaused = false;
    public GameObject PauseMenu;
    public GameObject MenuButtons;
    public GameObject SearchOptions;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
    }
    private void PauseGame()
    {
        PauseMenu.SetActive(true);
        _isPaused = true;
        Time.timeScale = 0.0f;
    }
    private void ResumeGame()
    {
        PauseMenu.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void SearchMenuActive()
    {
        Debug.LogWarning("Got here");
        SearchOptions.SetActive(true);
    }
}