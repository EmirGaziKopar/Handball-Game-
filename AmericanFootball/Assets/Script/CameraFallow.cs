using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public new Transform camera, man;


    MouseLook mouseLook;

    [SerializeField] GameObject mouseLookPointer;

    Vector3 yon;

    public float hiz = 10f;

    float x;
    float y;
    float z;

    [SerializeField] Vector3 distance;


    private void Start()
    {
        mouseLook = mouseLookPointer.GetComponent<MouseLook>();
        float x = distance.x;
        float y = distance.y;
        float z = distance.z;
    }
    private void FixedUpdate()
    {

        //distance.x = mouseLook.yRot;

        camera.position = Vector3.Lerp(camera.position, man.position - distance, .05f);
        
        
        camera.LookAt(man);
    }
}
