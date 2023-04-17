using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    public bool gameStarted = false;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Button tapToPlay;

    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("UpdateScore", 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void PlayGame()
    {
        scoreText.gameObject.SetActive(true);
        title.gameObject.SetActive(false);
        tapToPlay.gameObject.SetActive(false);
        gameStarted = true;
    }

    private void UpdateScore()
    {
        if(gameStarted == true)
        {
            score++;
            scoreText.text = "" + score;
        }
    }

}
