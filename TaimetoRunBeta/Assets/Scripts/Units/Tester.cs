using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour {


    private Player scriptPlayer;

    void Start()
    {
        scriptPlayer = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scriptPlayer.SwipeMove();
        }
	
	}
}
