using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSolution : MonoBehaviour
    {
    public GameObject cube;
    public GameObject foodCourt;
    [SerializeField] private bool swapped;
    [SerializeField] private float swapInterval;
    private float timer;
    private void Start()
        {
        toggleFoodCourt();
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
        cube.SetActive(swapped);
        foodCourt.SetActive(!swapped);
        }

    }


