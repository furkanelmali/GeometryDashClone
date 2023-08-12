using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public CharacterController controller;
    GeneralCharacterFeatures gcFeatures;

    private void Start()
    {
        controller = FindObjectOfType<CharacterController>();
        gcFeatures = FindObjectOfType<GeneralCharacterFeatures>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            controller.gameMode = ((controller.gameMode == GameMode.Cube) ? GameMode.Rocket : GameMode.Cube);
            gcFeatures.ChangeCharacter(controller.gameMode);
        }  
    }
}
