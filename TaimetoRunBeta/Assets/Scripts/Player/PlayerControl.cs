using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    private float lerpTime, currentLerpTime, perc = 1;

    public Text scoreText;
    public int coinTotal;

	public AudioSource coin;
	public AudioSource voice1;
	public AudioSource voice2;
	public AudioSource voice3;

    private List<GameObject> faixasPass = new List<GameObject>();
    public List<Faixa> faixasT = new List<Faixa>();

    private Vector3 startPos, endPos;
    public bool firstInput = true, isDead = false, canMove;
    public bool justJump;
    public AnimationController animationController;
	public static int scoreGame; 

    private int playerDistancia = 10, distanciaMinima = 6, countJump = 0;

    private BoxCollider boxCollider;

    public GameObject vikingNormal, vikingConfuso, vikingSemBarba, vikingStyle;

    void Start()
    {
        scoreGame = 0;
        boxCollider = GetComponent<BoxCollider>();
        animationController = GetComponent<AnimationController>();
    }

    public void ReloadConfig(){
        endPos = Vector3.zero;
        startPos = Vector3.zero;
        transform.position = Vector3.zero;
        foreach(Faixa i in faixasT){
            faixasT.Remove(i);
        }
        foreach(GameObject g in faixasPass){
            faixasPass.Remove(g);
        }
        playerDistancia = 10;
        scoreGame = 0;
        isDead = false;
        canMove = false;
        vikingConfuso.SetActive(false);
        vikingNormal.SetActive(true);
        vikingSemBarba.SetActive(false);
        vikingStyle.SetActive(false);
    }

    public void StartGamePlay(){
        isDead = false;
        canMove = true;
        startPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update(){

        if (isDead)
            return;

        if (GameManager.instance.gameState != GameState.RunnerGame)
            return;

        scoreText.text = scoreGame.ToString();

        RaycastHit hitDown;
        Ray landingRayDown = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(landingRayDown, out hitDown, 4))
        {
            Faixa t = hitDown.collider.gameObject.GetComponent<Faixa>();
            Debug.Log(hitDown.collider.name);
        }


        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
        Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && GameManager.instance.gameState == GameState.RunnerGame && canMove){
            if (perc == 1){
                lerpTime = 1;
                currentLerpTime = 0;
                firstInput = true;
                justJump = true;
            }
        }

        //Pegar a posição inicial
        if (GameManager.instance.gameState == GameState.RunnerGame)
        {
            startPos = gameObject.transform.position;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.transform.position == endPos){
                MoveLeft();
				int rand = Random.Range (1, 15);
				if (rand == 2){
					voice1.Play ();
				}if (rand == 7){
					voice2.Play ();
				}if (rand == 14){
					voice3.Play ();
				}
                
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.transform.position == endPos){
				MoveRight();
				int rand = Random.Range (1, 15);
				if (rand == 2){
					voice1.Play ();
				}if (rand == 7){
					voice2.Play ();
				}if (rand == 14){
					voice3.Play ();
				}
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && gameObject.transform.position == endPos){ 
				MoveDown();
				int rand = Random.Range (1, 15);
				if (rand == 2){
					voice1.Play ();
				}if (rand == 7){
					voice2.Play ();
				}if (rand == 14){
					voice3.Play ();
				}
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.transform.position == endPos){
				MoveUp();
				int rand = Random.Range (1, 15);
				if (rand == 2){
					voice1.Play ();
				}if (rand == 7){
					voice2.Play ();
				}if (rand == 14){
					voice3.Play ();
				}
            }

            if (firstInput)
            {
                currentLerpTime += Time.deltaTime * 5.5f;
                perc = currentLerpTime / lerpTime;
                transform.position = Vector3.Lerp(startPos, endPos, perc);
                if (perc > 0.8f)
                {
                    perc = 1;
                }
                if (Mathf.Round(perc) == 1)
                {
                    justJump = false;
                }
            }

        }

    }

    private bool checkJump(Vector3 direction)
    {
        RaycastHit hit;
        Vector3 rayPoint = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        Ray landingRay = new Ray(rayPoint, direction);
        if (Physics.Raycast(landingRay, out hit, 2.2f))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Obstaculos")
            {
                Debug.DrawRay(rayPoint, direction * 2, Color.red);
                return false;
            }else if(hit.collider.tag == "Moeda"){
				coin.Play ();
				scoreGame += 2;
                Destroy(hit.collider.gameObject);
            }
        }
        Debug.DrawRay(rayPoint, direction * 2, Color.green);
        if (direction == Vector3.forward)
        {
            checkFaixa();
        }
        return true;
    }

    void checkFaixa()
    {
        RaycastHit faixa;
        Vector3 rayPoint = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        Ray rayHit = new Ray(rayPoint, Vector3.down);
        Debug.DrawRay(transform.position, new Vector3(0, -4, 0), Color.red, 2f);
        if (Physics.Raycast(rayHit, out faixa, 4))
        {
            if (faixasPass.Contains(faixa.collider.gameObject) == false)
            {
                scoreGame++;
                faixasPass.Add(faixa.collider.gameObject);
            }
        }
    }



    public void AdicionarFaixa(Faixa current){
        faixasT.Add(current);
    }

    public void MoveUp(){
        animationController.rotateUp();
        if (checkJump(Vector3.forward) && gameObject.transform.position == endPos)
        {
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);

        }
    }

    public void MoveDown()
    {
        animationController.rotateDown();
        if (checkJump(Vector3.back) && gameObject.transform.position == endPos)
        {
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
        }

    }

    public void MoveRight()
    {
        animationController.rotateRight();
        if (checkJump(Vector3.right) && gameObject.transform.position == endPos)
        {
            endPos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        }

    }

    public void MoveLeft()
    {
        animationController.rotateLeft();
        if (checkJump(Vector3.left) && gameObject.transform.position == endPos)
        {
            endPos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
        }

    }

    public void ChoiseDeath(string enemy){
        if(enemy == "Reporter" || enemy == "CameraMan"){
            vikingConfuso.SetActive(true);
            vikingNormal.SetActive(false);
            vikingSemBarba.SetActive(false);
            vikingStyle.SetActive(false);
        }else if(enemy == "Barbeiro"){
            vikingConfuso.SetActive(false);
            vikingNormal.SetActive(false);
            vikingSemBarba.SetActive(true);
            vikingStyle.SetActive(false);
        }else if(enemy == "Estilista"){
            vikingConfuso.SetActive(false);
            vikingNormal.SetActive(false);
            vikingSemBarba.SetActive(false);
            vikingStyle.SetActive(true);
        }
        CheckDie();
    }

	public void CheckDie(){
		isDead = true;
		GameManager.instance.Die ();
	}
    

}