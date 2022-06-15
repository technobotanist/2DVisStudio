using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCreator : MonoBehaviour
{
    public float spawnInterval = 2f;
    private bool canSpawn = true;

    public GameObject pipes;
    public float pipeSpeed = 200f;
    private bool stopped = false;

    // Update is called once per frame
    void Update()
    {
        if(!stopped && canSpawn)
        {
            canSpawn = false;
            StartCoroutine(CreatePipe());
        }
    }

    IEnumerator CreatePipe()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, Random.Range(-3.25f, 3.25f), 0f);
        var pipeObject = Instantiate(pipes, spawnPoint, Quaternion.identity);
        pipeObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-pipeSpeed, 0f);
        Debug.Log(pipeObject.GetComponent<Rigidbody2D>().velocity);
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
    }

    public void SetStopped(bool isStopped)
    {
        stopped = isStopped;
    }
}
