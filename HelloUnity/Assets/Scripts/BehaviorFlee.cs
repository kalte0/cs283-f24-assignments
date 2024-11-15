using BTAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorFlee : MonoBehaviour
{
    public static float wanderRange = 5.0f; 
    private Root m_btRoot = BT.Root();
    public static float fleeRange = 5.0f; 
    private Animator Animator = null;
    public static float moveDistance = 10.0f; 

    // Start is called before the first frame update
    void Start()
    {
        Animator = this.GetComponent<Animator>();
        BTNode wander = BT.RunCoroutine(MoveToRandom);
        BTNode flee = BT.RunCoroutine(fleeFromPlayer);

        While wanderWhile = BT.While(playerNotInFleeRange);
        wanderWhile.OpenBranch(wander); 

        Sequence sequence = BT.Sequence();
        sequence.OpenBranch(wanderWhile);
        sequence.OpenBranch(flee); 

        m_btRoot.OpenBranch(sequence);
    }

    // Update is called once per frame
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
            this.transform.position, wanderRange, out target);
        agent.SetDestination(target);

        // wait for agent to reach destination 

        while (agent.remainingDistance > 0.1f)
        {
            yield return BTState.Continue;
        }
        yield return BTState.Success;
    }

    IEnumerator<BTState> fleeFromPlayer()
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

        Vector3 directionAwayFromPlayer = this.transform.position - GameObject.FindWithTag("Player").transform.position;
        directionAwayFromPlayer.Normalize();
        directionAwayFromPlayer.y = 0; // get direction vector parallel to floor. 
        
        Vector3 target; 
        Utils.RandomPointOnTerrain(
            this.transform.position + directionAwayFromPlayer * moveDistance, moveDistance/2, out target);
        agent.SetDestination(target);

        // wait for agent to reach destination 

        while (agent.remainingDistance > 0.1f)
        {
            yield return BTState.Continue;
        }
        yield return BTState.Success;
    }

    private bool playerNotInFleeRange()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 thisPos = this.transform.position;
        return Vector3.Distance(playerPos, thisPos) > fleeRange;
    }

}
