using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public Collider col;
    public MeshRenderer mesh;
    const string LAST = "last";
    const string PLAYER = "Player";
    const string ENEMY = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER) || other.CompareTag(ENEMY))
        {
            Character character = other.GetComponent<Character>();
            if (!CompareTag(LAST))
                OpenDoor(character);

        }                
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER) || other.CompareTag(ENEMY))
        {
            Character character = other.GetComponent<Character>();
            if (CompareTag(LAST))            
                CloseDoor(character);
            
        }
    }  
    public void OpenDoor(Character character)
    {        
        Physics.IgnoreCollision(character.collid, col,true);
        
    }
    public void CloseDoor(Character character)
    {
        Physics.IgnoreCollision(character.collid, col, false);

    }
    public void ChangeColor(Color color)
    {
        mesh.material.color = color;
    }
    
}
