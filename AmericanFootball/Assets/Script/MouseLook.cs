using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(50, 500)]
    [SerializeField] float sens;
    //public Transform body;
    public float yRot = 0f;
    //public Transform body;
    


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float rotX = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime; // yani mousedan gelen x de�eri * sens * zaman(kare ba��na stabil i�lem i�in)
        float rotY = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;

        yRot -= rotX;

        transform.localRotation = Quaternion.Euler(0f, -yRot, 0f);

        //body.Rotate(Vector3.up * yRot * Time.deltaTime);
    }
}
