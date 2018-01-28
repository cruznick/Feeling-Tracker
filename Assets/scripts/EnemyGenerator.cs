using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
    public GameObject[] limits;
    public GameObject[] enemies;
    public int initialEnemies;
	// Use this for initialization
	void Start () {
		for(int i = 0; i< initialEnemies; i++)
        {
            int aux = Random.Range(0,2);
            GameObject enem = Instantiate(enemies[aux]);
            Vector3 vect;
            vect = new Vector3(Random.Range(limits[0].transform.position.x, limits[1].transform.position.x), Random.Range(limits[0].transform.position.y, limits[1].transform.position.y),0);
            while (Mathf.Abs(vect.x) < 8 && Mathf.Abs(vect.y) < 8)
            {
                vect = new Vector3(Random.Range(limits[0].transform.position.x, limits[1].transform.position.x), Random.Range(limits[0].transform.position.y, limits[1].transform.position.y), 0);

            }

            enem.transform.position = vect;

            int rand = Random.Range(2, 8);
            enem.GetComponent<enemigo>().fe = rand;
            enem.GetComponent<IA>().vel_walk = Random.Range(1f, 3f); ;
            Color pcolor;

            if (rand < 4) pcolor = Color.white;
            else if (rand < 7) pcolor = Color.yellow;
            else pcolor = Color.red;

            enem.GetComponent<enemigo>().sprite.GetComponent<SpriteRenderer>().color = pcolor;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
