using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject songSelectionPanel;
    public GameObject gameplayElements;  
    public void OnStartButtonPressed()
    {
        mainMenuPanel.SetActive(false);  
        songSelectionPanel.SetActive(true);  
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void OnSongSelected()
    {
        songSelectionPanel.SetActive(false);  
        gameplayElements.SetActive(true); 
    }
}
