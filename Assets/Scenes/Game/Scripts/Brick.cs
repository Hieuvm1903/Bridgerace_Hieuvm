using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshRenderer rend;
    public Transform tf;

    public void ChangeColor(Color color)
    {
        rend.material.color = color;
    }
    public void Faded()
    {              
        rend.material.color = Color.gray;
        GetComponent<BoxCollider>().isTrigger = false;
        StartCoroutine(ITrigger());
    }
    public void OnDespawn()
    {
  
        Pooling.Instance.BackToPool(this);
        //Destroy(gameObject);
    }
    IEnumerator ITrigger()
    {
        
        
        yield return new WaitForSeconds(2f);
        GetComponent<BoxCollider>().isTrigger = true;
    }

    


}
