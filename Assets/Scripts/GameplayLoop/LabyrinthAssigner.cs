using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System.Linq;

public enum DifficultyLevels
{
    Easy,
    Normal,
    Hard
}

public class LabyrinthAssigner : MonoBehaviour
{
    [SerializeField] private List<Labyrinth> easyLabyrinths;
    [SerializeField] private int easyProblemsAmount;

    bool _isLabyrinthFound;


    public Labyrinth GetAvailableLabyrinth(DifficultyLevels difficulty)
    {
        switch (difficulty)
        {
            case DifficultyLevels.Easy:
                UsefulStuff.ShuffleList(easyLabyrinths); //Shuffling for randomness

                foreach (var labyr in easyLabyrinths)
                {
                    if(labyr.IsInUse == false)
                    {
                        labyr.UpdateProblems(easyProblemsAmount);
                        labyr.IsInUse = true;
                        labyr.OnCeasedToBeUsed += RestoreUse;
                        return labyr;
                    }
                }
                return null;
        }
        return null;
    }

    private void RestoreUse(Labyrinth labyrinth)
    {
        if (easyLabyrinths.Contains(labyrinth))
        {
            labyrinth.IsInUse = true;
            Debug.Log("Labyrinth in use again!!! HOORAY");
        }
    }
}
