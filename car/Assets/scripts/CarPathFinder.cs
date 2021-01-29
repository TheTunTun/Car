using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarPathFinder : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    int currentWaypoint = 0;
    private float agentSpeed;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
       
        agent = this.GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(waypoints[currentWaypoint].position);
        float distanceToTarget = Vector3.Distance(this.transform.position, waypoints[currentWaypoint].position);

        if(distanceToTarget < 1)
        {
            if(currentWaypoint < waypoints.Length - 1) { currentWaypoint++; } else { currentWaypoint = 0; }
        }
    }

    public void Wait()
    {
        agent.speed = 0;
    }

    public void Continue()
    {
        agent.speed = agentSpeed;
    }

}
