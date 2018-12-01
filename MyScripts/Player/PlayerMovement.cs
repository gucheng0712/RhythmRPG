using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;


    // Update is called once per frame
    void Update()
    {
        if (GameManager_Data.Instance != null)
        {
            if (Input.GetKey(GameManager_Data.Instance.keys["MoveLeftKey"]))
            {
                horizontalMove = -runSpeed;
            }
            else if (Input.GetKey(GameManager_Data.Instance.keys["MoveRightKey"]))
            {
                horizontalMove = runSpeed;
            }
            else
            {
                horizontalMove = 0f;
            }

            if (Input.GetKeyDown(GameManager_Data.Instance.keys["InteractKey"]))
            {
                controller.Interact();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                horizontalMove = -runSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalMove = runSpeed;
            }
            else
            {
                horizontalMove = 0f;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                controller.Interact();
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime);
    }


}
