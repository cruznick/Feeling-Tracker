using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creador : MonoBehaviour {
	public GameObject[] muebles=new GameObject[13];
	public int maximo;
	public int minimo;
	public float xmax;
	public float xmin;
	public float ymax;
	public float ymin;
    public float sizeRange;
	float x;
	float y;
	int alea;
	// Use this for initialization
	void Start () {
		generar ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void generar(){
		
		for(int j=0;j<13;j++){
			alea = Random.Range (minimo,maximo);
			for(int i=0; i<alea;i++){
				x = Random.Range (xmin,xmax);
				y = Random.Range (ymin,ymax);
				GameObject obj = Instantiate (muebles[j],new Vector3(x,y,0),Quaternion.identity);
                obj.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0,180)));
                float aux = UnityEngine.Random.Range(-sizeRange, sizeRange);
                obj.transform.localScale = new Vector3(obj.transform.localScale.x +aux, obj.transform.localScale.y +aux, 1);

            }
		}
	}
}
