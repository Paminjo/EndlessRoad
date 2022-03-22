using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject[] Roads = new GameObject[3];
    public Canvas LosingScreen;
    public Slider FuelBar;

    //Car Movement
    private float[] Positions =
    {
        -1.7f,
        -0.55f,
        0.55f,
        1.7f
    };

    private int destination = 2;
    private bool destinationReached = true;
    private float carSpeed = 2.0f;

    //UI Data - Player info
    private Timer ScoreTimer = new Timer(1000);

    public Text ScoreText;
    private Transform ActualScoreText;
    private int Score = 0;
    private float FuelLevel = 100;
    private int ScorePerUnit = 5;
    private int SpeedIncreaseToken = 1;

    //Data for Touch detection
    //public const float MinSwipeDistance = 0.08f;
    private Vector2 startTouchPosition, endTouchPosition;

    public static bool IsUserInputEnabled = true;

    // Start is called before the first frame update
    private void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        LosingScreen.enabled = false;

        Score = 0;
        ScorePerUnit = 1;

        IsUserInputEnabled = true;

        ScoreTimer.Elapsed += new ElapsedEventHandler(TimerTick);
        ScoreTimer.Start();
    }

    private void TimerTick(object sender, ElapsedEventArgs e)
    {
        Score += ScorePerUnit;
        FuelLevel -= 5;
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsUserInputEnabled)
        {
            UserInput();
        }

        if (FuelLevel == 0)
        {
            UserLost();
        }

        ScoreText.text = Score.ToString();

        if (Score / 100 >= SpeedIncreaseToken)
        {
            ScorePerUnit += 5;
            foreach (var Road in Roads)
            {
                Road.gameObject.GetComponent<Rigidbody2D>().velocity *= 1.5f;
            }
            SpeedIncreaseToken++;
        }
        if (FuelLevel > 100)
        {
            FuelLevel = 100;
        }
        FuelBar.value = FuelLevel;
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
                    Positions[destination], transform.position.y), carSpeed * 0.1f); //Time.deltatime removed cause it is shit
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coin"))
        {
            Score += 10;
        }
        if (collider.gameObject.tag.Equals("Canister"))
        {
            FuelLevel += 20;
        }
        if (collider.gameObject.tag.Equals("StartLine"))
        {
            carSpeed++;
        }
        collider.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.GetType().Equals(typeof(PolygonCollider2D))
            && collision.gameObject.CompareTag("WallObstacle"))
        {
            UserLost();
        }
        else if (collision.otherCollider.GetType().Equals(typeof(BoxCollider2D))
            && collision.gameObject.CompareTag("GroundObstacle"))
        {
            UserLost();
        }
    }

    private void UserLost()
    {
        IsUserInputEnabled = false;

        foreach (GameObject Road in Roads)
        {
            Road.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }

        ScoreTimer.Stop();
        LosingScreen.enabled = true;
        ActualScoreText = LosingScreen.transform.Find("ActualScore");
        ActualScoreText.GetComponent<Text>().text = Score.ToString();
        ScoreText.GetComponentInParent<Canvas>().enabled = false;
    }

    private void UserInput()
    {
        //touch swipe input
        if (Input.touchCount > 0
            && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount == 1
            && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.x < startTouchPosition.x)
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
            else if (endTouchPosition.x > startTouchPosition.x)
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
            else if (true)
            {
            }
        }

        //Key input
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
}