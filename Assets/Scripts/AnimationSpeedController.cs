using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedController : MonoBehaviour
{
    [SerializeField]private Animator animator; // Reference to the Animator component
    //public float animationSpeed = 1.0f; // Default speed

    public void UpdateSpeed(float speed)
    {
        // Set the animator speed
        animator.speed = speed;
    }
}
