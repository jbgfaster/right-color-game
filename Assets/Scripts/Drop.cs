using UnityEngine;

public class Drop : MonoBehaviour, ISetColor, IEventInt
{
    public CustomEventInt DestroyEvent;

    private bool isRight;


    void Update()
    {
        TouchGround();
    }

    private void TouchGround()
    {
        if(transform.position.y < -10)
        {
            if (isRight)
            {
                DestroyEvent.Invoke(-1);
            }
            else
            {
                DestroyEvent.Invoke(1);
            }
            Destroy(gameObject);
        }
    }

    public void SetColor(Material materialColor, bool isRight)
    {
        _SetColor(materialColor, isRight);
    }

    private void _SetColor(Material materialColor, bool isRight)
    {
        this.isRight = isRight;
        GetComponent<Renderer>().material = materialColor;         
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyEvent.Invoke(isRight?-1:1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Platform"))
        {
            DestroyEvent.Invoke(isRight?1:-1);
            Destroy(gameObject);
        }        
    }

    public CustomEventInt GetDestroyEvent()
    {
        return DestroyEvent;
    }
}
