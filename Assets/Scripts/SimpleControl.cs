using System;
using UnityEngine;

public class SimpleControl : MonoBehaviour
{
    public GameObject Barrier;
    public GameObject BarrierSpawnLocation;
    
   
    [Range(0, 30)] public float runSpeed = 5.0f;
    
    [SerializeField]
    private direction wantedDirection;
    [SerializeField]
    private direction currentDirection;
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
    void FixedUpdate()
    {
        if (checkIfMouvementIsPossible())
        {
            currentDirection = wantedDirection;
        }
        body.velocity = CalculateVector();
        SpawnBarrier();
    }

    bool checkIfMouvementIsPossible()
    {
         switch (currentDirection)
        {
            case direction.up when wantedDirection == direction.down:
            case direction.down when wantedDirection == direction.up:
            case direction.right when wantedDirection == direction.left:
            case direction.left when wantedDirection == direction.right:
                Debug.LogWarning("Ilegal move : Opposite direction");
                return false;
            default:
                return true;
        }
        
    }
    /*void CalculateRotation()
    {
        switch (currentDirection)
        {
            case direction.up:
                this.GetComponent<Transform>().Rotate(Vector3.up,90);
                break;
            case direction.down:
                break;
            case direction.right:
                break;
            case direction.left:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }*/

    void SpawnBarrier()
    {
        Instantiate(Barrier, BarrierSpawnLocation.GetComponent<Transform>().position, Quaternion.identity);
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