using UnityEngine;
using System.Collections;

public class SpecialEnemy : MovimentBase {

    public Transform targetPlayer;
    public float speed = 10;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(targetPlayer);
	}
}
