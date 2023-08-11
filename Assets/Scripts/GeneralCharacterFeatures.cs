using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralCharacterFeatures : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    [SerializeField] float delay;

    AudioSource audioSource;
    CharacterController controller;
    UIManager manager;


    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        manager = FindObjectOfType<UIManager>();

    }

    
    void Update()
    {
        
    }

    public void Die(AudioSource obstacleSource)
    {   
        //karakterimiz bir obstacle ile karsilastiginda gerekli olan effectler ile beraber, sahneyi yeniden yükleyen fonksiyon.
        
        obstacleSource.clip = sounds[3];
        obstacleSource.Play();
        controller.characterLives = false;

        Invoke("ReloadScene", audioSource.clip.length);
    }

    public void Finish(AudioSource finishLine) 
    {
        //Die fonksiyonu ile aynı islevi görse de, surdurulebilirlik adina yeni bir level eklendiginde islevi degisecegi icin farkli bir fonksiyonda yazmak istedim.
        finishLine.clip = sounds[4];
        finishLine.Play();
        
        controller.characterLives = false;

        Invoke("ReloadScene", audioSource.clip.length);
    }

    public void JumpEffects()
    {
        audioSource.clip = sounds[1];
        audioSource.Play();

    }

    public void RocketEffects()
    {
        audioSource.Stop();
        audioSource.clip = sounds[2];
        audioSource.Play();
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

   
}
