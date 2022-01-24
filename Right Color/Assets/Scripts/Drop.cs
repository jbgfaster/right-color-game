using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public bool right;
    public string color;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if(transform.position.y<-10)
        {
            if (right)
            {
                gameManager.Balls(-1);
            }
            else
            {
                gameManager.Balls(1);
            }
            Destroy(gameObject);
        }
    }
    public void SetColor(Material materialColor, bool rightTemp)
    {
        color = materialColor.name;
        right = rightTemp;
        GetComponent<Renderer>().material = materialColor;         
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && right)
        {
            gameManager.Balls(-1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && !right)
        {
            gameManager.Balls(1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Platform") && right)
        {
            gameManager.Balls(1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Platform") && !right)
        {
            gameManager.Balls(-1);
            Destroy(gameObject);
        }
        
    }
}
