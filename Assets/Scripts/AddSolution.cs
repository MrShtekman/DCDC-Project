using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class AddSolution : MonoBehaviour
    {
    public GameObject fence;
    //public GameObject foodCourt;
    [SerializeField] private bool swapped;
    [SerializeField] private float swapInterval;
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private ShowNavmesh showNav;
    [SerializeField] private GameObject ogDiners, changedDiners;

    private GameObject[] customers;
    private float timer;
    private void Start()
        {
        //toggleFoodCourt();
        }

    private void Update()
        {
        timer -= Time.deltaTime;
        }


    private void OnTriggerEnter(Collider other)
        {
        if(timer <= 0)
            {
            swapped = !swapped;
            toggleFoodCourt();
            timer = swapInterval;
            }
        
        }
    private void toggleFoodCourt()
    {

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
}


