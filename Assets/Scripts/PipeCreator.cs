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

    public float startingOffset = 2.0f;
    public float step = 0.1f;
    private float offset;

    // Update is called once per frame
    void Update()
    {
        if(!stopped && canSpawn)
        {
            canSpawn = false;
            StartCoroutine(CreatePipe());
        }
        else if(stopped)
        {
            offset = startingOffset;
        }
    }

    IEnumerator CreatePipe()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, Random.Range(-3.5f + offset, 3.5f - offset), 0f);
        var pipeObject = Instantiate(pipes, spawnPoint, Quaternion.identity);

        Transform[] childPipes = pipeObject.gameObject.transform.GetComponentsInChildren<Transform>();

        foreach(Transform child in childPipes)
        {
            if(child.gameObject.name == "TopPipe")
            {
                child.position = new Vector2(child.position.x, child.position.y + offset);
            }
            else if(child.gameObject.name == "BottomPipe")
            {
                child.position = new Vector2(child.position.x, child.position.y - offset);
            }
        }

        pipeObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-pipeSpeed, 0f);
        Debug.Log(pipeObject.GetComponent<Rigidbody2D>().velocity);
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
        
        if(offset > 0)
        {
            offset -= step;
            if(offset < 0)
            {
                offset = 0;
            }
        }
    }

    public void SetStopped(bool isStopped)
    {
        stopped = isStopped;
    }
}
