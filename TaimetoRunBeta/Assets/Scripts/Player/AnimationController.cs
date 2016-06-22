using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	private Animator anime;
	public GameObject modelTarget;
    private PlayerControl playerControlScript;
    private Transform tf;


	// Use this for initialization
	void Start () {
        playerControlScript = GetComponent<PlayerControl> ();
		anime = modelTarget.GetComponent<Animator> ();
        tf = GetComponent<Transform>();
	
	}

    public void rotateRight() {
        tf.rotation = Quaternion.Euler(0, -90, 0);
    }
    public void rotateLeft() {
        tf.rotation = Quaternion.Euler(0, 90, 0);
    }
    public void rotateUp() {
        tf.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void rotateDown() {
        tf.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update () {
		if (playerControlScript.justJump) {
			anime.SetBool ("Jump", true);
		} else {
			anime.SetBool ("Jump", false);
		}
	
	}
}
