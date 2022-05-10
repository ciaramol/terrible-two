using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject optionsDialogue;

    public void StartGame()
    {
        SceneManager.LoadScene("Indoor");
    }

    public void OpenOptions()
    {
        menu.SetActive(false);
        optionsDialogue.SetActive(true);
    }

    public void Return()
    {
        menu.SetActive(true);
        optionsDialogue.SetActive(false);
    }
}
