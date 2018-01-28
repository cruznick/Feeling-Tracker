using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour {
    public GameObject[] particles;
    public GameObject[] players;
    public bool particleActive;

    // Use this for initialization
    void Start () {
        particleActive = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //players[1].GetComponent<Light>().intensity = 5f;
        }


        if (particleActive && players[1] != null)
        {
            for (int i = 0; i < players.Length; i++)
            {
                //print("sdosdf");
                if(particles[i].gameObject.activeSelf == false) particles[i].gameObject.SetActive(true);
                particles[i].transform.position = players[i].transform.position;
                particles[i].transform.LookAt(players[(i + 1) % players.Length].transform);
                particles[i].transform.GetComponent<ParticleSystem>().startLifetime = Vector3.Distance(players[(i + 1) % players.Length].transform.position, players[i].transform.position) / particles[i].transform.GetComponent<ParticleSystem>().startSpeed;
                
            }
            //players[0].GetComponent<Light>().intensity += 0.001f;
            players[1].GetComponent<IA>().faith -= 0.8f;
            
            if ( players[1].GetComponent<IA>().faith <= 0)
            {
                particleActive = false;
                for (int i = 0; i < players.Length; i++)
                {
                    particles[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            //players[0].GetComponent<Light>().intensity -= 0.0005f;
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                particleActive = true;
                for (int i = 0; i < players.Length; i++)
                {
                    particles[i].gameObject.SetActive(true);
                }
            }*/
        }
      
	}

    public void SetLight()
    {
        particles[1].GetComponent<ParticleSystem>().startColor = players[1].GetComponent<enemigo>().sprite.GetComponent<SpriteRenderer>().color;
    }
}
