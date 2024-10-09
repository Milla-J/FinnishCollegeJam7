using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System.Linq;

public class LabyrinthAssigner : MonoBehaviour
{
    [SerializeField] private List<Labyrinth> _EasyLabyrinths;
    [SerializeField] private int _EasyProblemsAmount;

    [SerializeField] private List<Labyrinth> _NormalLabyrinths;
    [SerializeField] private int _NormalProblemsAmount;

    [SerializeField] private List<Labyrinth> _HardLabyrinths;
    [SerializeField] private int _HardProblemsAmount;

    bool _isLabyrinthFound;

    public Labyrinth GetAvailableLabyrinth()
    {
        switch (GameplayLoopManager.Instance.DifficultyLevel)
        {
            case DifficultyLevels.Easy:
                UsefulStuff.ShuffleList(_EasyLabyrinths); //Shuffling for randomness

                foreach (var labyr in _EasyLabyrinths)
                {
                    if(labyr.IsInUse == false)
                    {
                        labyr.UpdateProblems(_EasyProblemsAmount);
                        labyr.IsInUse = true;
                        labyr.OnCeasedToBeUsed += RestoreUse;
                        return labyr;
                    }
                }
                return null;

            case DifficultyLevels.Normal:
                UsefulStuff.ShuffleList(_EasyLabyrinths); //Shuffling for randomness

                foreach (var labyr in _NormalLabyrinths)
                {
                    if (labyr.IsInUse == false)
                    {
                        labyr.UpdateProblems(_NormalProblemsAmount);
                        labyr.IsInUse = true;
                        labyr.OnCeasedToBeUsed += RestoreUse;
                        return labyr;
                    }
                }
                return null;

            case DifficultyLevels.Hard:
                UsefulStuff.ShuffleList(_HardLabyrinths); //Shuffling for randomness

                foreach (var labyr in _HardLabyrinths)
                {
                    if (labyr.IsInUse == false)
                    {
                        labyr.UpdateProblems(_HardProblemsAmount);
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
        if (_EasyLabyrinths.Contains(labyrinth))
        {
            labyrinth.IsInUse = true;
            Debug.Log("Labyrinth in use again!!! HOORAY");
        }
        else
        {
            Debug.LogWarning("Labyrinth mistaaake!!");
        }
    }
}
