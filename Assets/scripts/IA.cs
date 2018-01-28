using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
 //Agregar para que se muevan en medio del mapa
 //Agregar para que duela mas el warenees
public class IA : MonoBehaviour {
    public Rigidbody2D me;
    public float is_aware = 0; // varios factores lo llevaran a 100 y se dara cuentun 100 es game over
    public float faith = 60; // resistencia contra el aura del mago
    public Vector3 wizard_pos; //Que tan cerca esta
    public float vel_run = .5f; // velocidad con la que corre
    public Vector3 endscreen;  // lugar al que desaparecen especificar lugar en todo caso
    public bool Can_realize = true; //necesario para tiempos de fe

    public Vector3 dis; //distancia entre el y el mago
    public float grow_aware = 2; // cada cuanto aumente el awareness si es 100 baila queso
    public bool under_spell = false; //utilizar para cuando es hechicazadoi

    public Vector3 direccion; //Genera direcciones ramdom
    public float vel_walk; // para casos normales
    public float distancia_atacado = 3;
    //public bool is_distress = false;
    float distance;
    public float sightDistance;

    public GameObject sign;

    public SpriteRenderer aura;
   
    void Start()
    {
        me = GetComponent<Rigidbody2D>();
        wizard_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        endscreen = GameObject.Find("end").transform.position;
        aura = transform.Find("sparkShape 1").GetComponent<SpriteRenderer>();
        aura.gameObject.SetActive(false);
        // signo.gameObject.SetActive(false);
    }
    private bool cuenta = false;
    private float tiempo= 2 ;

    Quaternion rotation;
    void Awake()
    {
        rotation = sign.transform.rotation;
    }
    void LateUpdate()
    {
        sign.transform.rotation = rotation;
    }
    void Update()
    {
        //transform.Rotate(new Vector3(0, 0, transform.rotation.eulerAngles.z - Vector3.Angle(transform.position, me.velocity)));
        // distance = Vector3.Distance(transform.position, jugador.transform.position);
        // if(distance > sightDistance)aura.sortingOrder = -1;
        // else aura.sortingOrder = 1;
        if (is_aware > 100) {
           GameObject enemi =  GameObject.FindGameObjectWithTag("Player");
            enemi.GetComponent<mover>().poderactual -=30;
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
        //Borrar cuenta y asignacion de sprite render

        
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
        StartCoroutine(Run());
        cor = true;
    }
    IEnumerator Run()
    {
        vel_walk*=2;
        yield return new WaitForSeconds(2);
        vel_walk /= 2;
    }
    public void atacado()
    { 

            List<IA> metodos = new List<IA>();
            GameObject[] grupo;
            grupo = GameObject.FindGameObjectsWithTag("victima");
            foreach (GameObject a in grupo)
            {
                Vector3 pos = a.transform.position;
                float diferencia = (transform.position - pos).magnitude;
                if (a != this.gameObject && diferencia < distancia_atacado)
                {
                    if (a.GetComponent<IA>())
                    {
                        metodos.Add(a.GetComponent<IA>());
                    }
                }
            }
            foreach (IA b in metodos)
            {
                b.me_di_cuenta();

            }
        
    }
    public float get_awareness() {
        return is_aware;
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
        
       
        // transform.rotation = Quaternion.LookRotation(me.velocity);
        if (cor) {
            StartCoroutine(MyCoroutine());
        }
       
        
    }

    IEnumerator MyCoroutine() {
        cor = false;
        float x = Random.Range(-9, 9);
        float y = Random.Range(-9, 9);
        direccion = new Vector3(x, y);
 
        Vector3 lookPos = (direccion).normalized;
        float rot_z = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        yield return new WaitForSeconds(Random.Range(3,9));
        cor = true;

        
    }

    public void OnTriggerEnter2D(Collider2D collision)
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
            direccion *= -1;

        }
    }

    public void close_contact()
    {
        rotin();
        this.is_aware += 49;
        aura.gameObject.SetActive(true);
        atacado();
       
    }
    public void rotin()
    {
   
        Vector3 lookPos = (wizard_pos - transform.position).normalized;
        float rot_z = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        
    }
}


