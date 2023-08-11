using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    Collider2D CubeCollider;
    Rigidbody2D myRigidbody;
    UIManager manager;
    GeneralCharacterFeatures gcFeatures;

    public int gameMode;
    public bool characterLives;

    [SerializeField] float[] speed;
    [SerializeField] float[] gravityScale;
    [SerializeField] float rotateSpeed;
    [SerializeField] float jumpForce;
    

    void Start()
    {
        CubeCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        gcFeatures = GetComponent<GeneralCharacterFeatures>();
        manager = FindObjectOfType<UIManager>();
    }

   
    void Update()
    {
        //Burada rigidbody fiziklerinden etkilenmeyecek bir sekilde, karakterin sürekli olarak saga belirli bir hızda hareket etmesini saglıyorum.
        SpeedandGravity();
        CubeJumpandFly();
    }

    private void CubeJumpandFly()
    {
        //Oyun main menu arayuzundeyken, timescale'imiz 0 olduğu icin Layer teması belirlenemiyor, bu da karakterimizin main menude sonsuz bir rotate almasına sebep oluyordu. GameRun boolu ile bunun onune gectim.
        if (manager.GameRun) 
        {
            if (gameMode == 0)
            {
                Jump();
            }
            else if (gameMode == 1)
            {
                Fly();
            }
        }
    }

    void Jump() 
    {
        //karakterimizin platform layerımıza degip degmedigini kontrol ettiriyorum.
        if (CubeCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
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
        else if (!CubeCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
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
            myRigidbody.gravityScale = -gravityScale[gameMode];
            gcFeatures.RocketEffects();
        }
        else 
        {
            myRigidbody.gravityScale = gravityScale[gameMode];
        }
    }

    void SpeedandGravity() 
    {
        if (characterLives)
        {
            transform.position += Vector3.right * speed[gameMode] * Time.deltaTime;
            myRigidbody.gravityScale = gravityScale[gameMode];
        }
    }

  
}
