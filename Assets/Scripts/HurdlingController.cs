using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdlingController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool playing = true;
    private float travelDistance = 1.25f;
    private float minPosition = -5f;
    private float maxPosition = 5f;

    private bool moving = false;
    public float moveStep = 0.25f;
    private Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playing)
        {
            if (!moving)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (!(transform.position.y - travelDistance < minPosition))
                    {
                        moving = true;
                        targetPosition = new Vector2(transform.position.x, transform.position.y - travelDistance);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (!(transform.position.y + travelDistance > maxPosition))
                    {
                        moving = true;
                        targetPosition = new Vector2(transform.position.x, transform.position.y + travelDistance);
                    }
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveStep);
                if (transform.position.y == targetPosition.y)
                {
                    moving = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Lose");
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<HurdlingHankGameManager>().Lose();
        }
    }

    public void SetPlaying(bool isPlaying)
    {
        playing = isPlaying;
    }
}
