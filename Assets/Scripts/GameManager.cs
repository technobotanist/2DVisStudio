using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int count = 0;
    public int highScore = 0;

    public GameObject player;

    public TMP_Text score;

    private bool playing = false;
    private bool started = false;
    private bool lost = false;
    public GameObject GameScreen;
    public GameObject StartScreen;
    public GameObject LoseScreen;
    public TMP_Text loseScore;
    public TMP_Text loseHighScore;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        score.SetText("" + count);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!started)
            {
                StartScreen.SetActive(false);
                Time.timeScale = 1;
                started = true;
                playing = true;
                GameScreen.SetActive(true);
            }
            else if(lost)
            {
                GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
                foreach(GameObject pipe in pipes)
                {
                    Destroy(pipe);
                }
                player.transform.position = Vector2.zero;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                LoseScreen.SetActive(false);
                Time.timeScale = 1;
                lost = false;
                playing = true;
                GameScreen.SetActive(true);
                score.SetText("" + count);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Lose()
    {
        GameScreen.SetActive(false);
        LoseScreen.SetActive(true);
        lost = true;
        playing = false;
        loseScore.SetText("Score: " + count);
        loseHighScore.SetText("High Score: " + highScore);
        count = 0;
        Time.timeScale = 0;
    }

    public void ScoreUp()
    {
        count++;
        if (highScore < count)
        {
            highScore = count;
        }
        score.SetText("" + count);
        Debug.Log(count);
    }
}
