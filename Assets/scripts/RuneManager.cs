using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour {

    public GameObject[] runes;
    public AudioClip[] audios;
    public AudioClip[] musicNotes;
    //public List<int> lastRunes = new List<int>();
    public GameObject light;
    private ScreenShake screenShake;
    private AudioSource[] audioSources;
    public GameObject circle;
    

    private KeyCode[] keyCodes = {
         KeyCode.LeftArrow,
         KeyCode.UpArrow,
         KeyCode.RightArrow,
         KeyCode.DownArrow,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };
    // Use this for initialization
    void Start () {
        /*for(int i = 0; i < 5; i++)
        {
            //lastRunes.Add(0);
        }*/
        
        audioSources = transform.GetComponents<AudioSource>();
        screenShake = transform.GetComponent<ScreenShake>();
    }
    /*
    public void resetLastRunes()
    {
        int i = 0;
        foreach (int n in lastRunes){
            lastRunes[i] = 0;
            i++;
        }
    


    public void printList()
    {
        print("Last Runes");
        
        foreach (int n in lastRunes)
        {
            print(n);
        }

    }*/
    // Update is called once per frame
    void Update () {
		for(int i = 0; i < runes.Length;i++)
        {
            //print(lastRunes.Count);
            if (circle.activeSelf)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    //lastRunes.RemoveAt(0);
                    //lastRunes.Add(i+1);

                    light.transform.position = runes[i].transform.position;
                    light.GetComponent<ParticleSystem>().Play();
                    screenShake.shake = screenShake.initialShake;
                    audioSources[0].clip = audios[i];

                    audioSources[1].clip = musicNotes[UnityEngine.Random.Range(0, musicNotes.Length - 1)];
                    audioSources[0].Play();
                    audioSources[1].Play();

                }
            }
        }
	}
}
