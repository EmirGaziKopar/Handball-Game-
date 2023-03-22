using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    Rigidbody charRB;
    CharacterController characterController;
    public static bool moveRight, moveLeft,isShoot;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
      //  charRB = characterController.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    
   
    public void Shoot()
    {
        isShoot = true;
        CharacterAnimController.animTime = 0f;
    }
    public void MoveRight()
    {
        moveRight = true;
    }


    public void MoveLeft()
    {

        moveLeft = true;

    }

    public void stopMoveRight()
    {

        Debug.Log("stopsagcalisti");
        moveRight = false;
        CharacterController.time = 0f;

        // charRB.velocity = Vector3.zero;

    }

    public void stopMoveLeft()
    {
        Debug.Log("stopsolcalisti");
        moveLeft = false;
        CharacterController.time = 0f;

        // charRB.velocity = Vector3.zero;

    }
    public void stopShoot()
    {

        isShoot = false;

    }


}
