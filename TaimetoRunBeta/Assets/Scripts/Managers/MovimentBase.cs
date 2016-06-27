using UnityEngine;
using System.Collections;

public abstract class MovimentBase : MonoBehaviour {

    [SerializeField]
    protected float lerpTime, currentLerpTime, perc = 1;
    protected Vector3 startPos, endPos;
    public bool firstInput = true, isDead = false, justJump;
    public GameObject targetObject;
    [SerializeField]
    protected BoxCollider boxCollider;
    [SerializeField]
    protected MeshRenderer meshRenderer;
    [SerializeField]
    protected Transform rayPointTransform;
    [SerializeField]
    protected Transform tf;
    [SerializeField]
    protected bool moveStatus;


    // Use this for initialization
    void Start () {
	
	}

    protected bool checkJump(Vector3 direction){
        RaycastHit hit;
        Vector3 rayPoint = new Vector3(rayPointTransform.position.x, rayPointTransform.position.y, rayPointTransform.position.z);
        Ray landingRay = new Ray(rayPoint, direction);
        if (Physics.Raycast(landingRay, out hit, 2.2f)){
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Obstaculo"){
                Debug.DrawRay(rayPoint, direction * 2, Color.red);
                return false;
            }
        }
        return true;
    }

    protected virtual void Move() {
        if (firstInput){
            while (transform.position != endPos){
                currentLerpTime += Time.deltaTime * 5.5f;
                perc = currentLerpTime / lerpTime;
                transform.position = Vector3.Lerp(startPos, endPos, perc);
                if (perc > 0.8f){
                    perc = 1;
                }
                if(Mathf.Round(perc) == 1){
                    justJump = false;
                }
            }
        }
    }


    #region Metodos para mover
    protected virtual void MoveUp(){
        moveStatus = true;
        if (transform.rotation != Quaternion.Euler(0, 180, 0))
            rotateUp();
        if (checkJump(Vector3.forward) && transform.position == endPos){
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);   
        }
        Move();

    }

    protected virtual void MoveDown(){
        moveStatus = true;
        if (transform.rotation != Quaternion.Euler(0, 0, 0))
            rotateDown();
        if (checkJump(Vector3.back) && transform.position == endPos){
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);  
        }
        Move();


    }

    protected virtual void MoveLeft(){
        moveStatus = true;
        if (transform.rotation != Quaternion.Euler(0, 90, 0))
            rotateLeft();
        if (checkJump(Vector3.left) && transform.position == endPos){
            endPos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z); 
        }
        Move();

    }

    protected virtual void MoveRight(){
        moveStatus = true;
        if (transform.rotation != Quaternion.Euler(0, -90, 0))
            rotateRight();
        if (checkJump(Vector3.right) && transform.position == endPos){
            endPos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        }
        Move();

    }
    #endregion


    #region Metodos para rotacionar
    public void rotateRight(){
        tf.rotation = Quaternion.Euler(0, -90, 0);
    }
    public void rotateLeft(){
        tf.rotation = Quaternion.Euler(0, 90, 0);
    }
    public void rotateUp(){
        tf.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void rotateDown(){
        tf.rotation = Quaternion.Euler(0, 0, 0);
    }
    #endregion


    protected virtual void MoveStatus(){
        if (perc == 1){
            lerpTime = 1;
            currentLerpTime = 0;
            firstInput = true;
            justJump = true;
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
