using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class deer : MonoBehaviour
{
    NavMeshAgent agent;
    public enum deerstate { eat, walk, run }
    public deerstate currentstate;
    float timer = 10f;
    float walktimer = 10f;
    Animator animator;
    Transform player;
    float sprinttimer = 10f;
    public bool isgunshhot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentstate = deerstate.eat;
        animator = GetComponent<Animator>();


    }
    void Update()
    {
        deermovement();
        if (isgunshhot)
        {
            currentstate = deerstate.run;
            agent.speed = 20f;

        }

    }
    void deermovement()
    {
        switch (currentstate)
        {
            case deerstate.eat:

                animator.SetBool("iswalk", false);
                animator.SetBool("iseat", true);
                agent.SetDestination(transform.position);
                Debug.Log("Deer Timer" + timer);

                timer -= Time.deltaTime;
                Debug.Log("Deer Timer" + timer);
                if (timer <= 0f)
                {
                    cgangestate();
                    currentstate = deerstate.walk;



                }
              
                break;
            case deerstate.walk:

                animator.ResetTrigger("sprint");
                animator.SetBool("issprint", false);
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath)
                {

                    timer = 10f;

                    currentstate = deerstate.eat;



                }



                {

                }






                break;

            case deerstate.run:
                {
                    animator.SetBool("iswalk", false);
                    Vector3 dis = transform.position - player.position;
                    Vector3 newdir = Random.insideUnitSphere * 30f;
                    Vector3 posss = transform.position + dis;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(posss, out hit, 20f, NavMesh.AllAreas);
                    agent.SetDestination(hit.position);
                     agent.speed = 20f;
                    animator.SetTrigger("sprint");
                    sprinttimer += Time.deltaTime;
                    if (Vector3.Distance(player.position, transform.position) > 100)
                    {
                        isgunshhot = false;
                        currentstate = deerstate.walk;
                    }

                    break;
                }
        }


    }
    public void cgangestate()
    {
        if (isgunshhot)
        {
            currentstate = deerstate.run;
        }
        NavMeshHit hit;
        Vector3 pos = Random.insideUnitSphere * 20f;
        pos += transform.position;
        NavMesh.SamplePosition(pos, out hit, 20f, NavMesh.AllAreas);
        animator.SetBool("iswalk", true);
        animator.SetBool("iseat", false);
        agent.SetDestination(hit.position);

    }
    public void isture(bool shoot)
    {
      
            isgunshhot = true;
            Debug.Log("Deer found");
        
    }

}



