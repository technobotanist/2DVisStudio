using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    public float spawnInterval = 2f;
    public float intervalStep = 0.025f;
    public float minInterval = 1f;
    private float interval;
    private bool canSpawn = true;

    public GameObject[] obstacles;
    public GameObject finishLine;
    public float obstacleSpeed = 20f;
    public float speedStep = 1f;
    public float maxSpeed = 35f;
    private float speed;
    private bool stopped = false;

    private bool won = false;

    private float[] yValues = { -4.375f, -3.125f, -1.875f, -.625f, .625f, 1.875f, 3.125f, 4.375f };

    // Update is called once per frame
    void Update()
    {
        if (!stopped && canSpawn)
        {
            canSpawn = false;

            if (won)
            {
                won = false;
                Vector3 spawnPoint = new Vector3(transform.position.x, -2.33f, 0f);
                var obstacleObject = Instantiate(finishLine, spawnPoint, Quaternion.identity);
                obstacleObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);
                StartCoroutine(FinishWait());
            }
            else
            {
                StartCoroutine(CreateObstacles());
            }
        }
        else if(stopped)
        {
            interval = spawnInterval;
            speed = obstacleSpeed;
        }
    }

    IEnumerator CreateObstacles()
    {
        int noSpawn = Random.Range(0, 8);

        for(int i = 0; i <= 7; i++)
        {
            if(i != noSpawn && Random.Range(0, 7) != 1)
            {
                Vector3 spawnPoint = new Vector3(transform.position.x, yValues[i], 0f);
                var obstacleObject = Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPoint, Quaternion.identity);
                obstacleObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);
            }
        }

        if(speed < maxSpeed)
        {
            speed += speedStep;
            if(speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        else if(interval > minInterval)
        {
            interval -= intervalStep;
            if(interval < minInterval)
            {
                interval = minInterval;
            }
        }

        yield return new WaitForSeconds(interval);
        canSpawn = true;
    }

    IEnumerator FinishWait()
    {
        yield return new WaitForSeconds(spawnInterval + interval);
        canSpawn = true;
    }

    public void SetStopped(bool isStopped)
    {
        stopped = isStopped;
    }

    public void SpawnWinLine()
    {
        won = true;
    }
}
