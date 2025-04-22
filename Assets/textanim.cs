using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class textanim : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public string texttodisply,kkkk;
    int i = 0;
    float timer=0f;
    PlayableDirector playableDirector;
    public GameObject helicopyter1,helipcopter2;
  public GameObject[] Car;
public GameObject[] run;
    public GameObject play;
    GameObject pannel;
    AudioSource audioSource;
  public  AudioClip audioClip;    
    // Start is called before the first frame update
    void Start()
    {
   
        Debug.Log("Text"+texttodisply.Length);
       
        pannel = GameObject.FindGameObjectWithTag("Pannel");
  
  
        audioSource=GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (i > texttodisply.Length)
        {
            audioSource.Stop();
            play.SetActive(true);
            pannel.SetActive(false);
            helicopyter1.SetActive(true);
            helipcopter2.SetActive(true);
         foreach(GameObject go in Car)
            {
                go.SetActive(true);
            }
            for (int i = 0; i < run.Length; i++)
            {
                run[i].SetActive(true);
            }
            return;
           
        }
        if(timer <= 0)
        {
            if(!audioSource.isPlaying) { 
            
            audioSource.PlayOneShot(audioClip);
            }

        text.text = texttodisply.Substring(0,i);
            timer = .3f;
            i++;

        }
        timer-=Time.deltaTime;
    }

    IEnumerator texterrr()
    {
            for(int i = 0;i<texttodisply.Length;i++) {

                yield return new WaitForSeconds(1f);
            text.text = texttodisply.Substring(i);  
            
            
            }
        }
    }

