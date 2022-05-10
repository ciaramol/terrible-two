using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject startButton;
    public GameObject optionsButton;
    public GameObject title;
    public GameObject optionsDialogue;

    public void StartGame()
    {
        SceneManager.LoadScene("Indoor");
    }

    public void OpenOptions()
    {
        startButton.SetActive(false);
        optionsButton.SetActive(false);
        title.SetActive(false);
        optionsDialogue.SetActive(true);
    }

    public void Return()
    {
        startButton.SetActive(true);
        optionsButton.SetActive(true);
        title.SetActive(true);
        optionsDialogue.SetActive(false);
    }
}
