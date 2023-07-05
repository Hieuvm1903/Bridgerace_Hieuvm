using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshRenderer rend;

    void Start()
    {
        
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
    public void changecolor(Color color)
    {
        rend.material.color = color;
    }
    public void faded()
    {
        rend.material.color = Color.gray;
    }
    public bool matchcol(Color col1, Color col2)
    {
        var r = Mathf.Abs(col1.r - col2.r);
        var g = Mathf.Abs(col1.g - col2.g);
        var b = Mathf.Abs(col1.b - col2.b);
        var a = Mathf.Abs(col1.a - col2.a);
        return Mathf.Sqrt(r * r + g * g + b * b + a * a) < 0.5f;

    }
}
