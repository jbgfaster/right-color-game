using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 cameraStartPos;
    [SerializeField] GameObject currentCamera;    

    [SerializeField] float speed = 10.0f;
    [SerializeField] int boundsX;
    [SerializeField] int boundsZ;
    void Start()
    {
        cameraStartPos = currentCamera.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        currentCamera.transform.position=gameObject.transform.position+cameraStartPos;

        transform.Translate(speed*Time.deltaTime * Vector3.forward*Input.GetAxis("Vertical"));
        transform.Translate(speed*Time.deltaTime* Vector3.right * Input.GetAxis("Horizontal"));

        if (transform.position.x>boundsX)
        {
            transform.position = new Vector3(boundsX,transform.position.y,transform.position.z) ;
        }
        if (transform.position.x < -boundsX)
        {
            transform.position = new Vector3(-boundsX, transform.position.y, transform.position.z);
        }
        if (transform.position.z > boundsZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundsZ);
        }
        if (transform.position.z < -boundsZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundsZ);
        }

    }
}
