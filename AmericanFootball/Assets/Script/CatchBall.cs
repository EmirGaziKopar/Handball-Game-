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
    // Start is called before the first frame update
    void Start()
    {
        
       // firstPosition = GetComponent<Vector3>();
        ballMovement = GetComponent<BallMovement>();
       // firstPosition = ballMovement.GetComponent<Transform>().transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball") && isTouching == false)
        {
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
