using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AgentController : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    //[SerializeField] private List<GameObject> customers;
    [SerializeField] private GameObject customer;
    [SerializeField] private Slider passerSlider;
    [SerializeField] private Text passerCount;

    private float spawnSpeed;
    private float spawnTimer = 0;
    private Transform entry, exit;
    void Start()
    {
        
    }

    
    void Update()
    {

        spawnSpeed = passerSlider.value;
        passerCount.text = spawnSpeed.ToString();
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SelectPath(out entry, out exit);
            Debug.Log(entry.lossyScale.z);

            float spawnRange = entry.lossyScale.z / 2;
            float entrySpawnZ = Random.Range(-spawnRange, spawnRange);
            Vector3 entryPoint;
            if (entry.rotation.eulerAngles.y == 0)
                entryPoint = new Vector3(entry.position.x, entry.position.y, entry.position.z + entrySpawnZ);
            else
                entryPoint = new Vector3(entry.position.x + entrySpawnZ, entry.position.y, entry.position.z);

            

            Vector3 exitPoint;
            if (entry.rotation.eulerAngles.y == 0)
                exitPoint = new Vector3(exit.position.x, exit.position.y, exit.position.z + entrySpawnZ);
            else
                exitPoint = new Vector3(exit.position.x + entrySpawnZ, exit.position.y, exit.position.z);

            GameObject visitor = Instantiate(customer, entryPoint, Quaternion.identity);
            visitor.GetComponent<CustomerNavMesh>().setDestination(exitPoint);
            spawnTimer = 60/spawnSpeed;
        }
    }

    private void SelectPath(out Transform entry, out Transform exit)
    {
        
        int entryIndex = Random.Range(0, spawnPoints.Count);
        int exitIndex = Random.Range(0, spawnPoints.Count);

        if (entryIndex == exitIndex && entryIndex == spawnPoints.Count - 1)
            entryIndex = 0;
        else if (entryIndex == exitIndex)
            entryIndex++;

        entry = spawnPoints[entryIndex];
        exit = spawnPoints[exitIndex];
        //Debug.Log(entryIndex + "  " + exitIndex);
        
    }

    
}
