using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public CharacterController controller;

    private void Start()
    {
        controller = FindObjectOfType<CharacterController>();
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Object in");

        if (collision.gameObject.tag == "Player")
            controller.gameMode = (controller.gameMode == 0) ? 1 : 0;
    }


}
