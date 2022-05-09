using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCourtArea_Spawner : MonoBehaviour 
 { 
 public GameObject FoodcourtPrefab;
 public GameObject targetModel;

    private const float interval = 3f;

    // Start is called before the first frame update
    void Start()
    {
      // InvokeRepeating("SpawnFoodCourt", 1f,interval);
    }

    private bool IsModelTracked() {
        return targetModel.GetComponentInChildren<MeshRenderer>().enabled;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnFoodCourt() {
        if (IsModelTracked()) {
            Spawn();
        }
    }

    public void Spawn() {
        GameObject.Instantiate(FoodcourtPrefab, targetModel.transform.position, targetModel.transform.rotation);
    }
}
