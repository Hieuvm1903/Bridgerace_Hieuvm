using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camfl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset;
    public float val = 100;
    public float dist = 2.5f;
    Transform tf ;
    void Start()
    {        
        tf = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        
        Vector3 pos = target.position + offset*dist;
        tf.position = Vector3.Lerp(tf.position, pos, val * Time.deltaTime);
        
    }
 
}
