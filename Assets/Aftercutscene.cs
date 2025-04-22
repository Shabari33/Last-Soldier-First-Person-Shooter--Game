using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aftercutscene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera thiscamera;
    [SerializeField] GameObject playercamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void cutscene()
    {
       player.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
