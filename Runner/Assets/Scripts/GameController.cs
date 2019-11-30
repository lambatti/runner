using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        score = (int)Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
