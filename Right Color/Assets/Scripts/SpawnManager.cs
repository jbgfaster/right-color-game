using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject drop;
    [SerializeField] Material[] materials;
    [SerializeField] string[] colors;

    private GameObject[] dropPositions;
    private GameManager gameManager;
    private int difficultyTemp;
    private float spawnRate=3.0f;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        dropPositions = GameObject.FindGameObjectsWithTag("Platform");
    }

    public void Restart()
    {

        difficultyTemp = 0;
        spawnRate = 3.0f;
        foreach (GameObject temp in dropPositions)
        {
            temp.GetComponent<Platform>().SetColor(materials[Random.Range(0, materials.Length)]);
        }
        Invoke("SpawnDrop", spawnRate);
    }

    void SpawnDrop()
    {
        GameObject randomDrop = dropPositions[Random.Range(0, dropPositions.Length)];
        Material randomColor = materials[Random.Range(0, materials.Length)];
        bool right = false;
        if (randomDrop.GetComponent<Platform>().color== randomColor.name)
        {
            right = true;
        }
        Instantiate(drop, randomDrop.transform.position+Vector3.up*20, randomDrop.transform.rotation).GetComponent<Drop>().SetColor(randomColor,right);
        difficultyTemp++;
        if(difficultyTemp>=10 &&spawnRate>0.1f)
        {
            difficultyTemp = 0;
            gameManager.difficulty++;
            spawnRate += - 0.1f;          
        }
        if(gameManager.inGame)
        {
            Invoke("SpawnDrop", spawnRate);
        }        
    }
}
