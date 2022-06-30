using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HanksHeistGameManager : MonoBehaviour
{
    public int count = 0;
    
    public GameObject player;
    private Rigidbody2D rb;
    private HeistController playerController;

    public HeistCameraMove cameraMove;

    private bool started = false;
    public GameObject GameScreen;
    public GameObject StartScreen;
    public GameObject WinScreen;

    public AudioSource music;
    public AudioSource scoreSound;
    public AudioSource winSound;
    private bool canContinue = false;
    public float timeBeforeContinue = 3f;

    private bool won = false;

    private GameObject[] bricks;

    public Image[] brickImages;

    private void Start()
    {
        playerController = player.GetComponent<HeistController>();
        rb = player.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        bricks = GameObject.FindGameObjectsWithTag("Brick");
    }

    private void Update()
    {
        if(!started && Input.GetAxis("Vertical") < -0.1f)
        {
            Debug.Log("Start");
            music.Play();
            StartScreen.SetActive(false);
            started = true;
            GameScreen.SetActive(true);
            cameraMove.MoveDown();
        }

        bool allBricksFound = AllBricksFound();

        if(allBricksFound)
        {
            Win();
        }

        if(canContinue && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private bool AllBricksFound()
    {
        foreach (GameObject brick in bricks)
        {
            if (!brick.GetComponent<BrickController>().GetFound())
            {
                return false;
            }
        }

        return true;
    }

    public void Win()
    {
        if(!won)
        {
            won = true;
            music.Pause();
            winSound.Play();
            playerController.SetPlaying(false);
            WinScreen.SetActive(true);

            cameraMove.MoveUp();

            count = 0;
        }
    }

    public void ScoreUp()
    {
        scoreSound.Stop();
        scoreSound.Play();
        brickImages[count].enabled = true;
        count++;
        Debug.Log(count);
    }

    IEnumerator SetToContinue()
    {
        yield return new WaitForSeconds(timeBeforeContinue);
        canContinue = true;
    }
}
