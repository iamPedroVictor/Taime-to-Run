using UnityEngine;
using System.Collections;

public class CarroVindoDoLadoDireito : MonoBehaviour {

	public GameObject startingPoint;
	public GameObject endingPoint;
	int sentido = 0;
	float count;

	public float speed = 0.001f;

	// Use this for initialization
	void Start () {
		//startingPoint = GameObject.Find ("LateralDireita");
		//endingPoint = GameObject.Find ("LateralEsquerda");
	
	}

	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;
		float speedRandom = Random.Range(0.0001f, speed);
		//Debug.Log(speedRandom);

		if (count >= 2f) {
			this.transform.position = new Vector3 (this.transform.position.x + speedRandom * Time.deltaTime / 20, 
				this.transform.position.y, this.transform.position.z);
		}
		if (count >= 3f) {
			this.transform.position = new Vector3 (this.transform.position.x - speedRandom * Time.deltaTime / 20, 
				this.transform.position.y, this.transform.position.z);
			count = 0;
		}
	}
}
