using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyManager eM;
    public float enemHealth = 3f;

    public GameObject gunHitEffect;

    public AudioSource haha;

    public AnglePlayer aP;

    private float currentEnemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        ApplyDifficultySettings();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemyHealth <= 0)
        {
            eM.RemoveEnemy(this);
            GetComponent<Enemy>().enabled = false;

            GetComponent<EnemyAI>().enabled = false;

            GetComponent<EnemyAwareness>().enabled = false;

            aP.isDead = true;

            haha.GetComponent<AudioSource>().enabled = false;

            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.RemoveEnemy(this);
            }

        }    
    }

    public void TakeDamage(float damage)
    {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        currentEnemyHealth -= damage;
        haha.GetComponent<AudioSource>().Play();
    }

    void ApplyDifficultySettings()
    {
        DifficultySelector.Difficulty difficulty = DifficultySelector.Instance.currentDifficulty;

        switch (difficulty)
        {
            case DifficultySelector.Difficulty.Cool:

                currentEnemyHealth = enemHealth -2;
                break;
            case DifficultySelector.Difficulty.Cowabunga:

                currentEnemyHealth = enemHealth-1;
                break;
            case DifficultySelector.Difficulty.WhoaMama:

                currentEnemyHealth = enemHealth;
                break;

            case DifficultySelector.Difficulty.OhQuitIt:

                currentEnemyHealth = enemHealth +1;
                break;

            case DifficultySelector.Difficulty.AwGeez:

                currentEnemyHealth = enemHealth +2;
                // Add more extreme behaviors here, e.g., dodging, special abilities
                break;
        }
    }
}
