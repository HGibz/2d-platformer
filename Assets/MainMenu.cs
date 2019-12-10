using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button newStartGameButton;

    public Button newControlsButton;


    public Button newInfoButton;

    public string newGameSceneName;

    public string newControlSceneName;



    public void StartGame()
    {
        SceneManager.LoadScene(newGameSceneName);
    }

    public void ControlsButton()
    {
        SceneManager.LoadScene(newControlSceneName);
    }


    public void InfoButton()
    {

    }
}
