using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scena_ini_run : MonoBehaviour {
    public GameObject [] array_imagenes ;
    public AudioClip[] array_audios;
    public int contador=0;
    public float [] times;
    public AudioSource audiosource;
    public int max_image;
    // Use this for initialization
    void Start () {
        audiosource = transform.GetComponent<AudioSource>();
        llamar_escenas();
    }
	
	// Update is called once per frame
	void Update () {

       
      
    }
    public void llamar_escenas() {
        StartCoroutine("Mycour");

    }
    IEnumerator Mycour() {

        for(int i= 0; i< array_imagenes.Length; i++)
        {
            array_imagenes[i].SetActive(false);
        }
        array_imagenes[contador].SetActive(true);
        if (array_audios[contador] != null) {
            audiosource.clip = array_audios[contador];
            audiosource.Play();
        }
        yield return new WaitForSeconds(times[contador]);
        contador += 1;
        if (contador >= max_image)
        {
            int nexSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nexSceneIndex)
            {
                SceneManager.LoadScene(nexSceneIndex);

            }

        }
        else {
            llamar_escenas();

        }
  
    }
            
        

    }

