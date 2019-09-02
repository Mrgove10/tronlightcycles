using System;
using UnityEngine;

public class SimpleControl : MonoBehaviour
{
    
    public direction currentDirection;
    [Range(0, 30)] public float runSpeed = 5.0f;

    private float horizontal;
    private float vertical;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxisRaw("Horizontal") + " | " + Input.GetAxisRaw("Vertical"));

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            currentDirection = direction.right;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            currentDirection = direction.left;
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            currentDirection = direction.up;
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            currentDirection = direction.down;
        }
        
        Debug.Log(currentDirection);
    }

    // Update is called once per frame at a fixed interval
    void FixedUpdate()
    {
        body.velocity = CalculateVector();
    }

    Vector3 CalculateVector()
    {
        switch (currentDirection)
        {
            case direction.up:
                horizontal = 0;
                vertical = 1;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
                break;
            case direction.down:
                horizontal = 0;
                vertical = -1;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
                break;
            case direction.right:
                horizontal = 1;
                vertical = 0;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
                break;
            case direction.left:
                horizontal = -1;
                vertical = 0;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}