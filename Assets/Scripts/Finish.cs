using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GeneralCharacterFeatures gcFeatures;

    AudioSource audioSource;

    private void Start()
    {
        gcFeatures = FindObjectOfType<GeneralCharacterFeatures>();
        audioSource = GetComponent<AudioSource>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        gcFeatures.Finish(audioSource);
    }
}
