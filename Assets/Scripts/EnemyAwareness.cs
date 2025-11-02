using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public float awarenessRadius = 8f;
    public Material aggroMat;
    public bool isAggro;
    private Transform playersTransform;
    public PlayerHealth pH;

    private float currentDetectionRange;

    private void Start()
    {
        playersTransform = FindObjectOfType<PlayerMovement>().transform;
        ApplyDifficultySettings();
    }

    private void Update()
    {

        var dist = Vector3.Distance(transform.position, playersTransform.position);

        if(dist < currentDetectionRange)
        {
            
            isAggro = true;
            
            
        }

        if(pH.defeated)
        {
            isAggro = false;
        }

        if (isAggro)
        {


            GetComponent<MeshRenderer>().material = aggroMat;

        }
    }
    void ApplyDifficultySettings()
    {
        DifficultySelector.Difficulty difficulty = DifficultySelector.Instance.currentDifficulty;

        switch (difficulty)
        {
            case DifficultySelector.Difficulty.Cool:

                currentDetectionRange = awarenessRadius * 0.4f;
                break;
            case DifficultySelector.Difficulty.Cowabunga:

                currentDetectionRange = awarenessRadius * 0.7f;
                break;
            case DifficultySelector.Difficulty.WhoaMama:

                currentDetectionRange = awarenessRadius;
                break;

            case DifficultySelector.Difficulty.OhQuitIt:

                currentDetectionRange = awarenessRadius * 1.2f;
                break;

            case DifficultySelector.Difficulty.AwGeez:

                currentDetectionRange = awarenessRadius * 1.6f;
                // Add more extreme behaviors here, e.g., dodging, special abilities
                break;
        }
    }

}
