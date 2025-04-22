using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dialougemanager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] dialougetext;
    [SerializeField] TextMeshProUGUI Startingtetxt;
    float timer = 30f;
    bool isoff;
    bool isonetime;
    bool isontt;
    [SerializeField] GameObject pannel;
    // Start is called before the first frame update
    void Start()
    {
        
        Startingtetxt.text = " Press  F  To  See Objectives ";
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;    
     if(Input.GetKeyUp(KeyCode.F)&&!isontt) {

            Startingtetxt.text = "";
            isoff = true;
            pannel.SetActive(true); 
            isontt = true;  
        }
     else if(Input.GetKeyUp(KeyCode.F)&&isontt)
        {
            pannel.SetActive(false);
            isontt = false; 
        }

     if(timer <= 0&&!isoff&&!isonetime)
        {
            Startingtetxt.text = "";
            isonetime = true;
        }
     
    }
    public void textbox()
    {

    }
    public void isjobedon(bool obj1,bool obj2,bool obj3)
    {
        if (obj1 == true)
        {

            dialougetext[0].color = Color.green;
        }
        if(obj2 == true)
        {
            dialougetext[1].color= Color.green;
        }
        if(obj3 == true)
        {
            dialougetext[2].color= Color.green;
        }
    }
    IEnumerator starttext()
    {
        
        yield return new WaitForSeconds(5f);
     pannel.SetActive(false) ;
    }
}
