using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    Transform Player;
    float distance;
    public float maxDistance;
    public Transform turretHead, spawn1, spawn2;
    public GameObject laser;
    public float projectileSpeed = 1000f;
    public float fireRate, nextFire;
    GameObject clone;
    bool lastSpawn1 = false;
    Vector3 spawnPoint;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.position, transform.position);
        if(distance <= maxDistance)
        {
            turretHead.LookAt(Player);
            if(Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                if (lastSpawn1)
                {
                    spawnPoint = spawn1.position;
                }
                else
                {
                    spawnPoint = spawn2.position;
                }
                Shoot();
            }
        }
    }
    void Shoot()
    {
        clone = Instantiate(laser, spawnPoint, turretHead.rotation);
        clone.GetComponent<Rigidbody>().AddForce(turretHead.forward * projectileSpeed);
        audioSource.Play();
        lastSpawn1 = !lastSpawn1;
        Destroy(clone, 25);
    }
}
