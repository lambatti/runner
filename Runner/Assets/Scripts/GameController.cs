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

    private Stopwatch sw;

    // Start is called before the first frame update
    void Start()
    {
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