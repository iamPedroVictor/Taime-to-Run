using UnityEngine;
using System.Collections;

public abstract class MovimentBase : MonoBehaviour {

    private float lerpTime, currentLerpTime, perc = 1;
    protected Vector3 startPos, endPos;
    public bool firstInput = true, isDead = false, justJump;
    public GameObject targetObject;
    protected AnimationController animationControler;
    protected BoxCollider boxCollider;
    [SerializeField]
    protected MeshRenderer meshRenderer;
    [SerializeField]
    protected Transform rayPointTransform;

    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider>();
	
	}

    protected virtual bool checkJump(Vector3 direction){
        RaycastHit hit;
        Vector3 rayPoint = new Vector3(rayPointTransform.position.x, rayPointTransform.position.y, rayPointTransform.position.z);
        Ray landingRay = new Ray(rayPoint, direction);
        if (Physics.Raycast(landingRay, out hit, 2.2f)){
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Obstaculos"){
                Debug.DrawRay(rayPoint, direction * 2, Color.red);
                return false;
            }
        }
        return true;
    }

    public virtual void Move(Vector3 direction) {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
