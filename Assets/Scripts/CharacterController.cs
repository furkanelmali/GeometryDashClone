using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    
    Rigidbody2D myRigidbody;
    UIManager manager;
    GeneralCharacterFeatures gcFeatures;

    
    public GameMode gameMode;
    public bool characterLives;

    [SerializeField] float[] speed;
    [SerializeField] float[] gravityScale;
    [SerializeField] float rotateSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Collider2D[] CharactersColliders;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        gcFeatures = GetComponent<GeneralCharacterFeatures>();
        manager = FindObjectOfType<UIManager>();
    }

   
    void Update()
    {
        //Burada rigidbody fiziklerinden etkilenmeyecek bir sekilde, karakterin sürekli olarak saga belirli bir hızda hareket etmesini saglıyorum.
        SpeedandGravity();
        CubeJumpandFly();
        gcFeatures.ChangeCharacter(gameMode);
    }

    private void CubeJumpandFly()
    {
        //Oyun main menu arayuzundeyken, timescale'imiz 0 olduğu icin Layer teması belirlenemiyor, bu da karakterimizin main menude sonsuz bir rotate almasına sebep oluyordu. GameRun boolu ile bunun onune gectim.
        if (manager.GameRun) 
        {
            if (gameMode == GameMode.Cube)
            {
                Jump();
            }
            else if (gameMode == GameMode.Rocket)
            {
                Fly();
            }
        }
    }

    void Jump() 
    {
        //karakterimizin platform layerımıza degip degmedigini kontrol ettiriyorum.
        if (CharactersColliders[(int)gameMode].IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            //karakterimizin, yere capraz inmesini onlemek adına eger yere degiyorsa, rotation z eksenini en yakin 90 katsayisina yuvarliyorum.
            Vector3 Rotation = transform.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            transform.rotation = Quaternion.Euler(Rotation);
            

            if (Input.GetMouseButton(0))
            {
                myRigidbody.velocity = Vector3.zero;
                myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                gcFeatures.JumpEffects();
            }
        }
        else if (!CharactersColliders[(int)gameMode].IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            //Karakterimiz yere degmedigi muddetce, karakterimizin donmesini saglıyorum.
            transform.Rotate(Vector3.back * rotateSpeed);
            
        }
    }

    void Fly()
    {
        transform.rotation = Quaternion.Euler(0, 0, myRigidbody.velocity.y * 2);
        if (Input.GetMouseButton(0)) 
        {
            if (!gcFeatures.Blast.isPlaying)
            {
                gcFeatures.Blast.Play();
            }
            myRigidbody.gravityScale = -gravityScale[(int)gameMode];
            gcFeatures.RocketEffects();
        }
        else 
        {
            if(gcFeatures.Blast.isPlaying)
            {
                gcFeatures.Blast.Stop(); 
            }
            myRigidbody.gravityScale = gravityScale[(int)gameMode];
        }
    }

    void SpeedandGravity() 
    {
        if (characterLives)
        {
            transform.position += Vector3.right * speed[(int)gameMode] * Time.deltaTime;
            myRigidbody.gravityScale = gravityScale[(int)gameMode];
        }
    }

  
}
