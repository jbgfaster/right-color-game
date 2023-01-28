using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] int boundsX;
    [SerializeField] int boundsZ;
    [SerializeField] GameObject currentCamera;    

    private Vector3 cameraStartPos;

    void Start()
    {
        cameraStartPos = currentCamera.transform.localPosition;
    }

    void Update()
    {
        currentCamera.transform.position=gameObject.transform.position+cameraStartPos;
        Move();
        StopByBonds();
    }

    private void Move()
    {
        transform.Translate(speed*Time.deltaTime * Vector3.forward*Input.GetAxis("Vertical"));
        transform.Translate(speed*Time.deltaTime* Vector3.right * Input.GetAxis("Horizontal"));
    }

    private void StopByBonds()
    {
        if (transform.position.x>boundsX)
        {
            transform.position = new Vector3(boundsX,transform.position.y,transform.position.z) ;
        }
        else if (transform.position.x < -boundsX)
        {
            transform.position = new Vector3(-boundsX, transform.position.y, transform.position.z);
        }
        if (transform.position.z > boundsZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundsZ);
        }
        else if (transform.position.z < -boundsZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundsZ);
        }
    }
}
