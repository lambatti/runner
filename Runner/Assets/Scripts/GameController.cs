using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class GameController : MonoBehaviour
{
    public long score;

    public TextMeshProUGUI scoreText;

    public Rigidbody2D player;

    private Stopwatch sw;

    // Start is called before the first frame update
    void Start()
    {
        player.position = new Vector3(-5,0.5f,0);
        score = 0;
        sw = new Stopwatch();
        sw.Start();
    }

    void FixedUpdate()
    {
        score = sw.ElapsedMilliseconds / 100;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {

    }
}