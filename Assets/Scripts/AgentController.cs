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
    
    [SerializeField] private GameObject customer, ogDiners, changedDiners;
    [SerializeField] private GameObject[] diners;
    
    [Space(20)]
    [SerializeField] private Slider passerSlider;
    [SerializeField] private Text customerIndicatorText, customerNumber;
    [SerializeField] private bool allowSpawning;

    [Header("Play testing variables")]
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private ShowNavmesh showNav;
    public GameObject fence;
    [SerializeField] private bool swapped;

    
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
        //0 = empty, 1 = medium, 2 = crowded.
        switch (passerSlider.value)
        {
            case 0:
                spawnSpeed = 30;
                customerIndicatorText.text = "Empty";
                ActivateThis(0);
                break;
            case 1:
                spawnSpeed = 80;
                customerIndicatorText.text = "Handful";
                ActivateThis(1);
                break;
            case 2:
                spawnSpeed = 140;
                customerIndicatorText.text = "Crowded";
                ActivateThis(1);
                break;
            case 3:
                spawnSpeed = 200;
                customerIndicatorText.text = "Full";
                //TestFunctions();
                ActivateThis(2);

                break;
        }
    }

    private void ActivateThis(int i)
    {
        foreach(GameObject dinerGroup in diners)
        {
            dinerGroup.SetActive(false);
        }

        if (swapped)
            i += 3;

        diners[i].SetActive(true);
    }

    private void TestFunctions()
    {
        GameObject[] customers;
        customers = GameObject.FindGameObjectsWithTag("Customer");
        
        foreach (GameObject customer in customers)
        {
            Destroy(customer.gameObject);
        }
        swapped = !swapped;
        ogDiners.SetActive(!swapped);
        changedDiners.SetActive(swapped);
        fence.SetActive(swapped);
        surface.BuildNavMesh();
        //showNav.ShowMesh();
    }



    public void StartSpawning()
    {
        allowSpawning = true;
    }

    
}
