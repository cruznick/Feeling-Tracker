using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private ParticlesManager particles;
    private RuneManager runeManager;
    
    float maxEnergy = 100;
    float energy = 30;
    bool cast;
    private int runeNumb;

    // Use this for initialization
    void Start () {
        cast = false;
        particles = transform.GetComponent<ParticlesManager>();
        runeManager = transform.GetComponent<RuneManager>();
        //particles.SetLight(energy, maxEnergy);
        //runeNumb = runeManager.lastRunes.Count;
    }
	
	// Update is called once per frame
	void Update () {
        /*
        cast = true;
		for(int i = 0; i < runeNumb; i++)
        {
            if (runeManager.lastRunes[i] != i+1) cast = false;
        }
        if (cast)
        {
            particles.particleActive = true;
            runeManager.resetLastRunes();
        }
        runeManager.printList();*/
    }
}
