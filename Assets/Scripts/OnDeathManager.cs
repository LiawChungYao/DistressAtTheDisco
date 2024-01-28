using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDeathManager : MonoBehaviour
{
    public GameObject gameOverPanel; 
    public GameObject winningPanel; 
    private void Start()
    {
        gameOverPanel.SetActive(false); 
        winningPanel.SetActive(false);
    }

    public void ShowDeathUI()
    {
        gameOverPanel.SetActive(true); 
    }

    public void ShowWinningUI()
    {
        winningPanel.SetActive(true); 
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("StartScene"); 
    }

}
