using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    private Vector3 shouldPos;
    private float velocity = 0.5f;
	// Update is called once per frame
	void Update () {
        if (GameManager.instace.isRunGame){
            shouldPos = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 0.5f);
            transform.position = new Vector3(shouldPos.x, 1, shouldPos.z);
        }

    }
}
