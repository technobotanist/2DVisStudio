using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeistCameraMove : MonoBehaviour
{
    private bool movingCamera = false;
    private bool movingPlayer = false;
    private bool moving;

    private Vector3 targetPositionCamera;
    private Vector2 targetPositionPlayer;
    public float moveStep = 0.01f;
    private float playerMoveStep;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moving = movingCamera | movingPlayer;
        if (movingCamera)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionCamera, moveStep);
            if (transform.position.y == targetPositionCamera.y)
            {
                movingCamera = false;
            }
        }
        if(movingPlayer)
        {
            while(player.transform.position.x > 142.2222f)
            {
                Debug.Log("Teleport left");
                player.transform.position = new Vector2(player.transform.position.x - 142.2222f, player.transform.position.y);
            }
            while(player.transform.position.x < -142.2222f)
            {
                Debug.Log("Teleport right");
                player.transform.position = new Vector2(player.transform.position.x + 142.2222f, player.transform.position.y);
            }
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetPositionPlayer, playerMoveStep);

            if(player.transform.position.y == targetPositionPlayer.y && player.transform.position.x == targetPositionPlayer.x)
            {
                movingPlayer = false;
                if(player.transform.position.y == -6.48f)
                {
                    player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    player.GetComponent<HeistController>().SetPlaying(true);
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
                
            }
        }
    }

    public void MoveDown()
    {
        if (!movingCamera && !movingPlayer && transform.position.y == 0)
        {
            Debug.Log("Move Down");
            movingCamera = true;
            movingPlayer = true;
            targetPositionCamera = new Vector3(transform.position.x, -6.48f, -10f);
            targetPositionPlayer = new Vector2(transform.position.x, -6.48f);
            playerMoveStep = moveStep;
        }
    }

    public void MoveUp()
    {
        if (!movingCamera && !movingPlayer && transform.position.y == -6.48f)
        {
            Debug.Log("Move Up");
            movingCamera = true;
            movingPlayer = true;
            targetPositionCamera = new Vector3(transform.position.x, 0f, -10f);
            targetPositionPlayer = new Vector2(transform.position.x, 0f);
            playerMoveStep = 1.8f * moveStep;
        }
    }
}
