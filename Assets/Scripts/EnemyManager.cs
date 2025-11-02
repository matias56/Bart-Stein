using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; } // Singleton pattern

    public List<Enemy> enemiesInTrigger = new List<Enemy>();

    private void Start()
    {
        enemiesInTrigger.Clear();

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        enemiesInTrigger.AddRange(allEnemies);
    }

    public void AddEnemy(Enemy enemy)
   {
        
        enemiesInTrigger.Add(enemy);
   }


    public void RemoveEnemy(Enemy enemy)
    {
        enemiesInTrigger.Remove(enemy);
    }
}
