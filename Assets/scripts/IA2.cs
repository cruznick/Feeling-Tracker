using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
 //Agregar para que se muevan en medio del mapa
 //Agregar para que duela mas el warenees
public class IA2 : MonoBehaviour {
    public Rigidbody2D me;
    public float is_aware = 0; // varios factores lo llevaran a 100 y se dara cuentun 100 es game over
    public float faith = 100; // resistencia contra el aura del mago
    public Vector3 wizard_pos; //Que tan cerca esta
    public float vel_run = .5f; // velocidad con la que corre
    public Vector3 endscreen;  // lugar al que desaparecen especificar lugar en todo caso
    public bool Can_realize = true; //necesario para tiempos de fe

    public Vector3 dis; //distancia entre el y el mago
    public float grow_aware = 2; // cada cuanto aumente el awareness si es 100 baila queso
                                 //public bool under_spell = false; //utilizar para cuando es hechicazadoi

    public Vector3 direccion; //Genera direcciones ramdom
    public float vel_walk; // para casos normales
    public float distancia_atacado = 3;
    //public bool is_distress = false;
    public GameObject sign;
    public int damp;

    public SpriteRenderer signo;

    public SpriteRenderer aura;
   
    void Start()
    {
        me = GetComponent<Rigidbody2D>();
        wizard_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        endscreen = GameObject.Find("end").transform.position;
        aura = transform.Find("sparkShape 1").GetComponent<SpriteRenderer>();
        signo.gameObject.SetActive(false);
    }
    private bool cuenta = false;
    private float tiempo= 2 ;
    void Update()
    {
        sign.transform.rotation = new Quaternion(0, 0, 0, 0);
        if (is_aware > 100) {
           GameObject enemi =  GameObject.FindGameObjectWithTag("Player");
            enemi.GetComponent<mover>().poderactual -=60;
            is_aware = 0;
        }
        tranquilizandose();
        aura.color = new Vector4(aura.color.r, aura.color.g, aura.color.b, faith / 100f);
        wizard_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (faith > 0)
        {
            movemente();
        }//under_spell
        else
        {
            vel_walk = 0;
            direccion = new Vector3(0, 0, 0);
            me.velocity = direccion * vel_walk;
        }

        if (faith <= 0)
        {
            correr();
        }
        if (cuenta) {
            tiempo -= Time.deltaTime;
            if (tiempo < 0) {
                tiempo = 2;
                signo.gameObject.SetActive(false);
                cuenta = false;
            }
        }

        
    }
    private float max =2f;
    private float min = 2f;
    private bool isd = true;

    public void tranquilizandose() {
        if (isd)
        {
            isd = false;
            if (is_aware > 0) {
                this.is_aware -= 2;
            }
           
        }
        else if (!isd){
            min -= Time.deltaTime;
            if (min < 0) {
                min = max;
                isd = true;
            }

        }
  

    }
      public void me_di_cuenta()
    {
        this.is_aware += 30;
       
    }

    public void atacado()
    { 

            List<IA2> metodos = new List<IA2>();
            GameObject[] grupo;
            grupo = GameObject.FindGameObjectsWithTag("victima");
            foreach (GameObject a in grupo)
            {
                Vector3 pos = a.transform.position;
                float diferencia = (transform.position - pos).magnitude;
                if (a != this.gameObject && diferencia < distancia_atacado)
                {
                    if (a.GetComponent<IA2>())
                    {
                        metodos.Add(a.GetComponent<IA2>());
                    }
                }
            }
            foreach (IA2 b in metodos)
            {
                b.me_di_cuenta();
                b.surprise();
            }
        
    }

    public void surprise() {
        if (!cuenta) {
            signo.gameObject.SetActive(true);
            cuenta = true;

        }
        
    }

    public float get_faith()
    { //llamar para conseguir la fe

        return faith;

    }
    public void succes()
    { 
        faith -= 10;
        aura.color = new Vector4(aura.color.r, aura.color.g, aura.color.b, faith / 100f);
        if (faith <= 0)
        {
            correr();
        }
      
    }
    public void correr()
    {
        transform.position = Vector3.MoveTowards(transform.position, endscreen, vel_run);
    }
    public bool cor=true;
    public void movemente()
    {
        
        
        me.velocity = direccion.normalized * vel_walk;
        if (cor && (faith > 0)) {
            StartCoroutine(MyCoroutine());
        }
       
        
    }

    IEnumerator MyCoroutine() {
        cor = false;
        float x = Random.Range(-9, 9);
        float y = Random.Range(-9, 9);
        direccion = new Vector3(x, y);
        yield return new WaitForSeconds(Random.Range(3,9));
        cor = true;

        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "muro")
        {
            float x = Random.Range(-3, 3);
            float y = Random.Range(-3, 3);
            if (x == 0 || y == 0)
            {
                x = Random.Range(1, 3);
                y = Random.Range(1, 3);
            }
            direccion = new Vector3(x, y) * -1;

        }
    }

    public void close_contact()
    {
        rotin();
        this.is_aware += 45;
        atacado();
       
    }
    public void rotin()
    {
        damp = 1;
        Vector3 lookPos = (wizard_pos - transform.position).normalized;
        float rot_z = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        
    }
}


