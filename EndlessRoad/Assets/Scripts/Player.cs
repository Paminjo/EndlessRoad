using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;

    float[] Positions =
    {
        -1.7f,
        -0.55f,
        0.55f,
        1.7f
    };

    int destination = 2;
    bool destinationReached = true;
    float carSpeed = 5.0f;

    int Score = 0;
    float Tank = 100;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x.Equals(Positions[0]))
            {
                destination = 1;
                destinationReached = false;
            }
            if (transform.position.x.Equals(Positions[1]))
            {
                destination = 2;
                destinationReached = false;
            }
            if (transform.position.x.Equals(Positions[2]))
            {
                destination = 3;
                destinationReached = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x.Equals(Positions[3]))
            {
                destination = 2;
                destinationReached = false;
            }
            if (transform.position.x.Equals(Positions[2]))
            {
                destination = 1;
                destinationReached = false;
            }
            if (transform.position.x.Equals(Positions[1]))
            {
                destination = 0;
                destinationReached = false;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (carSpeed <= 10f)
                carSpeed++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if(carSpeed > 5f)
                carSpeed--;
        }
    }
    private void FixedUpdate()
    {
        if (Positions[destination].Equals(transform.position.x))
        {
            destinationReached = true;
        }
        if (!destinationReached)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, new Vector2(
                    Positions[destination], transform.position.y), carSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Coin"))
        {
            Score += 5;
            collider.gameObject.SetActive(false);
        }
        if(collider.gameObject.tag.Equals("Canister"))
        {
            Tank += 20;
            collider.gameObject.SetActive(false);
        }
        if (collider.gameObject.tag.Equals("StartLine"))
        {
            carSpeed++;

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            
        }
    }
}
