using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float score;

    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    void FixedUpdate()
    {
        score += Time.deltaTime*10;
        scoreText.text = "Score: " + (int)score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
