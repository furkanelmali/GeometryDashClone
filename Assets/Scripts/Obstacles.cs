using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    GeneralCharacterFeatures gcFeatures;
    AudioSource audioSource;

    private void Start()
    {
        gcFeatures = FindObjectOfType<GeneralCharacterFeatures>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gcFeatures.Die(audioSource);
        }
    }
}
