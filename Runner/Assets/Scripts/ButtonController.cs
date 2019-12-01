using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
}
