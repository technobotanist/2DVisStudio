using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HoppyHankGameManager : MonoBehaviour
{
    public int count = 0;
    public int highScore = 0;

    public GameObject player;
    private Rigidbody2D rb;
    private HoppyController playerController;

    public TMP_Text score;

    public PipeCreator pipeCreator;

    private bool canRespawn;
    public float respawnDelay = 1f;

    private bool started = false;
    private bool lost = false;
    public GameObject GameScreen;
    public GameObject StartScreen;
    public GameObject LoseScreen;
    public TMP_Text loseScore;
    public TMP_Text loseHighScore;

    public AudioSource music;
    public AudioSource scoreSound;

    private void Start()
    {
        pipeCreator.SetStopped(true);
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<HoppyController>();
        rb = player.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        score.SetText("" + count);
        canRespawn = true;
    }

    private void Update()
    {
        if(canRespawn && Input.GetKeyDown(KeyCode.Space))
        {
            if(!started)
            {
                music.Play();
                pipeCreator.SetStopped(false);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                StartScreen.SetActive(false);
                started = true;
                GameScreen.SetActive(true);
            }
            else if(lost)
            {
                music.Play();
                GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
                foreach(GameObject pipe in pipes)
                {
                    Destroy(pipe);
                }
                pipeCreator.SetStopped(false);
                player.transform.position = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                player.transform.rotation = Quaternion.identity;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                LoseScreen.SetActive(false);
                lost = false;
                GameScreen.SetActive(true);
                score.SetText("" + count);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Lose()
    {
        if(!lost)
        {
            music.Pause();
            GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
            foreach (GameObject pipe in pipes)
            {
                pipe.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            pipeCreator.SetStopped(true);
            GameScreen.SetActive(false);
            LoseScreen.SetActive(true);
            lost = true;
            loseScore.SetText("Score: " + count);
            loseHighScore.SetText("High Score: " + highScore);
            count = 0;

            playerController.SetPlaying(false);
            canRespawn = false;
            StartCoroutine(WaitToRespawn());
        }
    }

    public void ScoreUp()
    {
        scoreSound.Stop();
        scoreSound.Play();
        count++;
        if (highScore < count)
        {
            highScore = count;
        }
        score.SetText("" + count);
        Debug.Log(count);
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        canRespawn = true;
        playerController.SetPlaying(true);
    }
}
