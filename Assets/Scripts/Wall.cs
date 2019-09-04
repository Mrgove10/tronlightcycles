using System;
using UnityEditor;
using UnityEngine;


public class Wall : MonoBehaviour
{
    public bool Collided ;
    public Transform CollisionPos;    
    [SerializeField] public GameObject ColliderGM;
    
    void OnTriggerEnter(Collider other)
    {
        CollisionPos = other.transform;
        ColliderGM = other.gameObject;
        Collided = true;
    }

    void OnTriggerExit(Collider other)
    {
        CollisionPos = null;
        ColliderGM = null;
        Collided = false;
    }
}