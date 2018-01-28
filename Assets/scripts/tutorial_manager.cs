using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorial_manager : MonoBehaviour {
    public Text instrucciones;
    public Text historia;
    public bool task1_completed = false;
    public bool task2_completed = false;
    public bool task3_completed = false;
    public bool task4_completed = false;
    public GameObject vic1;
    public GameObject vic2;
    public bool third_active = false;
    // Use this for initialization
    void Start () {
       // instrucciones = GameObject.Find("instruccionos").GetComponent<Text>();
        //historia = GameObject.Find("historio").GetComponent<Text>();
        instrucciones.text = "Presione las teclas W y S para avanzar teclas A y D para cambiar de direccion";
        historia.text = "Gana tanto poder como puedas lanzando maldiciones a la gente";
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mover>().poderactual < 50) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mover>().poderactual += 20;
        }
         //para que no muera en turotial
        if ((vic1.GetComponent<IA>().faith < 0 || vic2.GetComponent<IA>().faith < 0) && task2_completed) {
            third_active = true;

        }
        if (!task4_completed) {
            first_task();
            second_task();
            third_task();
            last_task();
        }
       
        
	}
    private bool a_p=false, w_p = false, s_p = false, d_p = false;

    public void last_task() {
        if (task1_completed && task2_completed && task3_completed && !task4_completed) {
            instrucciones.text = "Finalmente pulse p para regresar al menu";

           if (Input.GetKeyDown(KeyCode.P)) {
                task4_completed = true;
                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
            }
            
        }
    }
    public void first_task() {
       
        if (!task1_completed) {
            check_Input();
            if (a_p && w_p && s_p && d_p) {
                task1_completed = true;
            }
        }
    }

   
    public void second_task() {
        if (task1_completed && !task2_completed) {
            instrucciones.text = "Estando cercas de un enemigo Presione espacio para iniciar un ritual";
            historia.text = "Lanzar maldiciones te asegura poder  pero alertara a las personas cercanas si tu barra de poder llega a cero perderas";
            if (Input.GetKeyDown(KeyCode.Space)) {

                task2_completed = true;
            } 
        }
    }

   // private bool i_p = false, j_p = false, k_p = false, l_p = false;

    public void third_task() {
        if (task1_completed && task2_completed && !task3_completed) {
            instrucciones.text = "Presione las secuencias indicadas utilizando las flechas del teclado";
            historia.text = "Fallar un ritual ocasionara que pierdas poder";
            //check_Input2();
            if (third_active)
            {
                task3_completed = true;
            }
        }
    }
    
    public void check_Input() {

        if (!task1_completed) {
            if (Input.GetKeyDown(KeyCode.A))
            {
                a_p = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                s_p = true;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                w_p = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                d_p = true;
            }

        }
        


    }
}
