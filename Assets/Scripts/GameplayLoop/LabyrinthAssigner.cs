using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DifficultyLevels
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

    public IEnumerator GetAvailableLabyrinth(DifficultyLevels difficulty, ref Labyrinth labyrinth)
    {
        labyrinth = null;

        switch(difficulty)
        {
            case DifficultyLevels.Easy:
                var random = Labyrinth.GetRandomIndex(easyLabyrinths);
                labyrinth = easyLabyrinths[random];
                labyrinth.UpdateProblems(easyProblemsAmount);
                labyrinth.IsInUse = true;
                yield break;
            default:
                yield break;

        }
    }

    private Labyrinth GetLabyrinth()
    {

    }
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
