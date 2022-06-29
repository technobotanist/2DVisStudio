using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HurdlingHankGameManager : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private HurdlingController playerController;

    public TMP_Text time;
    private bool timeStart = false;
    public int maxTime = 90;
    private int timeLeft;
    private bool waitForTime = false;

    public ObstacleCreator obstacleCreator;
    private bool spawnedFinish = false;

    private bool canRespawn;
    public float respawnDelay = 1f;

    private bool started = false;
    private bool lost = false;
    private bool won = false;
    public GameObject GameScreen;
    public GameObject StartScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public TMP_Text loseScore;
    public TMP_Text loseHighScore;

    public AudioSource music;
    public AudioSource winSound;

    private void Start()
    {
        obstacleCreator.SetStopped(true);
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<HurdlingController>();
        rb = player.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        canRespawn = true;
        timeLeft = maxTime;
    }

    private void Update()
    {
        if (canRespawn && (Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.UpArrow)))
        {
            if (!started)
            {
                music.Play();
                obstacleCreator.SetStopped(false);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                StartScreen.SetActive(false);
                started = true;
                GameScreen.SetActive(true);
                timeStart = true;
                time.SetText("" + timeLeft);
                playerController.SetTargetPosition(player.transform.position);
            }
            else if (lost | won)
            {
                music.Play();
                lost = false;
                won = false;
                GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
                foreach (GameObject obstacle in obstacles)
                {
                    Destroy(obstacle);
                }
                obstacleCreator.SetStopped(false);
                player.transform.position = new Vector2(0, 0.625f);
                playerController.SetTargetPosition(player.transform.position);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                player.transform.rotation = Quaternion.identity;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                LoseScreen.SetActive(false);
                WinScreen.SetActive(false);
                GameScreen.SetActive(true);
                timeStart = true;
                waitForTime = false;
                time.SetText("" + timeLeft);
                spawnedFinish = false;
            }
        }

        if (timeLeft == 0 && !spawnedFinish)
        {
            spawnedFinish = true;
            waitForTime = true;
            obstacleCreator.SpawnWinLine();
        }

        if (timeStart && !waitForTime)
        {
            waitForTime = true;
            StartCoroutine(WaitOneSecond());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1);
        timeLeft -= 1;
        time.SetText("" + timeLeft);
        waitForTime = false;
    }

    public void Lose()
    {
        if (!lost)
        {
            music.Pause();
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obstacle in obstacles)
            {
                obstacle.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            obstacleCreator.SetStopped(true);
            GameScreen.SetActive(false);
            LoseScreen.SetActive(true);
            lost = true;

            loseScore.SetText("Time Left: " + timeLeft);

            timeStart = false;
            timeLeft = maxTime;
            playerController.SetPlaying(false);
            canRespawn = false;
            StartCoroutine(WaitToRespawn());
        }
    }

    public void Win()
    {
        if(!won)
        {
            music.Pause();
            winSound.Play();
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obstacle in obstacles)
            {
                obstacle.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            obstacleCreator.SetStopped(true);
            GameScreen.SetActive(false);
            WinScreen.SetActive(true);
            won = true;

            timeStart = false;
            timeLeft = maxTime;
            playerController.SetPlaying(false);
            canRespawn = false;
            StartCoroutine(WaitToRespawn());
        }
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        canRespawn = true;
        playerController.SetPlaying(true);
    }
}
