using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInit : MonoBehaviour
{
    public GameObject column;
    public int columnQuantity = 10;
    public float columnWidth = 100;

    
    void Start()
    {
        Vector3 spawnCoordinates = new Vector3();
        for(int i = 0; i < columnQuantity; i++)
        {
            spawnCoordinates.y = Random.Range(0, 1000 - columnWidth); // planet size (1000) - column size
            spawnCoordinates.z = Random.Range(-1000 + columnWidth, 0);
            Instantiate(column, spawnCoordinates, Quaternion.identity);
            // The Quarternion.identity thing is from https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
        }
    }

    //void Update()
    //{
        
    //}
}
