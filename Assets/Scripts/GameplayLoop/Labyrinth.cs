using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Labyrinth: MonoBehaviour
{
    public bool IsInUse;
    public event Action<Labyrinth> OnCeasedToBeUsed;
    [SerializeField] private List<Transform> possibleProblemPlaces;
    [SerializeField] private List<Problem> PossibleProblems;

    private Problem previousProblem;
    private int randomIndex;

    public void UpdateProblems(int problemsCount)
    {
        List <Transform> places = new List <Transform>();

        for (int i = 0; i < problemsCount; i++)
        {
            GetRandomIndex(possibleProblemPlaces);
            places.Add(possibleProblemPlaces[i]);
        }
        
        foreach(Transform t in places)
        {
            int random = UnityEngine.Random.Range(0, places.Count);
            PossibleProblems[random].transform.position = t.position;
        }
    }

    /// <summary>
    /// Should be called from robot when it stops using this Labyrinth
    /// </summary>
    public void RemoveFromUse()
    {
        OnCeasedToBeUsed?.Invoke(this);
    }

    public static int GetRandomIndex<T>(List<T> list)
    {
        var random = UnityEngine.Random.Range(0, list.Count);
        return random;
    }
}
