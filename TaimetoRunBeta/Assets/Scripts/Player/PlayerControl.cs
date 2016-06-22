using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    private float lerpTime, currentLerpTime, perc = 1;

    public GameObject gamePanel;
    public Text scoreText;

    private List<GameObject> faixasPass = new List<GameObject>();

    private Vector3 startPos, endPos;
    public bool firstInput = true, isDead = false;
	public bool justJump;
    public AnimationController animationController;
    private int scoreGame;


    private BoxCollider boxCollider;

    void Start() {
        scoreGame = 0;
        boxCollider = GetComponent<BoxCollider>();
        animationController = GetComponent<AnimationController>();
        gamePanel.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
        Debug.Log(faixasPass.Count);
        scoreText.text = scoreGame.ToString();

        RaycastHit hitDown;
        Ray landingRayDown = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(landingRayDown,out hitDown,4)){
            Debug.Log(hitDown.collider.name);
        }


        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || 
		Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && GameManager.instace.isRunGame)
        {
			if (perc == 1) {
				lerpTime = 1;
				currentLerpTime = 0;
				firstInput = true;
				justJump = true;
			}
		}

        //Pegar a posição inicial
        if (GameManager.instace.isRunGame){
            startPos = gameObject.transform.position;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.transform.position == endPos){
                MoveLeft();
                //endPos = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.transform.position == endPos){
                //endPos = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && gameObject.transform.position == endPos){
                //endPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1);
                MoveDown();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.transform.position == endPos){
                MoveUp();
                //endPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
            }

            if (firstInput){
                currentLerpTime += Time.deltaTime * 5.5f;
                perc = currentLerpTime / lerpTime;
                transform.position = Vector3.Lerp(startPos, endPos, perc);
                if (perc > 0.8f){
                    perc = 1;
                }
                if (Mathf.Round(perc) == 1){
                    justJump = false;
                }
            }

        }

	}

    private bool checkJump(Vector3 direction) {
        RaycastHit hit;
        Vector3 rayPoint = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        Ray landingRay = new Ray(rayPoint, direction);
        if (Physics.Raycast(landingRay, out hit, 2.2f)){
            Debug.Log(hit.collider.name);
            if(hit.collider.tag == "Obstaculos"){
                Debug.DrawRay(rayPoint, direction * 2, Color.red);
                return false;
            }
        }
        Debug.DrawRay(rayPoint, direction * 2, Color.green);
        if(direction == Vector3.forward){
            checkFaixa();
        }
        return true;
    }

    void checkFaixa(){
        Debug.Log("Inicio Ponto");
        RaycastHit faixa;
        Vector3 rayPoint = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        Ray rayHit = new Ray(rayPoint, Vector3.down);
        Debug.DrawRay(transform.position, new Vector3(0,-4,0),Color.red,2f);
        if (Physics.Raycast(rayHit, out faixa, 4)){
            Debug.Log("Pegou algo");
            if (faixasPass.Contains(faixa.collider.gameObject) == false){
                Debug.Log("Adicionou");
                scoreGame++;
                faixasPass.Add(faixa.collider.gameObject);
            }
        }
    }

	public void MoveUp(){
        animationController.rotateUp();
        if (checkJump(Vector3.forward) && gameObject.transform.position == endPos){
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
            
        }
    }

	public void MoveDown(){
        animationController.rotateDown();
        if (checkJump(Vector3.back) && gameObject.transform.position == endPos){
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);   
        }
            
	}

	public void MoveRight(){
        animationController.rotateRight();
        if (checkJump(Vector3.right) && gameObject.transform.position == endPos){
            endPos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        }
            
	}

	public void MoveLeft(){
        animationController.rotateLeft();
        if (checkJump(Vector3.left) && gameObject.transform.position == endPos){
            endPos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
        }
            
	}

    public void checkDie(){
        if (isDead){
            GameManager.instace.Die();
        }
    }

}
