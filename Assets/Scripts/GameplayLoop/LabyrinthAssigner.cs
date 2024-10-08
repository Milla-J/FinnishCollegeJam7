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

    private System.Random rng = new(); // used for shuffling lists

    public Labyrinth GetAvailableLabyrinth(DifficultyLevels difficulty)
    {
        switch (difficulty)
        {
            case DifficultyLevels.Easy:
                ShuffleList(easyLabyrinths); //Shuffling for randomness
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

    private void ShuffleList(List<Labyrinth> list) 
    {
        var shuffledcards = list.OrderBy(_ => rng.Next()).ToList();
    }

    //public IEnumerator GetAvailableLabyrinth(DifficultyLevels difficulty, ref Labyrinth labyrinth)
    //{
    //    labyrinth = null;

    //    switch(difficulty)
    //    {
    //        case DifficultyLevels.Easy:
    //            var random = Labyrinth.GetRandomIndex(easyLabyrinths);
    //            labyrinth = easyLabyrinths[random];
    //            labyrinth.UpdateProblems(easyProblemsAmount);
    //            labyrinth.IsInUse = true;
    //            yield break;
    //        default:
    //            yield break;

    //    }
    //}

    //private Labyrinth GetLabyrinth()
    //{

    //}
    //private IEnumerator DigForlabyrinth()
    //{
    //    while (true)
    //    {
    //        if (TryGetLabyrinth(out Labyrinth newLabyrinth))
    //        {
    //            yield break;
    //        }
    //    }
    //}
    //private bool TryGetLabyrinth(out Labyrinth labyrinth)
    //{
    //    if(this.previousLabyrinth!= null)
    //    {
    //        var random = Labyrinth.GetRandomIndex(easyLabyrinths);
    //        Labyrinth newLabyrinth = easyLabyrinths[random];
    //        if (this.previousLabyrinth.ID != newLabyrinth.ID)
    //        {
    //            labyrinth = newLabyrinth;
    //            return true;
    //        }
    //        else
    //        {
    //            labyrinth = null;
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        var random = Labyrinth.GetRandomIndex(easyLabyrinths);
    //        Labyrinth newLabyrinth = easyLabyrinths[random];
    //    }
    //}
}
