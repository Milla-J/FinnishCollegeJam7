using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Labyrinth: MonoBehaviour
{
    public bool IsInUse;
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
            int random = Random.Range(0, places.Count);
            PossibleProblems[random].transform.position = t.position;
        }
    }

    public static int GetRandomIndex<T>(List<T> list)
    {
        var random = Random.Range(0, list.Count);
        return random;
    }
}
