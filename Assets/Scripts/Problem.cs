using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class Problem : MonoBehaviour
{
    private GameManager gameManager;
    public event Action<Problem> OnProblemFixed;
    [SerializeField] private string targetTag;

    [SerializeField] private Sprite newSprite;
     private SpriteRenderer spriteRenderer;

    // Reference to the child object's SpriteRenderer component
    private SpriteRenderer childSpriteRenderer;

    private Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        myCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Transform child = this.gameObject.transform.GetChild(0);

        if (child != null )
        {
            childSpriteRenderer = child.GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            //Debug.Log("Fixed problem");
            OnProblemFixed?.Invoke(this);
            ChangeSprite(Color.blue);
            gameManager.StopLowerSatisfaction();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            DisableCollider();
            gameManager.UnlockLowerSatisfaction();
        }
    }

    private void DisableCollider()
    {
        Debug.Log("Disabling collider");
        myCollider.enabled = false;
    }

    private void ChangeSprite(Color newColor)
    {
        //Debug.Log("Changing sprite");
        if (newSprite != null)
        {
            childSpriteRenderer.sprite = newSprite;
        }
        else
        {
            spriteRenderer.color = newColor;
        }
    }
}
