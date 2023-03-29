using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    public GameObject ball;
    public Transform rightHand;
    public static bool isTouching;
    BallMovement ballMovement;
    Vector3 firstPosition;
    public Collider YayEtkisiCollider; 
    // Start is called before the first frame update
    void Start()
    {
        
       // firstPosition = GetComponent<Vector3>();
        ballMovement = GetComponent<BallMovement>();
       // firstPosition = ballMovement.GetComponent<Transform>().transform.position;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("ball"))
        {
            BallMovement.isHoldBall = true; //Topu tuttum. Oyunun ba��nda top elde ba�layacaksa start'da isHoldBall true olmal�d�r.
            YayEtkisiCollider.enabled = true; //Topun �zerindeki collider zaman zaman buraya erken girildi�inde kapanm�yor ve bu da topu kendine h�zl�ca cekip i�imizdeki collider ile topun �arp��mas�na neden oluyor ben buna yay etkisi diyorum. 
            Debug.Log("collidercalisti");
           // ball.transform.SetParent(rightHand);
            ball.transform.parent = rightHand.transform;
            ball.transform.position = rightHand.transform.position;
            isTouching = true;
            ball.transform.rotation = Quaternion.Euler(0, 0, 0);
            //ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
           //  ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;


        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
