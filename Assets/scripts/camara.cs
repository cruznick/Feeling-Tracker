
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour {

	public float vel;
	public float vel1;
	public float interpvelo;
	public float mindistancia;
	public float followdistancia;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetpos;
	// Use this for initialization
	void Start () {
		targetpos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(target){
			Vector3 posNoz = transform.position;
			posNoz.z = target.transform.position.z;
			Vector3 targetdireccion = (target.transform.position-posNoz);
			interpvelo = targetdireccion.magnitude * vel;
			targetpos=transform.position+(targetdireccion.normalized*interpvelo*Time.deltaTime);
			transform.position = Vector3.Lerp (transform.position,targetpos+offset,vel1);
		}
	}
}
