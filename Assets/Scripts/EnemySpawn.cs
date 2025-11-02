using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your enemy prefab in the Inspector   

    public Transform[] spawnPoint;   // Assign a spawn location in the Inspector

    public DifficultySelector ds;

    public int limit;

    private void Start()
    {
        ds = FindObjectOfType<DifficultySelector>();

        if(ds.currentDifficulty == DifficultySelector.Difficulty.Cool)
        {
            limit = 5;
        }
        else if(ds.currentDifficulty == DifficultySelector.Difficulty.Cowabunga)
        {
            limit = 10;
        }
        else if (ds.currentDifficulty == DifficultySelector.Difficulty.WhoaMama)
        {
            limit = 15;
        }
        else if (ds.currentDifficulty == DifficultySelector.Difficulty.OhQuitIt)
        {
            limit = 20;
        }
        else if (ds.currentDifficulty == DifficultySelector.Difficulty.AwGeez)
        {
            limit = 25;
        }
    }

    public void SpawnEnemy(int numSpawnersToActivate)
    {

        

        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Enemy prefab or spawn point not assigned!");
            return;
        }

        System.Random random = new System.Random();
        spawnPoint = spawnPoint.OrderBy(x => random.Next()).ToArray();

        numSpawnersToActivate = Mathf.Min(limit, spawnPoint.Length);

        for (int i = 0; i < numSpawnersToActivate; i++)
        {
            Instantiate(enemyPrefab, spawnPoint[i].position, spawnPoint[i].rotation);
        }

        
    }
}
