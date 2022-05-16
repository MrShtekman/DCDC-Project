using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AgentController : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject customer;
    [SerializeField] private Slider passerSlider;
    [SerializeField] private Text customerIndicatorText, customerNumber;
    [SerializeField] private bool allowSpawning;

    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private ShowNavmesh showNav;

    private float spawnSpeed, spawnTimer;

    private Transform entry, exit;
    void Start()
    {
        passerSlider.onValueChanged.AddListener(delegate { CustomerSliderChange(); });
        CustomerSliderChange();
    }


    void Update()
    {
        customerNumber.text = GameObject.FindGameObjectsWithTag("Customer").Length.ToString();

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && allowSpawning)
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

    private void CustomerSliderChange()
    {
        switch (passerSlider.value)
        {
            case 0:
                spawnSpeed = 30;
                customerIndicatorText.text = "Empty";
                break;
            case 1:
                spawnSpeed = 80;
                customerIndicatorText.text = "Handful";
                break;
            case 2:
                spawnSpeed = 140;
                customerIndicatorText.text = "Crowded";
                break;
            case 3:
                spawnSpeed = 200;
                customerIndicatorText.text = "Full";
               /* GameObject[] customers;
                customers = GameObject.FindGameObjectsWithTag("Customer");
                foreach (GameObject customer in customers)
                {
                    Destroy(customer.gameObject);
                }
                surface.BuildNavMesh();
                showNav.ShowMesh();*/
                break;
        }
    }

    public void StartSpawning()
    {
        allowSpawning = true;
    }

    
}
