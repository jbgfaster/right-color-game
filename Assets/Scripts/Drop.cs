using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    private bool isRight;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(transform.position.y<-10)
        {
            if (isRight)
            {
                gameManager.ChangeScore(-1);
            }
            else
            {
                gameManager.ChangeScore(1);
            }
            Destroy(gameObject);
        }
    }

    public void SetColor(Material materialColor, bool isRight)
    {
        this.isRight = isRight;
        GetComponent<Renderer>().material = materialColor;         
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ChangeScore(isRight?-1:1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Platform"))
        {
            gameManager.ChangeScore(isRight?1:-1);
            Destroy(gameObject);
        }        
    }
}
