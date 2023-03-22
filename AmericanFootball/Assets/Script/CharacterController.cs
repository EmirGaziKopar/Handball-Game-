using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    new Rigidbody rigidbody;
    [SerializeField] float jumpPower;
    Animator anim;
    public float velocity;
    ButtonController buttonController;
    public static float time;
    private void Awake()
    {
        buttonController = GetComponent<ButtonController>();
        anim = this.gameObject.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        
    }
    void Update()
    {     
        moveTheJack();
    }
   public void moveTheJack()
    {
        if(transform.position.z >= 560.3f && transform.position.z <= 576f && ButtonController.isShoot != true) //þut durumu false ise hareket etmeli, þut atarken hareket etmemeli
        {
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
            Debug.Log("konum: " + transform.position.z);

            if (Input.GetKey(KeyCode.S) | ButtonController.moveLeft==true)
            {
                Debug.Log("buraya girdi oyy");
                transform.Translate(new Vector3(0f, 0f, -time * velocity * Time.deltaTime));
                
                anim.SetFloat("Vertical",-1);
                if (time <= 1)
                {
                    time += Time.deltaTime*2f;
                

                }
            }
            else if (Input.GetKey(KeyCode.W) | ButtonController.moveRight==true)
            { 
                Debug.Log("buraya girdi umarým");

                transform.Translate(new Vector3(0f, 0f, time * velocity * Time.deltaTime));
                 anim.SetFloat("Vertical",time);
                if (time <= 1)
                {
                    time += Time.deltaTime*2;


                }
            }



            if (Input.GetKeyDown(KeyCode.Space))
            {
                 rigidbody.AddForce(transform.up * jumpPower);
            }
        }
        else if(transform.position.z < 560.3f)
        {
            transform.position += new Vector3(0f, 0f, 1f*Time.deltaTime);
        }
        else if (transform.position.z > 576f)
        {
            transform.position += new Vector3(0f, 0f, -1f*Time.deltaTime );
        }

    }
        
}