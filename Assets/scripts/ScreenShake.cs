using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {
    public float shake;
    public float initialShake;
    public float shakeAmount;
    public float decreaseFactor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (shake > 0)
        {
            //float z = Camera.main.transform.localPosition.z;
            //Camera.main.transform.position = Random.insideUnitSphere * shakeAmount;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Random.Range(-shakeAmount,shakeAmount), Camera.main.transform.position.y + Random.Range(-shakeAmount, shakeAmount), Camera.main.transform.position.z);
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            shake = 0.0f;
        }
    }
}
