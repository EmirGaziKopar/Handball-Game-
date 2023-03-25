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
    int sayac;
    private void Awake()
    {
        buttonController = GetComponent<ButtonController>();
        anim = this.gameObject.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        
    }
    /*
    private void FixedUpdate()
    {
        moveTheJack();
    }
    */
    
    void Update()
    {     
        moveTheJack();
        Debug.Log("isJump" + ButtonController.isJump);
    }
    
   public void moveTheJack()
    {
        if(transform.position.z >= 560.3f && transform.position.z <= 576f && ButtonController.isShoot != true && ButtonController.isJump != true) //�ut durumu false ise hareket etmeli, �ut atarken hareket etmemeli
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
                Debug.Log("buraya girdi umar�m");

                transform.Translate(new Vector3(0f, 0f, time * velocity * Time.deltaTime));
                 anim.SetFloat("Vertical",time);
                if (time <= 1)
                {
                    time += Time.deltaTime*2;


                }
            }



            if (Input.GetKeyDown(KeyCode.Space)) //Get And Move
            {
                if (sayac <1)
                {
                    Debug.Log("Calisti");
                    sayac++;
                    rigidbody.velocity = new Vector3(0f, 600f * Time.deltaTime, 0f);
                    ButtonController.isJump = true; //Normalde bu buttonun down olmas� sonucu �al��acak ama �uanda mevcut bir button olmad��� i�in test olarak koydum
                    CharacterAnimController.animTime = 0f; //Buttona ve space'e bast���nda hareketsiz kalmas� gerken s�reyi tekrar 0'lad�k ki tekrar 1 saniye boyunca isJump = false kalabilsin(ButtonController scriptinde)
                }
                
                //rigidbody.Sleep();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                sayac = 0;
            }
        }
        else if(transform.position.z <= 560.3f)
        {
            transform.position += new Vector3(0f, 0f, 1f*Time.deltaTime);
        }
        else if (transform.position.z >= 576f)
        {
            transform.position += new Vector3(0f, 0f, -1f*Time.deltaTime );
        }

    }
        
}