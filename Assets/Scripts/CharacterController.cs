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

    public int gameMode;

    [SerializeField] float[] speed;
    [SerializeField] float[] gravityScale;
    [SerializeField] float rotateSpeed;
    [SerializeField] float jumpForce;
    

    void Start()
    {
        CubeCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        //Burada rigidbody fiziklerinden etkilenmeyecek bir sekilde, karakterin sürekli olarak saga belirli bir hızda hareket etmesini saglıyorum.
        transform.position += Vector3.right * speed[gameMode] * Time.deltaTime;
        myRigidbody.gravityScale = gravityScale[gameMode];
        CubeJumpandFly();
    }

    private void CubeJumpandFly()
    {
        if (gameMode == 0)
        { 
            Jump();
        }
        else if(gameMode == 1)
        {
            Fly();
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
        }
        else 
        {
            myRigidbody.gravityScale = gravityScale[gameMode];
        }
    }

  
}
