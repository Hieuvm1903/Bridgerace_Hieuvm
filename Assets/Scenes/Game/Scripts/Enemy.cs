using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
    }
}
