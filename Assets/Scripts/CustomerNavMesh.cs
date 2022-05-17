using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerNavMesh : MonoBehaviour
{
    private NavMeshAgent agent;
    //[SerializeField] private Transform desti;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(0.1f, 0.17f);
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            Destroy(gameObject);

        if (agent.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(agent.velocity);
    }

    public void setDestination(Vector3 destination)
    {
        //desti = destination;
        agent.SetDestination(destination);
        
    }

}

