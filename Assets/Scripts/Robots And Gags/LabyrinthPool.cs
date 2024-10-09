using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System.Linq;

public class LabyrinthPool : MonoBehaviour
{
    [SerializeField] private GameplayLoopController _GameplayLoopManager;

    [SerializeField] private List<Labyrinth> _EasyPool;
    [SerializeField] private int _EasyProblemsAmount;

    [SerializeField] private List<Labyrinth> _NormalPool;
    [SerializeField] private int _NormalProblemsAmount;

    [SerializeField] private List<Labyrinth> _HardPool;
    [SerializeField] private int _HardProblemsAmount;

    bool _isLabyrinthFound;

    public Labyrinth GetAvailableLabyrinth()
    {
        switch (_GameplayLoopManager.DifficultyLevel)
        {
            case DifficultyLevels.Easy:
                UsefulStuff.ShuffleList(_EasyPool); //Shuffling for randomness

                foreach (var labyr in _EasyPool)
                {
                    if(labyr.IsInUse == false)
                    {
                        labyr.UpdateProblems(_EasyProblemsAmount);
                        labyr.IsInUse = true;
                        labyr.OnNoLongerUsed += RestoreUse;
                        return labyr;
                    }
                }
                return null;

            case DifficultyLevels.Normal:
                UsefulStuff.ShuffleList(_EasyPool); //Shuffling for randomness

                foreach (var labyr in _NormalPool)
                {
                    if (labyr.IsInUse == false)
                    {
                        labyr.UpdateProblems(_NormalProblemsAmount);
                        labyr.IsInUse = true;
                        labyr.OnNoLongerUsed += RestoreUse;
                        return labyr;
                    }
                }
                return null;

            case DifficultyLevels.Hard:
                UsefulStuff.ShuffleList(_HardPool); //Shuffling for randomness

                foreach (var labyr in _HardPool)
                {
                    if (labyr.IsInUse == false)
                    {
                        labyr.UpdateProblems(_HardProblemsAmount);
                        labyr.IsInUse = true;
                        labyr.OnNoLongerUsed += RestoreUse;
                        return labyr;
                    }
                }
                return null;
        }
        return null;
    }
    private void RestoreUse(Labyrinth labyrinth)
    {
         labyrinth.IsInUse = true;
         Debug.Log("Labyrinth in use again!!! HOORAY");
    }
}
