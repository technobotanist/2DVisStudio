using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPipe : MonoBehaviour
{
    public float destroyDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pipe")
        {
            Debug.Log("Destroy pipe");
            StartCoroutine(DestroyPipeObject(collision.gameObject));
        }
    }

    IEnumerator DestroyPipeObject(GameObject pipe)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(pipe);
    }
}
