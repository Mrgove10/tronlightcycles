using System;
using UnityEngine;
using Random = System.Random;

public class RandomAI : MonoBehaviour
{
    private Control controller;
    private Random rand;

    public int timeSinceChoice = 0;
    [Range(1, 100)] public int choiceEveryFrame = 15;

    private void Start()
    {
        controller = this.GetComponent<Control>();
        rand = new Random();
    }

    private void FixedUpdate()
    {
        timeSinceChoice++;
        if (timeSinceChoice == choiceEveryFrame)
        {
            controller.wantedDirection = (direction) rand.Next(0, 3);
            timeSinceChoice = 0;
        }
    }
}