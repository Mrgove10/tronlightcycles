using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Control : MonoBehaviour
{
    public bool player = true;

    public GameObject barrier;
    public GameObject barrierSpawnLocation;
    public GameObject BarrierContainer;


    [Range(0, 30)] public float speed = 5.0f;

    public direction wantedDirection;
    private Collider collider;
    private direction currentDirection;
    private float horizontal;
    private float vertical;
    private Rigidbody body;


    // Start is called before the first frame update
    private void Start()
    {
        collider = GetComponent<Collider>();
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
        Instantiate(barrier, barrierSpawnLocation.GetComponent<Transform>().position, Quaternion.identity,
            BarrierContainer.transform);
    }

    private Vector3 CalculateVector()
    {
        switch (currentDirection)
        {
            case direction.Up:
                horizontal = 0;
                vertical = 1;
                return new Vector3(horizontal * speed, 0, vertical * speed);
            case direction.Down:
                horizontal = 0;
                vertical = -1;
                return new Vector3(horizontal * speed, 0, vertical * speed);
            case direction.Right:
                horizontal = 1;
                vertical = 0;
                return new Vector3(horizontal * speed, 0, vertical * speed);
            case direction.Left:
                horizontal = -1;
                vertical = 0;
                return new Vector3(horizontal * speed, 0, vertical * speed);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.gameObject.CompareTag("Barrier"))
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Touched a barrier, end of game");
            destroyPlayer();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Touched a Wall, end of game");
            destroyPlayer();
        }
    }


    private void destroyPlayer()
    {
        Destroy(gameObject);
        Destroy(BarrierContainer);
    }
}