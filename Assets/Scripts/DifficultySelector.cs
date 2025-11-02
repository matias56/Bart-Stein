using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    public enum Difficulty { AwGeez, OhQuitIt, WhoaMama, Cowabunga, Cool }

    public Difficulty currentDifficulty = Difficulty.Cool;

    public static DifficultySelector Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Ensures only one instance

        DontDestroyOnLoad(gameObject); // Persist across scenes
    }




    public void Choose(int numb)
    {
        if (numb == 1)
        {
            currentDifficulty = Difficulty.Cool;
        }
        else if (numb == 2)
        {
            currentDifficulty = Difficulty.Cowabunga;
        }
        else if (numb == 3)
        {
            currentDifficulty = Difficulty.WhoaMama;
        }
        else if (numb == 4)
        {
            currentDifficulty = Difficulty.OhQuitIt;
        }
        else if (numb == 5)
        {
            currentDifficulty = Difficulty.AwGeez;
        }
    }
}
