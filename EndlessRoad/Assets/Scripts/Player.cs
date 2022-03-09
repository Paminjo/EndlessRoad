using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] Roads = new GameObject[3];

    private Vector2 startTouchPosition, endTouchPosition;

    private float[] Positions =
    {
        -1.7f,
        -0.55f,
        0.55f,
        1.7f
    };

    private int destination = 2;
    private bool destinationReached = true;
    private float carSpeed = 5.0f;

    private int Score = 0;
    private float Tank = 100;

    // Start is called before the first frame update
    private void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
    }

    // Update is called once per frame
    private void Update()
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
            if (carSpeed > 5f)
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coin"))
        {
            Score += 5;
            collider.gameObject.SetActive(false);
        }
        if (collider.gameObject.tag.Equals("Canister"))
        {
            Tank += 20;
            collider.gameObject.SetActive(false);
        }
        if (collider.gameObject.tag.Equals("StartLine"))
        {
            carSpeed++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.GetType().Equals(typeof(PolygonCollider2D)) && collision.gameObject.CompareTag("WallObstacle"))
        {
            foreach (GameObject Road in Roads)
            {
                Road.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;

            }
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else if (collision.otherCollider.GetType().Equals(typeof(BoxCollider2D)) && collision.gameObject.CompareTag("GroundObstacle"))
        {
            foreach(GameObject Road in Roads)
            {
                Road.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;

            }
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }   
}