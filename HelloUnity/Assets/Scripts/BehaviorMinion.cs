using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.AI; 
using BTAI;
using System;
using System.Drawing;

public class BehaviorMinion : MonoBehaviour
{
    public Transform wanderRange; // Set to a sphere. 
    public Transform homeBase; // set to a sphere
    private Root m_btRoot = BT.Root();
    public float followRange = 5.0f;
    public float attackRange = 1f;
    private Animator Animator = null;
    private bool chasing = false; 

    void Start()
    {
        Animator = this.GetComponent<Animator>(); 
        BTNode wander = BT.RunCoroutine(MoveToRandom);
        BTNode chase = BT.RunCoroutine(ChasePlayer);
        BTNode attack = BT.RunCoroutine(Attack);

        While wanderWhile = BT.While(NPCWandering);
        wanderWhile.OpenBranch(wander); 

        Sequence sequence = BT.Sequence();
        sequence.OpenBranch(wanderWhile); 
        sequence.OpenBranch(chase);
        sequence.OpenBranch(attack);

        m_btRoot.OpenBranch(sequence); 
    }

    void Update()
    {
        m_btRoot.Tick();
    }

    IEnumerator<BTState> MoveToRandom()
    {
        if (Animator != null)
        {
            Animator.Play("Walk");
        }
        else
        {
            Debug.Log("No Animator selected for Minion!!");
        }

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        Vector3 target;
        Utils.RandomPointOnTerrain(
            wanderRange.position, wanderRange.localScale.x, out target);
        agent.SetDestination(target); 

        // wait for agent to reach destination 

        while (agent.remainingDistance > 0.1f)
        {
            yield return BTState.Continue; 
        }
        yield return BTState.Success; 
    }

    IEnumerator<BTState> ChasePlayer()
    {
        chasing = true; 
        if (Animator != null)
        {
            Animator.Play("Walk");
        }
        else
        {
            Debug.Log("No Animator selected for Minion!!");
        }

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3 playerPos;
        NavMeshHit positionOnMesh;

        // wait for agent to reach destination 
        playerPos = GameObject.FindWithTag("Player").transform.position;
        NavMesh.SamplePosition(playerPos, out positionOnMesh, 0.1f, NavMesh.AllAreas);
        agent.SetDestination(positionOnMesh.position);

        while (!playerInAttackRange() && !playerInHomeBase()) // while not in attackRange
        {
            //Debug.Log(agent.remainingDistance); 
            playerPos = GameObject.FindWithTag("Player").transform.position;
            float floorY = GameObject.Find("Building/Building/FloorPlatform").GetComponent<Collider>().bounds.max.y;
            playerPos.y = floorY; 
            Debug.Log("playerPos: " + playerPos); 
            NavMesh.SamplePosition(playerPos, out positionOnMesh, 0.1f, NavMesh.AllAreas);
            Debug.Log(positionOnMesh.position);
            agent.SetDestination(positionOnMesh.position);
            yield return BTState.Continue;
        }

        Debug.Log("Finished chase");
        yield return BTState.Success; // finishing chasing, go to attacking/ 
    }


    IEnumerator<BTState> Attack()
    {
        if (Animator != null)
        {
            Animator.Play("Attack"); 
        }
        else
        {
            Debug.Log("No Animator selected for Minion!!");
        }

       
        while (playerInAttackRange() && !playerInHomeBase()) // while in attack range. 
        {
            yield return BTState.Continue; 
        }

        Debug.Log("Finished attack");
        yield return BTState.Success; // finished Attacking, go back to chasing. 
    }

    private bool playerInFollowRange()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 thisPos = this.transform.position;
        //Debug.Log(Vector3.Distance(playerPos, minionPos)); 
        return Vector3.Distance(playerPos, thisPos) < followRange; 
    }

    private bool playerInAttackRange()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 thisPos = this.transform.position;
        //Debug.Log(Vector3.Distance(playerPos, minionPos));
        return Vector3.Distance(playerPos, thisPos) < attackRange;
    }

    private bool playerInHomeBase()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 homeBaseCenter = homeBase.position;
        chasing = false; // if in home base, then stop chasing. 
        //Debug.Log(Vector3.Distance(playerPos, minionPos));
        return Vector3.Distance(playerPos, homeBaseCenter) < homeBase.localScale.x/2; // if within radius of home base
    }

    private bool NPCWandering()
    {
        // NPC should wander if not chasing and player is not in range OR if player is in home base. 
        return (!playerInFollowRange() && !chasing) || playerInHomeBase(); 
    }
}