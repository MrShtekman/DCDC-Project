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
    [SerializeField] private Text customerIndicatorText, customerNumber, breakfastText, lunchText, dinnerText;
    [SerializeField] private bool allowSpawning;

    [Header("Play testing variables")]
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private ShowNavmesh showNav;
    public GameObject fence;
    //[SerializeField] private bool swapped;

    [SerializeField] private AddSolution addSol;

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

    public void CustomerSliderChange()
    {
        //0 = empty, 1 = medium, 2 = crowded.
        switch (passerSlider.value)
        {
            case 0:
                spawnSpeed = 60;
                breakfastText.gameObject.SetActive(true);
                lunchText.gameObject.SetActive(false);
                dinnerText.gameObject.SetActive(false);
                ActivateThis(0);
                break;
            case 1:
                spawnSpeed = 200;
                breakfastText.gameObject.SetActive(false);
                lunchText.gameObject.SetActive(true);
                dinnerText.gameObject.SetActive(false);
                ActivateThis(2);
                break;
            case 2:
                spawnSpeed = 140;
                breakfastText.gameObject.SetActive(false);
                lunchText.gameObject.SetActive(false);
                dinnerText.gameObject.SetActive(true);
                ActivateThis(1);
                break;

        }
    }

    public void ActivateThis(int i)
    {
        foreach(GameObject dinerGroup in diners)
        {
            dinerGroup.SetActive(false);
        }

        if (addSol.swapped)
            i += 3;

        diners[i].SetActive(true);
    }




    public void StartSpawning()
    {
        allowSpawning = true;
    }

    public void ToggleSolution()
    {
        addSol.gameObject.SetActive(!addSol.swapped);
    }

}
