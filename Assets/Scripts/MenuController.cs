using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour

{
    public GameObject menuPanel;
    public GameObject instructionsPanel;

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }


    private void Start()
    {
        ShowMenu();
    }

    public void ShowInstructions()
    {
        menuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void ShowMenu()
    {
        instructionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }


}
