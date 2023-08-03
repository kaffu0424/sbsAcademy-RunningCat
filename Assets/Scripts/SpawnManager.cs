using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] walls;
    public float StartTime, betweenTime;

    private void Start()
    {
        InvokeRepeating("SpawnWalls", StartTime, betweenTime);
    }
    
    public void SpawnWalls()
    {
        int randomResult = Random.Range(0, walls.Length);
        Instantiate(walls[randomResult]);
    }
}
