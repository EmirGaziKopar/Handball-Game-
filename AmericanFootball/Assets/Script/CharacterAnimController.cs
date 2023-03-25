using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    Animator anim;
    ButtonController buttonController;
    public static float animTime; //Her click'de shoot click bunu sýfýrlayacak boylelikle oyuncu animasyon bitmeden buttona basamayacak.
    bool isAnimRunning;
    public static float animTime2;
    //Transform sceneTransform;
    //[SerializeField] GameObject Scene;
    // Start is called before the first frame update
    void Start()
    {
        buttonController = GetComponent<ButtonController>();
        anim = GetComponent<Animator>();
        //sceneTransform = Scene.GetComponent<Transform>();
    }

    public void AnimTiming()
    {
        if (animTime < 1f)
        {
            Debug.Log("Buradayiz;");
            animTime += Time.deltaTime;
        }
        else
        {
            ButtonController.isShoot = false;
            ButtonController.isJump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Test : " + ButtonController.isShoot);
        // if (Input.GetMouseButton(0) | ButtonController.isShoot==true)
         if(ButtonController.isShoot == true) //clicklenen an
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0); Bunun yüzünden animasyona çamur attým
            //transform.parent = sceneTransform.transform;
            Debug.Log("Buraya girildi");
            anim.SetBool("shot", true);
            AnimTiming();

        }
        else
        {
            anim.SetBool("shot", false);
        }
        

         if(ButtonController.isJump == true)
        {
            anim.SetBool("backflip", true);
            AnimTiming();

        }
        else
        {
            anim.SetBool("backflip", false);
        }
        
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("backflip", true);
            AnimTiming();
        }
        else
        {
            anim.SetBool("backflip", false);
        }
        */
        /*if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            anim.SetBool("shot", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) //animasyonu durduktan sonra false yap demek istedik bu nedenle animasyon bitmeden tekrar sol click dahi yapsanýz animasyon baþlamayacaktýr.
        {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            Debug.Log("not playing");
            anim.SetBool("shot", false);
        }*/


    }
}
