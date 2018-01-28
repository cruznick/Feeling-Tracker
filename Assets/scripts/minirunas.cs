using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minirunas : MonoBehaviour {

	public GameObject jugador;

	public int cantidadrunas;
	public int[] armado = new int[20];
	public GameObject[] runas = new GameObject[4];

	public float cont;
	public float max;
	public int expulsador;
	int presioando;
	bool respuesta;
	bool incorrecto;
    private int runeQuantity;
    public bool casting;

    // Use this for initialization
    void Start () {
		respuesta = false;
		incorrecto = false;
		expulsador = 0;
		cont = 0;
		generador ();
	}
	
	// Update is called once per frame
	void Update () {
        if (casting)
        {



            runas[armado[expulsador]].SetActive(true);
            cont += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                presioando = 0;
                comparador();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                presioando = 1;
                comparador();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                presioando = 2;
                comparador();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                presioando = 3;
                comparador();
            }


            if (respuesta == true)
            {
               // StartCoroutine(EliminarRuna(expulsador));
                runas[armado[expulsador]].SetActive(false);
                expulsador++;
                cont = 0;
                respuesta = false;
            }

            if (expulsador > runeQuantity)
            {
                casting = false;
                cont = 0;
                expulsador = 0;
                
                jugador.SendMessage("FinishCast");
                generador();
                incorrecto = false;
                runas[0].SetActive(false);
                runas[1].SetActive(false);
                runas[2].SetActive(false);
                runas[3].SetActive(false);
                this.gameObject.SetActive(false);

            }

            if (incorrecto == true || cont >= max && respuesta == false)
            {
                casting = false;
                //jugador.SendMessage("finishCast");
                jugador.SendMessage("ExitCast");
                cont = 0;
                expulsador = 0;
                generador();
                incorrecto = false;
                runas[0].SetActive(false);
                runas[1].SetActive(false);
                runas[2].SetActive(false);
                runas[3].SetActive(false);
                this.gameObject.SetActive(false);

            }
        }
	}
    /*
    private IEnumerator EliminarRuna(int expulsador)
    {
        yield return new WaitForSeconds(0.1f);
        Color col = runas[armado[expulsador]].GetComponent<SpriteRenderer>().color;
        float i = 1f;
        while (true)
        {
            i -= 0.001f;
            runas[armado[expulsador]].GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, i);
            if (i < 0) break;
        }
        
        runas[armado[expulsador]].SetActive(false);
        runas[armado[expulsador]].GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, 1f);
    }*/

    void generador(){
		armado [0] = UnityEngine.Random.Range (0,4);
		armado [1] = UnityEngine.Random.Range (0,4);
		armado [2] = UnityEngine.Random.Range (0,4);
		armado [3] = UnityEngine.Random.Range (0,4);
		armado [4] = UnityEngine.Random.Range (0,4);
		armado [5] = UnityEngine.Random.Range (0,4);
		armado [6] = UnityEngine.Random.Range (0,4);
		armado [7] = UnityEngine.Random.Range (0,4);
		armado [8] = UnityEngine.Random.Range (0,4);
		armado [9] = UnityEngine.Random.Range (0,4);
		armado [10] = UnityEngine.Random.Range (0,4);
		armado [11] = UnityEngine.Random.Range (0,4);
		armado [12] = UnityEngine.Random.Range (0,4);
		armado [13] = UnityEngine.Random.Range (0,4);
		armado [14] = UnityEngine.Random.Range (0,4);
		armado [15] = UnityEngine.Random.Range (0,4);
		armado [16] = UnityEngine.Random.Range (0,4);
		armado [17] = UnityEngine.Random.Range (0,4);
		armado [18] = UnityEngine.Random.Range (0,4);
		armado [19] = UnityEngine.Random.Range (0,4);

	}
	void comparador(){
		if (armado [expulsador] == presioando) {
			respuesta = true;
		} else {
			respuesta = false;
			incorrecto = true;
		}
	}

    public void setRunes(int quantity)
    {
        runeQuantity = quantity;
    }


}
