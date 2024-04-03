using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject drop;
    [SerializeField] private Material[] materials;
    
    [SerializeField] private Transform[] platforms;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float startSpawnRate = 3.0f;
    [SerializeField] private float startYposition = 20.0f;

    private int difficulty;
    private float spawnRate;
    

    void Start()
    {
        spawnRate = startSpawnRate;
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
        difficulty ++;
        if(difficulty >= 10 && spawnRate > 0.5f)
        {
            difficulty = 0;
            ReplacePlatforms();            
            gameManager.ChangeDifficulty(1);
            spawnRate += - 0.1f;          
        }
        
        SpawnDrop();

        if(gameManager.InGame())
        {
            Invoke("ChangeDifficulty", spawnRate);
        } 
    }

    private void ReplacePlatforms()
    {
        foreach (var i in platforms)
        {
            i.GetComponent<ISetColor>()?.SetColor(materials[Random.Range(0, materials.Length)]);
        }
    }

    private void SpawnDrop()
    {       
        Transform randomPlatform = platforms[Random.Range(0, platforms.Length)];
        Material randomColor = materials[Random.Range(0, materials.Length)];

        bool isSameColor = randomPlatform.GetComponent<MeshRenderer>().material.name.Contains(randomColor.name)?true:false;

        var instance = Instantiate(drop, randomPlatform.transform.position + Vector3.up * startYposition, randomPlatform.transform.rotation);
        instance.GetComponent<ISetColor>()?.SetColor(randomColor,isSameColor);
        instance.GetComponent<IEventInt>()?.GetDestroyEvent().AddListener(gameManager.ChangeScore);
    }
}
