using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -2)
        {
            transform.position = new Vector3(
                transform.position.x, transform.position.y + 0.1f * Time.deltaTime, transform.position.z);
        }
    }
}
