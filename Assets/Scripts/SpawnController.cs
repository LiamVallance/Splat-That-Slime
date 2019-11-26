﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] shapes;
    public GameObject[] lastShape;

    private GameObject shape;
    private int randShape;

    private int speed = 100;
    private int stage = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
            Spawn(i);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].tag == "SpawnRight")
            {
                if (lastShape[i].transform.position.x < 3.5)
                    Spawn(i);
            }
            else
                if (lastShape[i].transform.position.x > -3.5)
                    Spawn(i);
        }
    }

    void Spawn(int spawnPoint)
    {
        randShape = Random.Range(0, shapes.Length);
    
        shape = Instantiate(shapes[randShape], spawnPoints[spawnPoint].position, Quaternion.identity);

        if (spawnPoints[spawnPoint].tag == "SpawnRight")
            shape.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0));
        else
            shape.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));

        lastShape[spawnPoint] = shape;

    }
}