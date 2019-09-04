using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Control : MonoBehaviour
{
    public bool player = true;

    public GameObject barrier;
    public GameObject barrierSpawnLocation;

    [Range(0, 30)] public float runSpeed = 5.0f;

    public direction wantedDirection;

    private direction currentDirection;
    private float horizontal;
    private float vertical;
    private Rigidbody body;


    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(Input.GetAxisRaw("Horizontal") + " | " + Input.GetAxisRaw("Vertical"));
        if (player)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                wantedDirection = direction.Right;
            }

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                wantedDirection = direction.Left;
            }

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                wantedDirection = direction.Up;
            }

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                wantedDirection = direction.Down;
            }
        }
    }

    // Update is called once per frame at a fixed interval
    private void FixedUpdate()
    {
        if (CheckIfMouvementIsPossible())
        {
            if (currentDirection == direction.Up)
            {
                switch (wantedDirection)
                {
                    case direction.Right:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, 90);
                        break;
                    case direction.Left:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, -90);
                        break;
                }
            }

            if (currentDirection == direction.Down)
            {
                switch (wantedDirection)
                {
                    case direction.Right:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, -90);
                        break;
                    case direction.Left:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, 90);
                        break;
                }
            }

            if (currentDirection == direction.Right)
            {
                switch (wantedDirection)
                {
                    case direction.Up:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, -90);
                        break;
                    case direction.Down:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, 90);
                        break;
                }
            }

            if (currentDirection == direction.Left)
            {
                switch (wantedDirection)
                {
                    case direction.Up:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, 90);
                        break;
                    case direction.Down:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, -90);
                        break;
                }
            }


            currentDirection = wantedDirection;
        }

        body.velocity = CalculateVector();
        SpawnBarrier();
    }

    private bool CheckIfMouvementIsPossible()
    {
        switch (currentDirection)
        {
            case direction.Up when wantedDirection == direction.Down:
            case direction.Down when wantedDirection == direction.Up:
            case direction.Right when wantedDirection == direction.Left:
            case direction.Left when wantedDirection == direction.Right:
               // Debug.LogWarning("Illegal move : Opposite direction");
                return false;
            default:
                return true;
        }
    }

    private void SpawnBarrier()
    {
        Instantiate(barrier, barrierSpawnLocation.GetComponent<Transform>().position, Quaternion.identity);
    }

    private Vector3 CalculateVector()
    {
        switch (currentDirection)
        {
            case direction.Up:
                horizontal = 0;
                vertical = 1;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
            case direction.Down:
                horizontal = 0;
                vertical = -1;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
            case direction.Right:
                horizontal = 1;
                vertical = 0;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
            case direction.Left:
                horizontal = -1;
                vertical = 0;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}