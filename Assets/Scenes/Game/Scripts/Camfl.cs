using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camfl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset;
    public float val = 100;
    public float dist = 1.5f;
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        settarget(player.transform);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        
        Vector3 pos = target.position + offset*dist;
        transform.position = Vector3.Lerp(transform.position, pos, val * Time.deltaTime);
        
    }
    public void settarget(Transform target)
    {
        this.target = target.transform;
    }
}
