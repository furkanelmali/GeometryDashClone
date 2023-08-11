using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    Collider2D CubeCollider;
    Rigidbody2D myRigidbody;
    [SerializeField] float speed;
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
        transform.position += Vector3.right * speed * Time.deltaTime;
        CubeJumpandFly();
    }

    private void CubeJumpandFly()
    {
        //karakterimizin platform layerımıza degip degmedigini kontrol ettiriyorum.
        if(CubeCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) 
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
}
