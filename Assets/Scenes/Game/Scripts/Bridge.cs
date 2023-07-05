using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public int length;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"|| collision.gameObject.tag =="Enemy")
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
            collision.gameObject.transform.SetParent(transform.parent);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;
            collision.gameObject.transform.SetParent(null);
        }
    }
}
