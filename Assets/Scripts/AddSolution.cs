using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSolution : MonoBehaviour
{
    public GameObject cube;
    public GameObject solution;

    private void OnTriggerEnter(Collider other)
        {
        cube.SetActive(true);
        solution.SetActive(false);
        gameObject.SetActive(false);
        }
    }
