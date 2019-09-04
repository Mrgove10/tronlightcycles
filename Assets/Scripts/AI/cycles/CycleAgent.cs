using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class CycleAgent : Agent
{
    Rigidbody rBody;
    public Transform Target;
    
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
    
    public override void CollectObservations()
    {
        // Target and Agent positions
        AddVectorObs(transform.position);
        AddVectorObs(Target.transform.position);

        // Agent velocity
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.gameObject.CompareTag("Barrier"))
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Touched a barrier, end of game");
            AgentReset();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Touched a Wall, end of game");
            AgentReset();
        }
    }

    public override void AgentReset()
    {
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.position = new Vector3( 0, 0.5f, 0);
    }
}