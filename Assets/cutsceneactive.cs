using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class cutsceneactive : MonoBehaviour
{
    public GameObject cutscene;
    public GameObject playercameraaa;
    public string[] pathhhh;
    public TextMeshProUGUI pathtext;
    int i = 0;
    bool isend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isend == false)
        {

        StartCoroutine(startcutscn(2f));
        }
        if(i==2)
        {
            StartCoroutine(Blanktext());
        }

    }
 

    private void Startcutscene()
    {
        cutscene.SetActive(true);
        playercameraaa.SetActive(false);
    }

    public void dialougessss()
    {
       pathtext.text = pathhhh[i];
        i++;
    }
    public void oncamera()
    {
        playercameraaa.SetActive(true);
        
    }
    IEnumerator startcutscn(float waitscenods)
    {
        yield return new WaitForSeconds(waitscenods);
        Startcutscene();
        isend=true;
    }
    IEnumerator Blanktext()
    {
        yield return new WaitForSeconds(3f);
        pathtext.text = " ";
    }


   }
