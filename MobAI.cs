using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobAI : MonoBehaviour
{
    [Range(0.5f, 50)]
    public float detectDistance = 3;
    public Transform[] points;
    NavMeshAgent agent;
    int destinationIndex = 0;
    Transform player;
    float agentSpeed;
    //public GameObject spawnEffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if(agent !=null)
        {
            agent.destination = points[destinationIndex].position;
        }
        agentSpeed = agent.speed;
    }

    private void Update()
    {
        Walk();
        SearchPlayer();
        SetMobSize();
    }

    public void SearchPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer <= detectDistance)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed + 0.5f;
        }

        else
        {
            agent.speed = agentSpeed;
        }
    }

    public void Walk()
    {
        float dist = agent.remainingDistance;
        if (dist <= 0.05f)
        {
            //destinationIndex++;
            //if (destinationIndex > points.Length - 1)
            //destinationIndex = 0;
            //agent.destination = points[destinationIndex].position;
            agent.destination = points[Random.Range(0, points.Length)].position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }

    public void SetMobSize()
    {
        if(Vector3.Distance(transform.position, player.position)<= detectDistance + 3)
        {
            iTween.ScaleTo(gameObject, Vector3.one, 0.5f);
            //GameObject se = Instantiate(spawnEffect, gameObject.transform.position, Quaternion.identity); //se = spawn effect
            //Destroy(se, 0.5f);
        }
    }
}
