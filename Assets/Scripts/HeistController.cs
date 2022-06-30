using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeistController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float playerSpeed = 7f;
    public float upForce = 1f;
    public float downGravity = 1.5f;
    private float defaultGravity;
    private bool playing = true;

    public AudioSource flapSound;
    public AudioSource bonkSound;

    public Transform[] hanks;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(playing)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");

            float x = rb.velocity.x;
            float y = rb.velocity.y;

            if (vertical > 0.1f)
            {
                Debug.Log("Up");
                y = upForce;
            }
            else if (vertical < -0.1f)
            {
                Debug.Log("Down");
                rb.gravityScale = downGravity;
            }
            else if(rb.gravityScale != defaultGravity)
            {
                Debug.Log("Reset Gravity");
                rb.gravityScale = defaultGravity;
            }

            if(horizontal > 0.1f)
            {
                Debug.Log("Right");
                x = playerSpeed;
                transform.localScale = new Vector2(1, 1);
            }
            else if(horizontal < -0.1f)
            {
                Debug.Log("Left");
                x = -playerSpeed;
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                x = 0f;
            }

            rb.velocity = new Vector2(x, y);

            foreach(Transform hank in hanks)
            {
                if(hank.position.x < -142.2222f)
                {
                    Debug.Log("Teleport Right");
                    hank.position = new Vector2(hank.position.x + (142.2222f * 2), hank.position.y);
                }
                else if(hank.position.x > 142.2222f)
                {
                    Debug.Log("Teleport Left");
                    hank.position = new Vector2(hank.position.x - (142.2222f * 2), hank.position.y);
                }
            }
        }
        else
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pipe")
        {
            Debug.Log("Lose");
            bonkSound.Play();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<HoppyHankGameManager>().Lose();
        }
    }

    public void SetPlaying(bool isPlaying)
    {
        playing = isPlaying;
    }
}
