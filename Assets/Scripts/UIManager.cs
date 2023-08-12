using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool GameRun;
    public bool SoundCheck;

    [SerializeField] Sprite[] soundSprites;
    [SerializeField] Image soundImage;

    public AudioListener soundListener;

    void Start()
    {
        soundListener = FindObjectOfType<AudioListener>();
        GameRun = false;
        
    }
    
    void Update()
    {
        isGameRunning();
    }

    void isGameRunning()
    {
        if(GameRun) 
        {
            Time.timeScale = 1;
        }
        else 
        {
            Time.timeScale = 0;
        }
    }

    public void PlayBtn() 
    {
        GameRun = true;
    }

    public void SoundBtn() 
    {
        if(SoundCheck) 
        {
            SoundCheck = false;
            soundImage.sprite = soundSprites[0];
            soundListener.enabled = false;
        }
        else 
        {
            SoundCheck = true;
            soundImage.sprite = soundSprites[1];
            soundListener.enabled = true;
        }
    }
}
