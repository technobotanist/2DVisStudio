using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float flapForce = 5f;
    public float flapDelay = 0.25f;
    private bool playing = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playing && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Flap");
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * flapForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pipe")
        {
            Debug.Log("Lose");
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Lose();
        }
    }

    public void SetPlaying(bool isPlaying)
    {
        playing = isPlaying;
    }
}
