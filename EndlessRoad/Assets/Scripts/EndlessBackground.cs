using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    public BoxCollider2D collider2D;
    public Rigidbody2D rigidbody2D;

    private float height;
    private float scrollSpeed = -3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        height = collider2D.size.y;
        collider2D.enabled = false;

        rigidbody2D.velocity = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < - height)
        {
            Vector2 resetPosition = new Vector2(0, height * 2f);
            transform.position = (Vector2)transform.position + resetPosition;
        }
    }
}
