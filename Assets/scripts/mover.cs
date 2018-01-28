using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class mover : MonoBehaviour {

    public float x;
    public float y;
    float velocidad;
    float rotacion;
    public GameObject circulo;
    public GameObject iniciador;
    bool activador;
    public AudioClip[] music;
    bool ss;

    

    public List<GameObject> enemies = new List<GameObject>();
    public GameObject[] imagenes;
    public GameObject[] victimario = new GameObject[20];
    public float[] fevictimas = new float[20];
    public GameObject victima;
    public int victimastotal;
    public int victimasactuales;
    public float femasbaja;
    public int indicevictimario;

    public Slider porderoso;
    public float podertotal;
    public float poderactual;
    public GameObject gameover;
    private ParticlesManager particleManager;
    private bool casting;
    private float dist;
    public float maxDist;
    private Animator animator;
    private AudioSource[] audioSource;

    public GameObject win;
    // Use this for initialization
    void Start () {
        audioSource = transform.GetComponents<AudioSource>();
        animator = transform.GetComponent<Animator>();
        casting = false;
		ss = false;
		victimasactuales = 0;
		activador = false;
        //poderactual = 100;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(poderactual >= 200)
        {
            int nexSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nexSceneIndex)
            {
                SceneManager.LoadScene(nexSceneIndex);

            }
        }
        else if (casting)
        {
            imagenes[0].SetActive(false);
            imagenes[1].SetActive(false);
            imagenes[2].SetActive(false);
            imagenes[3].SetActive(true);
        }
        else if(poderactual>= 140)
        {
            imagenes[0].SetActive(true);
            imagenes[1].SetActive(false);
            imagenes[2].SetActive(false);
            imagenes[3].SetActive(false);
        }
        else if(poderactual >= 50)
        {
            imagenes[0].SetActive(false);
            imagenes[1].SetActive(true);
            imagenes[2].SetActive(false);
            imagenes[3].SetActive(false);
        }
        else
        {
            imagenes[0].SetActive(false);
            imagenes[1].SetActive(false);
            imagenes[2].SetActive(true);
            imagenes[3].SetActive(false);
        }
       // print(victimasactuales);
        particleManager = gameObject.GetComponent<ParticlesManager>();

        float movi = Input.GetAxis ("Vertical");
		float rotar = Input.GetAxis ("Horizontal");
		movi *= Time.deltaTime;
		rotar *= Time.deltaTime;
        if (movi != 0) animator.SetBool("walking", true);
        else animator.SetBool("walking", false);
        transform.Translate (0,movi*y,0);
		transform.Rotate (0,0,-1*rotar*x);
        //print(poderactual);

        poderactual -= 0.065f;
        porderoso.value = poderactual;

        if (poderactual <= 0)
        {
            gameover.SetActive(true);
        }

        if (!casting)
        {
            
            if (circulo.activeSelf) circulo.SetActive(false);
            if (victimasactuales > 0)
            {
                if (!activador) iniciador.SetActive(true);
                activador = true;
            }
            else
            {
                if (activador) iniciador.SetActive(false);
                activador = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && activador == true)
            {
                audioSource[2].clip = music[1];
                audioSource[2].Play();
                circulo.GetComponent<minirunas>().casting = true;
                
                casting = true;
                //ss = true;
                activador = false;
                checkVictima();
                victima.gameObject.tag = "Untagged";
                circulo.GetComponent<minirunas>().setRunes(victima.GetComponent<enemigo>().fe);
                victima.GetComponent<IA>().close_contact();
                circulo.SetActive(true);
                iniciador.SetActive(false);
                //poderactual -= 10;
                
                
            }
        }
        else
        {
           // victima.GetComponent<IA>().aura.gameObject.SetActive(false);
            dist = Vector2.Distance(transform.position, victima.transform.position);
            //print(dist);
            if (dist > maxDist)
            {
                ExitCast();
         //       victima.GetComponent<IA>().aura.gameObject.SetActive(false);
                circulo.GetComponent<minirunas>().casting = false;

            }
        }
	}


    private void checkVictima()
    {
        float dist = 10000f;
        float aux;
        for(int i= 0; i < enemies.Count; i++)
        {
            aux = Vector2.Distance(transform.position,enemies[i].transform.position);
            if (dist > aux)
            {

                dist = aux;
                victima = enemies[i];
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "muro")
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = new Vector3(coll.contacts[0].point.x, coll.contacts[0].point.y,0) - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            transform.Translate(dir);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "victima")
        {
            if (!findEnemy(col.gameObject))
            {
                victimasactuales++;
                enemies.Add(col.gameObject);
                col.gameObject.GetComponent<IA>().aura.gameObject.SetActive(true);
            }
           // print(enemies.Count);
        }


    }

    private bool findEnemy(GameObject gameObject)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (gameObject == enemies[i]) return true;
        }
        return false;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "victima")
        {
            if(findEnemy(col.gameObject))victimasactuales--;
            enemies.Remove(col.gameObject);
            col.gameObject.GetComponent<IA>().aura.gameObject.SetActive(false);
            //print(enemies.Count);
        }
        
    }
    public void Recibir(){
		poderactual += (victima.GetComponent<enemigo>().fe*10)-10;
		porderoso.value = poderactual;
        //Destroy (victima);
        particleManager.players[1] = victima;
        particleManager.particleActive = true;
        particleManager.SetLight();
        //victima.GetComponent<IA>().faith = 0;
    }

    public void FinishCast()
    {
        Recibir();
        audioSource[2].clip = music[0];
        audioSource[2].Play();

        casting = false;
        
        victimasactuales--;
        //victima.GetComponent<IA>().aura.gameObject.SetActive(false);
        enemies.Remove(victima);
        
    }

    public void ExitCast()
    {
        audioSource[2].clip = music[0];
        audioSource[2].Play();
        poderactual -= 10f;
        victima.gameObject.tag = "victima";
        victimasactuales--;
        enemies.Remove(victima);
        victima.GetComponent<IA>().aura.gameObject.SetActive(false);
        casting = false;
    }

}
