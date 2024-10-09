using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Labyrinth: MonoBehaviour
{
    [HideInInspector] public bool IsInUse;

    public event Action<Labyrinth> OnNoLongerUsed;

    [SerializeField] private List<ProblemPlace> _ProblemsPlaces;
    [SerializeField] private List<Problem> _ProblemsPrefabs;
    [SerializeField] private Transform _ProblemsParent;

    private List<Problem> InstantiatedProblems = new();

    private float _problemsFixed;
    private float _problemsAmount;
    public float FinalFactor { get; private set; } //Final completion factor

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);


    /// <summary>
    /// Prepares new labyrinth variant
    /// </summary>
    /// <param name="problemsCount"></param>
    public void UpdateProblems(int problemsCount)
    {
        DeleteCurrentProblems();
        _problemsAmount = problemsCount;
        _problemsFixed = 0;

        UsefulStuff.ShuffleList(_ProblemsPlaces); //Shuffle places for randomness

        for (int i = 0; i < problemsCount; i++)  // As much as problemsCount parameter
        {
            foreach(var place in _ProblemsPlaces) 
            {
                if (!place.InUse) //Stops on an available place 
                {
                    place.InUse = true;
                    var index = UsefulStuff.GetRandomIndex(_ProblemsPrefabs); //Chooses random problem
                    Problem newProblem = Instantiate(_ProblemsPrefabs[index], place.transform.position, place.transform.rotation, _ProblemsParent); //Instantiating problems
                    newProblem.OnProblemFixed += UpdateCompletionPercentage;
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
        OnNoLongerUsed?.Invoke(this);
    }

    private void DeleteCurrentProblems()
    {
        foreach(var problem in InstantiatedProblems)
        {
            Destroy(problem.gameObject);
        }
        InstantiatedProblems.Clear();
    }

    private void UpdateCompletionPercentage(Problem problemFixed)
    {
        problemFixed.OnProblemFixed -= UpdateCompletionPercentage;
        _problemsFixed++;
        FinalFactor = _problemsFixed /_problemsAmount;
    }
}
