using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    public bool moving = true;

    float speed = 5.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	void Update () 
    {
        if ( moving )
            Movement();

        MovementCheck();
	}

    public void SetMoving (bool val)
    {
        moving = val;
    }

    void Movement()
    {
        moving = false;

        if ( Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if ( Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if ( Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if ( Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            moving = true;
        }

    }

    void MovementCheck()
    {
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            moving = false;
        else
            moving = true;
    }
}
