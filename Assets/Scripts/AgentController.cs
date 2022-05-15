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
    [SerializeField] private Text customerIndicatorText;

    private float spawnSpeed;
    private float spawnTimer = 0;
    private Transform entry, exit;
    void Start()
    {
        passerSlider.onValueChanged.AddListener(CustomerSliderChange);
        passerSlider.value = 2;
    }


    void Update()
    {

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

    private void CustomerSliderChange(float arg0)
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
                break;
        }
    }

    private void ShowCustomerNumber()
    {
        //int i = GameObject.FindGameObjectsWithTag("Customer")
    }

    
}
