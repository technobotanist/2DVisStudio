using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private bool found = false;
    private bool canCollect = false;

    // Update is called once per frame
    void Update()
    {
        if(canCollect && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<HanksHeistGameManager>().ScoreUp();
            this.found = true;
            gameObject.SetActive(false);
        }
    }

    public bool GetFound()
    {
        return found;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canCollect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canCollect = false;
        }
    }
}
