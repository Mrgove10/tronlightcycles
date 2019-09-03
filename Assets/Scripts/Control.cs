using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Control : MonoBehaviour
{
    public GameObject barrier;
    public GameObject barrierSpawnLocation;
    
    [Range(0, 30)] public float runSpeed = 5.0f;

    [SerializeField] private direction wantedDirection;
    [SerializeField] private direction currentDirection;
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

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            wantedDirection = direction.right;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            wantedDirection = direction.left;
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            wantedDirection = direction.up;
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            wantedDirection = direction.down;
        }
    }

    // Update is called once per frame at a fixed interval
    private void FixedUpdate()
    {
        if (CheckIfMouvementIsPossible())
        {
            if (currentDirection == direction.up )
            {
                switch (wantedDirection)
                {
                    case direction.right:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, 90);
                        break;
                    case direction.left:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, -90);
                        break;
                }
            }
            if (currentDirection == direction.down )
            {
                switch (wantedDirection)
                {
                    case direction.right:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, -90);
                        break;
                    case direction.left:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, 90);
                        break;
                }
            }
            if (currentDirection == direction.right )
            {
                switch (wantedDirection)
                {
                    case direction.up:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, -90);
                        break;
                    case direction.down:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, 90);
                        break;
                }
            }
            if (currentDirection == direction.left )
            {
                switch (wantedDirection)
                {
                    case direction.up:
                        transform.RotateAround(barrierSpawnLocation.transform.position, Vector3.up, 90);
                        break;
                    case direction.down:
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
            case direction.up when wantedDirection == direction.down:
            case direction.down when wantedDirection == direction.up:
            case direction.right when wantedDirection == direction.left:
            case direction.left when wantedDirection == direction.right:
                Debug.LogWarning("Illegal move : Opposite direction");
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
            case direction.up:
                horizontal = 0;
                vertical = 1;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
            case direction.down:
                horizontal = 0;
                vertical = -1;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
            case direction.right:
                horizontal = 1;
                vertical = 0;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
            case direction.left:
                horizontal = -1;
                vertical = 0;
                return new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}