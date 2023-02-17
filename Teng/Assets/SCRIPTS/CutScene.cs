using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    // GameObject Variables
    public GameObject enemyCutscene;
    public GameObject playerCutscene;
    public GameObject cutsceneObjects;
    public GameObject timeline;
    public GameObject timelineAfter;
    public GameObject cutsceneCamera;
    // Start is called before the first frame update
    void Start()
    {
        // Finding game obejct references
        cutsceneCamera = GameObject.Find("CutScene");
        enemyCutscene = GameObject.Find("EnemyInCutscene");
        playerCutscene = GameObject.Find("PlayerInCutscene");
        cutsceneObjects = GameObject.Find("CUTSCENES");
        timeline = GameObject.Find("Timeline");
        timelineAfter = GameObject.Find("TIMELINEAFTER");

        // Set the timeline after setactive to false
        timelineAfter.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Switch from timeline to timelineafter
    public void StartCutsceneTransition()
    {
        timeline.gameObject.SetActive(false);
        timelineAfter.gameObject.SetActive(true);
    }
}
