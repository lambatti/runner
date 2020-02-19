using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System.IO;

public class GameController : MonoBehaviour
{
    public int score = 0;

    private int bestScore;
    private string bestScoreName = "BestScore";

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI bestScoreText;

    public Rigidbody2D player;

    private Stopwatch sw;

    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt(bestScoreName, 0);
        PlayerPrefs.Save();
        UnityEngine.Debug.Log("Loaded");
    }

    // Start is called before the first frame update
    void Start()
    {
        bestScoreText.text = "Best score: " + bestScore;
        player.position = new Vector3(-5,0.5f,0);
        sw = new Stopwatch();
        sw.Start();
    }

    void FixedUpdate()
    {
        score = (int)sw.ElapsedMilliseconds / 100;
        scoreText.text = "Score: " + score;

        //if (score > bestScore)
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnApplicationQuit()
    {

    }

    public void Transition(Rigidbody2D player)
    {
        PlayerController.speed *= 2;
        player.position = new Vector3(25, player.position.y);
    }

    public void GameOver()
    {
        if (score > bestScore)
        {
            PlayerPrefs.SetInt(bestScoreName, score);
            PlayerPrefs.Save();
            UnityEngine.Debug.Log("Saved");
        }
        SceneManager.LoadScene("Menu",LoadSceneMode.Single);
    }



}