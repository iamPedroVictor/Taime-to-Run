using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public GameObject startingPoint;
	public GameObject endingPoint;
	public int sentido;
    //Sentido == 0 => Esquerda
    //Sentido == 1 => Direita
    //Sentido == 2 => Seguir Jogador
	private float speed = 100, speedRandom;
    public bool isDiagonal = false;
    protected float canIJump = 2;

	void Start(){
		startingPoint = GameObject.Find ("Start");
		endingPoint = GameObject.Find ("End");
		speedRandom = Random.Range(20, speed);
    }


    /*
    public void Update(){
        if (GameManager.instance.gameState != GameState.RunnerGame)
            return;

        if (canIJump < 1f)
            canIJump += Time.deltaTime;
        if(sentido == 0 && canIJump >= 1){
            canIJump = 0;
            if (transform.position.x <= startingPoint.transform.position.x){
                sentido = 1;
            }
        }
        else if(sentido == 1 && canIJump >= 1)
        {
            canIJump = 0;
            if (transform.position.x >= endingPoint.transform.position.x){
                sentido = 0;
            }     
        }
    }*/



    
	public void Update(){

		if (sentido == 1) {
			this.transform.position = new Vector3 (this.transform.position.x + speedRandom * Time.deltaTime / 20, 
				this.transform.position.y, this.transform.position.z);
		} else if (sentido == 0) {
			this.transform.position = new Vector3 (this.transform.position.x - speedRandom * Time.deltaTime / 20, 
				this.transform.position.y, this.transform.position.z);
		}

		if (this.transform.position.x >= endingPoint.transform.position.x) {		
            if (this.gameObject.name.Contains("reporter")){
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }else{
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
			sentido = 0;
		} else if (this.transform.position.x <= startingPoint.transform.position.x) {
			sentido = 1;
            if (this.gameObject.name.Contains("reporter")){
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }else{
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player")
			other.GetComponent<PlayerControl> ().CheckDie ();
	}

}
