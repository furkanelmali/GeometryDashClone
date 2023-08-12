using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class GeneralCharacterFeatures : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    [SerializeField] GameObject[] CharacterSprites;
    [SerializeField] float delay;

    AudioSource audioSource;
    CharacterController controller;
    UIManager manager;

    public ParticleSystem crashEffect;
    public ParticleSystem Blast;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        manager = FindObjectOfType<UIManager>();
    }
    public void Die(AudioSource obstacleSource)
    {   
        //karakterimiz bir obstacle ile karsilastiginda gerekli olan effectler ile beraber, sahneyi yeniden yükleyen fonksiyon.
        
        obstacleSource.clip = sounds[3];
        crashEffect.Play();
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
    public void ChangeCharacter(GameMode gameMode) 
    {
        for(int i = 0; i < CharacterSprites.Length; i++) 
        {
            CharacterSprites[i].SetActive(i == (int)gameMode);
        }
    }
}
