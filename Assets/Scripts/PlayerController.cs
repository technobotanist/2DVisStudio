using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float flapForce = 5f;
    private bool canFlap = true;
    public float flapDelay = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canFlap)
        {
            Debug.Log("Flap");
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * flapForce);
            /**canFlap = false;
            StartCoroutine(WaitFlap());*/
        }
    }

    IEnumerator WaitFlap()
    {
        yield return new WaitForSeconds(flapDelay);
        canFlap = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pipe")
        {
            Debug.Log("Lose");
        }
    }
}
