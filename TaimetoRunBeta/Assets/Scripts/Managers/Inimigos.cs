using UnityEngine;
using System.Collections;

public class Inimigos : MovimentBase {

    [SerializeField]
    private float timeForJump = 1.1f;
    private bool isLeft;
    public GameObject startingPoint;
    public GameObject endingPoint;

    // Use this for initialization
    void Start () {
        startingPoint = GameObject.Find("Start");
        endingPoint = GameObject.Find("End");
	
	}
	
	// Update is called once per frame
	void Update () {

        if (GameManager.instance.gameState != GameState.RunnerGame && GameManager.instance.gameState != GameState.GameOver)
            Destroy(this.gameObject);

        if (timeForJump > 0){
            timeForJump -= Time.deltaTime;
            justJump = false;
        }
        if(timeForJump <= 0){
            if (isLeft){
                //Movimenta para a esquerda
                justJump = true;
                MoveLeft();
            }else{
                //Movimenta para a direita
                MoveRight();
                justJump = true;
            }
            timeForJump = 1.1f;
        }

        if(this.transform.position.x <= startingPoint.transform.position.x){
            isLeft = false;
        }else if(this.transform.position.x >= endingPoint.transform.position.x){
            isLeft = true;
        }


	
	}
}
