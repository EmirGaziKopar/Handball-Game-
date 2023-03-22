using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowCharacter : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] GameObject characterPointer;

    Transform characterPosition;

    // Start is called before the first frame update
    void Start()
    {
        characterPosition = characterPointer.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, characterPosition.transform.position, Time.deltaTime * speed);
    }
}
