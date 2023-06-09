using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    Rigidbody charRB;
    CharacterController characterController;
    public static bool moveRight, moveLeft,isShoot,isJump;
    public Button ShootButton;
    //public Button JumpButton;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
      //  charRB = characterController.GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void Update()
    {
        if(ShootButton != null)
        {
            if (ShootButton.image.fillAmount < 1.0f)
            {
                ShootButton.interactable = false;
                //ShootButton.enabled = false;
                ShootButton.image.fillAmount += Time.deltaTime * 0.6f;
            }
            else
            {
                ShootButton.interactable = true;
                //ShootButton.enabled = true;
            }
        }
        
            
    }
    public void IsJump()
    {
        CharacterAnimController.animTime = 0f;
        isJump = true;
        //CharacterAnimController.jumpTime = 0f;
    }
    public void Shoot()
    {
        if (BallMovement.isHoldBall) //Top sadece elimizdeyse animasyon �al��s�n
        {
            ComputerController.jumpStopCounter = 0;
            ShootButton.image.fillAmount = 0;
            //ShootButton.interactable = false;

            isShoot = true;
            CharacterAnimController.animTime = 0f;
            //BallMovement.isHoldBall = false; //Top elden ��kt� Burada bu kod olmamal�d�r. Aksi taktirde button down olunca top hen�z elden ��kmadan rigidbody sleep olur.
        }


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
