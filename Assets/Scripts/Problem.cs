using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Problem : MonoBehaviour
{
    public string targetTag;

    public Sprite newSprite;
    private SpriteRenderer spriteRenderer;

    private Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            Debug.Log("Fixed problem");
            DisableCollider();
            ChangeSprite(Color.blue);
        }
    }

    private void DisableCollider()
    {
        Debug.Log("Disabling collider");
        myCollider.enabled = false;
    }

    private void ChangeSprite(Color newColor)
    {
        Debug.Log("Changing sprite");
        spriteRenderer.color = newColor;
        //spriteRenderer.sprite = newSprite;
    }
}
