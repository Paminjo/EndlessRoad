using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
    

    public GameObject StartRoad;
    public GameObject RoadA;
    public GameObject RoadB;
    public GameObject RoadC;
    public GameObject RoadD;

    public List<GameObject> ActiveRoads = new List<GameObject>();
    public List<GameObject> InactiveRoads = new List<GameObject>();

    float ActiveRoadMaxY = 0;
    public float Speed = 2;

    private Vector3 RoadGarbageCollector = new Vector3(15, 0, 0);

    // Start is called before the first frame update
    private void Start()
    {
        ActiveRoads.Add(RoadA);
        ActiveRoads.Add(RoadB);
        ActiveRoads.Add(StartRoad);

        InactiveRoads.Add(RoadC);
        InactiveRoads.Add(RoadD);
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var Road in ActiveRoads)
        {
            Road.transform.position = new Vector3(
                Road.transform.position.x, Road.transform.position.y - 0.1f * Speed * Time.fixedDeltaTime, 0);
        }
        if (StartRoad.transform.position.y < -16) // StartRoad Removing
        {
            ActiveRoads.Remove(StartRoad);
            StartRoad.transform.position = RoadGarbageCollector;
            StartRoad.SetActive(false);
        }

        if(ActiveRoads.Count == 3)
        {
            foreach(var Road in ActiveRoads)
            {
                if(Road.transform.position.y < -16)
                {
                    ActiveRoads.Remove(Road);
                    InactiveRoads.Add(Road);
                    Road.transform.position = RoadGarbageCollector;
                }
            }
        }

        if(ActiveRoads.Count < 3) // Adding a new Road
        {
            ActiveRoads.Add(InactiveRoads[Random.Range(0,InactiveRoads.Count-1)]);
            
            foreach (var Road in ActiveRoads)
            {
                if(Road.transform.position.y > ActiveRoadMaxY)
                {
                    ActiveRoadMaxY = Road.transform.position.y;
                }
                if(Road.transform.position.x != 0)
                {
                    Road.transform.position = new Vector3(0, ActiveRoadMaxY + 8, 0);                   
                }
                if(Road.Equals(InactiveRoads.Any()))
                {
                    InactiveRoads.Remove(Road);
                }
            }
        }
    }
}