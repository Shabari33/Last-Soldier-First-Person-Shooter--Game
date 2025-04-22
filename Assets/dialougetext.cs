using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class dialougetext : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialougetextsss;
    public string[] dialouges;
    int i;
    PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void textshow()
    {

        dialougetextsss.text = dialouges[i];
        i++;



    }
    public void pause()
    {
        playableDirector.Pause();
    }
    public void nextscene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        index++;
        SceneManager.LoadScene(index);  
    }
}
