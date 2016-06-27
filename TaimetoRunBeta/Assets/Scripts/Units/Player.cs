using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MovimentBase {

    public int scoreGame;
    public List<GameObject> faixasTotais = new List<GameObject>();
    public int coinTotal;
    public Mesh[] meshGameStyle;
    [SerializeField]
    private MeshFilter meshFilter;
    public bool canMove;
    public int charChoice;


    private void CheckFaixa(){
        RaycastHit faixa;
        Vector3 rayPoint = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        Ray rayHit = new Ray(rayPoint, Vector3.down);
        Debug.DrawRay(transform.position, new Vector3(0, -4, 0), Color.red, 2f);
        if (Physics.Raycast(rayHit, out faixa, 4)){
            if (faixasTotais.Contains(faixa.collider.gameObject) == false){
                scoreGame++;
                faixasTotais.Add(faixa.collider.gameObject);
            }
        }
    }

	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<BoxCollider>();
        tf = GetComponent<Transform>();



        scoreGame = 0;
        coinTotal = 0;
	
	}

    public void StartGameplay(){
        if(GameManager.instance.gameState == GameState.RunnerGame){
            canMove = true;
            isDead = false;
        }

    }

    public void SwipeMove(){
        if (canMove){
            if (perc == 1){
                lerpTime = 1;
                currentLerpTime = 0;
                firstInput = true;
                this.justJump = true;
            }
        }
    }

    protected override void MoveUp()
    {
        SwipeMove();
        base.MoveUp();
    }

    void Update(){

        if(isDead){
            canMove = false;
            return;
        }

        if (GameManager.instance.gameState != GameState.RunnerGame){
            canMove = false;
            return;
        }
        StartGameplay();

    }

    
    void CheckDie(string typeDeath){
        //Faz o personagem morrer e trocar seu mesh.
        switch (typeDeath){
            case "CameraMan":{
                    meshFilter.mesh = meshGameStyle[0];
                    break;
                }
            case "StyleMan":{
                    meshFilter.mesh = meshGameStyle[1];
                    break;
                }
            case "Barber":{
                    meshFilter.mesh = meshGameStyle[2];
                    break;
                }
            case "RanOver":{
                    if (meshRenderer.transform.localScale.z > 0.1)
                        meshRenderer.transform.localScale -= new Vector3(0, 0, 0.1f);
                        break;
                }
        }
        isDead = true;
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Moeda"){
            coinTotal++;
        }
    }

}
