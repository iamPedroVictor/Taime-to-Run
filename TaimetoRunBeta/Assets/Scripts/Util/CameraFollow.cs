using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    private Vector3 shouldPos;
	
	// Update is called once per frame
	void Update () {
        shouldPos = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        transform.position = new Vector3(shouldPos.x, 1, shouldPos.z);

    }
}
