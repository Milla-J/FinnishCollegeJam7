using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemPlace : MonoBehaviour
{
    public bool InUse { get; set; }
    private void Awake()
    {
        InUse = false;
    }
}
