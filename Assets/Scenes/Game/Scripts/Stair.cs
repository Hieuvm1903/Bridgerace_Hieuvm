using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField]GameObject door;
    public Collider col;
    public MeshRenderer mesh;
    private void Start()
    {
        col = door.GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
        for(int i = 0; i<Lvlmanager.Instance.listofcharacters.Count;i++)
        {
            OpenDoor(Lvlmanager.Instance.listofcharacters[i]);
        }
    }
    // Start is called before the first frame update
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
    private void OnTriggerEnter(Collider other)
    {
        if(!CompareTag("last"))
        if(other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Character character = other.GetComponent<Character>();
            OpenDoor(character);
        }
    }
}
