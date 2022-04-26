using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AgentController : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    //[SerializeField] private List<GameObject> customers;
    [SerializeField] private GameObject customer;
    [SerializeField] private float spawnInterval;

    private float spawnTimer = 0;
    private Transform entry, exit;
    void Start()
    {
        
    }

    
    void Update()
    {
  
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
        
            SelectPath(out entry, out exit);
            GameObject visitor = Instantiate(customer, entry.position, Quaternion.identity);
            visitor.GetComponent<CustomerNavMesh>().setDestination(exit.position);
            spawnTimer = spawnInterval;
        }
    }

    private void SelectPath(out Transform entry, out Transform exit)
    {
        Random random = new Random();
        int entryIndex = random.Next(0, spawnPoints.Count);
        int exitIndex = random.Next(0, spawnPoints.Count);
        while(exitIndex == entryIndex)
        {
            exitIndex = random.Next(0, spawnPoints.Count);
        }

        entry = spawnPoints[0];
        exit = spawnPoints[1];
        
    }
}
