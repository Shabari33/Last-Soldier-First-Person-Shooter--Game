using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class electricboxhealth : MonoBehaviour
{
   public float health=80f;
    Soldierscript soldierscript;
    GameObject[] soldiers;
    public float timer = 5f;
    Rigidbody rb;
    public float force = 10f;
    public float radisu = 10f;
    bool bomb;
    public GameObject[] fire;
    [SerializeField] GameObject[] Destroyed;
    GameObject House;
    public LayerMask player;
    GameObject Player;
    Transform playerpos;
    float timerrrr = 5f;
    bool isrange;
    public TextMeshProUGUI bombtimer;
    public float destruyedtimer = 60f;
    AudioSource audiosource;
    AudioSource playersfx;
    [SerializeField] TextMeshProUGUI firetext;
    AudioSource houseaudiosource;
    [SerializeField] AudioClip Firesfx;
    [SerializeField] AudioClip Bombsfx;
    bool isaudioplyaed;
  public  bool isplayerexited;
    // Start is called before the first frame update
    void Start()
    {
        soldiers = GameObject.FindGameObjectsWithTag("Soliders");
        rb = GetComponent<Rigidbody>();
        House = GameObject.FindGameObjectWithTag("House");
        Player = GameObject.FindGameObjectWithTag("Player");
        audiosource=GetComponent<AudioSource>();
        playersfx = GameObject.FindGameObjectWithTag("Audiomanager").GetComponent<AudioSource>();
        houseaudiosource=House.GetComponent<AudioSource>(); 
   
    }

    // Update is called once per frame
    void Update()
    {
        if(isplayerexited)
        {
            houseaudiosource.Stop();    
        }
        if (bomb)
        {
          
            destruyedtimer-=Time.deltaTime; 
            if(destruyedtimer <= 0 &&!isplayer())
            {
                //Destroy(House);
            }
            
            timer -= Time.deltaTime;
            bombtimer.text=timer.ToString("00");
           
            if (timer <= 0)
            {
               
                bombtimer.enabled=false;
              for(int i=0; i < Destroyed.Length; i++)
                {   
                    Destroyed[i].SetActive(true);
                   
                   

                }
                if (!isaudioplyaed)
                {
                houseaudiosource.clip = Bombsfx;
                houseaudiosource.loop = false;
                    houseaudiosource.Play();
                    isaudioplyaed = true;
                StartCoroutine(playfirevfx());

                }
                if (isplayer()==true)
                {
                  timerrrr-= Time.deltaTime;
                  playermovement playermovement=Player.GetComponent<playermovement>();
                    if (timerrrr <= 0)
                    {
                    playermovement.Healthofplayer(20);
                        timerrrr = 5f;

                    }
                }
                Collider[] colliders = Physics.OverlapSphere(transform.position, radisu);
                foreach (Collider collider in colliders)
                {
                    Debug.Log("Colliderrr Name"+collider.name);
                    Rigidbody rbs = collider.GetComponent<Rigidbody>();
                    Destroy(rbs.gameObject);
                    if (rbs != null)
                    {

                        rbs.AddExplosionForce(force, transform.position, radisu);
                    }

                 


                }
               



            }
           
        }
        
    }
    public void takedamage(float damage)
    {
        health -= damage;
        if(health <=50)
        {
            audiosource.Play();
            houseaudiosource.Play();
            playersfx.Play();
            firetext.text = "No Fire i Have to leave now...i can't breathe!";
            StartCoroutine(offthefiretxt());


            foreach (GameObject fire in fire)
            {
                fire.SetActive(true);
            }
            StartCoroutine(makethemrun());
            bomb=true;
            
        }
    }
    IEnumerator  makethemrun()
    {
        yield return new WaitForSeconds(5f);
     audiosource.Stop();
        for (int i = 0; i < soldiers.Length; i++)
        {

            soldierscript = soldiers[i].GetComponent<Soldierscript>();
            soldierscript.isrun = true; 
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radisu);
    }
    public bool isplayer()
    {
       
        Collider[] colliders = Physics.OverlapSphere(transform.position, radisu,player);
      foreach(Collider collider in colliders)
        {
            if (collider.transform == Player.transform)
            {
                return true;
            }
           
        }
      return false;
     
      
    }
    IEnumerator offthefiretxt()
    {
        yield return new WaitForSeconds(4.5f);
        firetext.text = "Health will be reduced if you stay in fire zone";
        firetext.color = Color.red;

        yield return new WaitForSeconds(5f);
        firetext.enabled = false;
    }
    IEnumerator playfirevfx()
    {
        yield return new WaitForSeconds(4f);
        houseaudiosource.priority = 10;
        houseaudiosource.pitch = -3f;
        houseaudiosource.clip = Firesfx;
       houseaudiosource.loop=true;
        houseaudiosource.Play();
    }
}
