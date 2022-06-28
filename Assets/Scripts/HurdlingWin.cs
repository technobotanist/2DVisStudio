using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdlingWin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<HurdlingHankGameManager>().Win();
        }
    }
}
