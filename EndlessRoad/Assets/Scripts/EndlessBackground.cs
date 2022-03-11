using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    public BoxCollider2D collider2D;
    public Rigidbody2D rigidbody2D;
    
    public GameObject InteractiveObjects;

    Transform[] children;
    private float height;
    private float scrollSpeed = -2.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        height = collider2D.size.y;
        collider2D.enabled = false;
        rigidbody2D.velocity = new Vector2(0, scrollSpeed);
        children = InteractiveObjects.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < - height)
        {
            ResetRoadTile();
        }
    }

    private void ResetRoadTile()
    {
        Vector2 resetPosition = new Vector2(0, height * 3f);
        transform.position = (Vector2)transform.position + resetPosition;
        
        foreach (var child in children)
        {
            child.gameObject.SetActive(true);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(true)
        {
            scrollSpeed -= 100;
        }
    }
}
