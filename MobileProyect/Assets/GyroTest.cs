using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTest : MonoBehaviour
{

    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.gravity = Input.gyro.gravity*45;       
        //body.AddForce(new Vector2(Input.gyro.userAcceleration.x*50, Input.gyro.userAcceleration.y*50));
        body.AddForce(new Vector2(Input.acceleration.x*50, Input.acceleration.y*50));
    }
}
