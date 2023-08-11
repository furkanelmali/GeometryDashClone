using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    GeneralCharacterFeatures gcFeatures;

    private void Start()
    {
        gcFeatures = FindObjectOfType<GeneralCharacterFeatures>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gcFeatures.Die();
        }
    }
}
