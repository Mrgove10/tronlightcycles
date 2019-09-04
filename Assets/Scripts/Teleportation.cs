using System;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public int topLimit = 400;
    public int botLimit = -400;
    public int rightLimit = 400;
    public int leftLimit = -400;
    public GameObject PlayerCycle;
    public GameObject AICycle;

    private void Update()
    {
        if (PlayerCycle.transform.position.z > topLimit)
        {
            Debug.Log("toplimite");
            PlayerCycle.GetComponent<Transform>().TransformPoint(new Vector3(PlayerCycle.transform.position.x, PlayerCycle.transform.position.y, botLimit));
        }

        /*   foreach (var wall in ListWall)
           {
               if (wall.name == "Left Wall")
               {
                   wall.GetComponent<Wall>().ColliderGM.GetComponent<Transform>().position = new Vector3(0,0,0);
               }
               Debug.Log(wall.name + " | " + wall.GetComponent<Wall>().Collided + " | " + wall.GetComponent<Wall>().CollisionPos);
           }*/
    }
}