using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	bool isDead = false;
	public GameObject startingPoint;
	public GameObject endingPoint;
	public int sentido;
	private float speed = 100, speedRandom;

	void Start(){
		startingPoint = GameObject.Find ("Start");
		endingPoint = GameObject.Find ("End");
		speedRandom = Random.Range(20, speed);
	}
		
		



	public void Update(){

		if (sentido == 1) {
			this.transform.position = new Vector3 (this.transform.position.x + speedRandom * Time.deltaTime / 20, 
				this.transform.position.y, this.transform.position.z);
		} else if (sentido == 0) {
			this.transform.position = new Vector3 (this.transform.position.x - speedRandom * Time.deltaTime / 20, 
				this.transform.position.y, this.transform.position.z);
		}

		if (this.transform.position.x >= endingPoint.transform.position.x) {
			transform.rotation = Quaternion.Euler(0, 90, 0);
			sentido = 0;
		} else if (this.transform.position.x <= startingPoint.transform.position.x) {
			sentido = 1;
			transform.rotation = Quaternion.Euler(0, -90, 0);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player")
			other.GetComponent<PlayerControl> ().CheckDie ();
	}

}
