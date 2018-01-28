using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour {

    //public bool on = false;
    public SpriteRenderer pol;
    public float aware = 0;
	// Use this for initialization
	void Start () {
       // gameObject.SetActive(false);
        pol.color = new Color(1,1,1,0);
       
    }

    // Update is called once per frame
    void Update()
    {
        aware = this.GetComponent<IA>().get_awareness();
        // pol.color = new Color(pol.color.r, pol.color.g, pol.color.b, aware);
        pol.color = new Color(1, 1, 1, aware / 100f);
    }
     
}
