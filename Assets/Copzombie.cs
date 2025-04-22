using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Copzombie : MonoBehaviour
{
    public enum zombistates { walk, chase, attack, die }
    public zombistates currentstae;
    GameObject player;
    [SerializeField] float Distancebetween;
    [SerializeField] float Attackrange;
    NavMeshAgent agent;
    Animator animator;
    playermovement playermovement;
    float attacktime = 2f;
    float trackattacktime;
    bool isattacking;
    int Health = 100;
    bool iswalking = true;
    public bool ischase;
    public bool isattack;
    bool isdead;
    bool ishurt;
    bool isheadshot;
    public GameObject Playerblood;




    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentstae = zombistates.walk;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = player.transform.position - this.transform.position;
        distance.y = 0f;

        float angle = Vector3.Angle(transform.forward, distance);
        Quaternion targetrotation = Quaternion.LookRotation(distance);
        if (angle > 1f)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * 10f);

        }

        CopZombieMEchanism();

    }

    private void CopZombieMEchanism()
    {
        if(isdead||ishurt)
        {
            return;
        }
        if (!iswalking)
        {
            agent.speed = speed();
        }
        switch (currentstae)
        {
            case zombistates.walk:
                if (agent.remainingDistance < 0.1f)
                {
                    walk();
                }
                if (Vector3.Distance(this.transform.position, player.transform.position) < Distancebetween)
                {


                    currentstae = zombistates.chase;


                }

                break;
            case zombistates.chase:
                iswalking = false;


                agent.SetDestination(player.transform.position);

                animator.SetBool("isrun", true);
                animator.SetBool("iswalk", false);
                animator.SetBool("isattack", false);
                if (Vector3.Distance(player.transform.position, this.transform.position) < Attackrange)
                {
                    isattack = true;
                    ischase = false;
                    currentstae = zombistates.attack;


                }
                break;
            case zombistates.attack:
                agent.SetDestination(transform.position);
                iswalking = false;
                isattack = true;
                if (Time.time >= trackattacktime && isattack == true)
                {
                    Debug.Log("attacking");
                    animator.SetBool("isattack", true);
                    animator.SetBool("isrun", false);
                    animator.SetBool("iswalk", false);

                    trackattacktime = attacktime + Time.deltaTime;
                }
                if (Vector3.Distance(player.transform.position, this.transform.position) > Attackrange)
                {

                    ischase = true;
                    isattack = false;
                    currentstae = zombistates.chase;
                }
                break;

        }
    }

    void walk()
    {
        Vector3 positionn = randomposition();
        agent.speed = 0.3f;
        agent.SetDestination(positionn);

    }
    public Vector3 randomposition()
    {
        Vector3 randomposition = Random.insideUnitSphere * 5f;
        randomposition += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomposition, out hit, 5f, NavMesh.AllAreas);
        return hit.position;
    }
    public void attacking()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider collider1 in collider)
        {
            playermovement playermovement = collider1.GetComponent<playermovement>();
            if (playermovement != null)
            {
                if (playermovement.healthofplayerss() >= 10)
                {
                    GameObject blood = Instantiate(Playerblood);

                    StartCoroutine(stopblood(blood));
                }
                else
                {
                    StopCoroutine(stopblood(Playerblood));   
                }
                playermovement.Healthofplayer(5);
                isattack = false;
            }
        }
    }
    public float speed()
    {
        return 10f;
    }
    public bool death(int health)
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    public void takedamage(int health) {
        if (health <= 0)
        {
            return;
        }
        if (health >= 100)
        {
            isheadshot = true;
            animator.SetTrigger("Headshot");
            Deathprocedures();
        }
        if (isdead)
        {
            return;
        }
        animator.SetTrigger("ishurt");
        agent.SetDestination(transform.position);
        ishurt = true;
    Health-=health;
        StartCoroutine(hurtaniamtion());
        Debug.Log("Health"+ health);
  if(death(Health)&&isheadshot==false)
        {
            animator.ResetTrigger("ishurt");
            animator.SetTrigger("Dead");
            Deathprocedures();
        }
    }

    private void Deathprocedures()
    {
        isdead = true;
        Destroy(agent);
        Collider collider = this.gameObject.GetComponent<Collider>();
        Destroy(collider);
        Debug.Log("Cop Zombie Dead");
        
       
    }

    public void dead()
    {
        Destroy(this.gameObject);
    }
    IEnumerator hurtaniamtion()
    {
        if (isdead)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.08f);
        ishurt = false;

    }
    IEnumerator stopblood(GameObject Bloods)
    {
        yield return new WaitForSeconds(1f);
        Destroy(Bloods);

    }
  
}

