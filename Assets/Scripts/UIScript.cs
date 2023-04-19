using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    public bool gameStarted = false;
    [SerializeField]
    private GameObject menuScreen;
    [SerializeField]
    private GameObject inGameScreen;

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
        menuScreen.SetActive(false);
        inGameScreen.SetActive(true);
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
