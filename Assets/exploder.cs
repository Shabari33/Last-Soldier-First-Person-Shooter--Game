using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exploder : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 30f);
               
            foreach (Collider col in collider)
            {
                playermovement playermovement =col.GetComponent<playermovement>();  
                if (playermovement != null)
                {
                    Destroy(playermovement.gameObject);
                }
            }
               
            }
        }
    

