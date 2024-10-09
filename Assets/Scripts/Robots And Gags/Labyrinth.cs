using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Labyrinth: MonoBehaviour
{
    [HideInInspector] public bool IsInUse;

    public event Action<Labyrinth> OnCeasedToBeUsed;

    [SerializeField] private List<ProblemPlace> _ProblemsPlaces;
    [SerializeField] private List<Problem> _ProblemsPrefabs;
    [SerializeField] private Transform _ProblemsParent;

    private List<Problem> InstantiatedProblems = new();

    private Problem previousProblem;
    private int randomIndex;

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    public void UpdateProblems(int problemsCount)
    {
        DeleteCurrentProblems();
        UsefulStuff.ShuffleList(_ProblemsPlaces); //Shuffle places for randomness

        for (int i = 0; i < problemsCount; i++)  // As much as problemsCount parameter
        {
            foreach(var place in _ProblemsPlaces) 
            {
                if (!place.InUse) //Stops on an available place 
                {
                    place.InUse = true;
                    var index = UsefulStuff.GetRandomIndex(_ProblemsPrefabs); //Chooses random problem
                    Problem newProblem = Instantiate(_ProblemsPrefabs[index], place.transform.position, place.transform.rotation, _ProblemsParent);
                    InstantiatedProblems.Add(newProblem);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Should be called from robot when it stops using this Labyrinth
    /// </summary>
    public void RemoveFromUse()
    {
        OnCeasedToBeUsed?.Invoke(this);
    }

    private void DeleteCurrentProblems()
    {
        foreach(var problem in InstantiatedProblems)
        {
            Destroy(problem.gameObject);
        }
        InstantiatedProblems.Clear();
    }
}
