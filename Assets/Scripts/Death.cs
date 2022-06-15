using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Lose");
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Lose();
        }
    }
}
