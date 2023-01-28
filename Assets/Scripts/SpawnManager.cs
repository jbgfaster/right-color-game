using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject drop;
    [SerializeField] private Material[] materials;
    [SerializeField] private float startSpawnRate=3.0f;

    private Platform[] platforms;
    private GameManager gameManager;
    private int difficulty;
    private float spawnRate;
    

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        platforms = GameObject.FindObjectsOfType<Platform>();
        spawnRate=startSpawnRate;
    }

    public void Restart()
    {
        difficulty = 0;
        spawnRate = startSpawnRate;
        ReplacePlatforms();
        Invoke("ChangeDifficulty", spawnRate);
    }

    private void ChangeDifficulty()
    {
        difficulty++;
        if(difficulty>=10 && spawnRate>0.5f)
        {
            difficulty = 0;
            ReplacePlatforms();            
            gameManager.ChangeDifficulty(1);
            spawnRate += - 0.1f;          
        }
        
        SpawnDrop();

        if(gameManager.inGame)
        {
            Invoke("ChangeDifficulty", spawnRate);
        } 
    }

    private void ReplacePlatforms()
    {
        foreach (Platform i in platforms)
        {
            i.SetColor(materials[Random.Range(0, materials.Length)]);
        }
    }

    private void SpawnDrop()
    {       
        Platform randomPlatform = platforms[Random.Range(0, platforms.Length)];
        Material randomColor = materials[Random.Range(0, materials.Length)];

        bool isSameColor= randomPlatform.GetComponent<MeshRenderer>().material.name.Contains(randomColor.name)?true:false;
        Instantiate(drop, randomPlatform.transform.position+Vector3.up*20, randomPlatform.transform.rotation).GetComponent<Drop>().SetColor(randomColor,isSameColor);
    }
}
