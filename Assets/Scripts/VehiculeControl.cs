using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculeControl : MonoBehaviour
{
    public GameObject MotoPrefab;

    public directionEnum direction;

    public enum directionEnum
    {
        Up,
        Down,
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(direction);

        if (Input.GetAxis("Horizontal") > 0)
        {
            direction = directionEnum.Right;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            direction = directionEnum.Left;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            direction = directionEnum.Up;
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            direction = directionEnum.Down;
        }
    }
}