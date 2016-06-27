using UnityEngine;
using System.Collections;

public class Limpeza : MonoBehaviour {
    private float velocity = 1f;
    public bool podeDeletar = true;
    public bool isJogador = false;
    public Transform refLimpeza;
    // Use this for initialization
    void Start () {
        if (this.name != "Limpeza")
            refLimpeza = GameObject.Find("Limpeza").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.gameState == GameState.RunnerGame)
        {
            if (podeDeletar){
                if(this.transform.position.z < refLimpeza.position.z){
                    if (isJogador){
                        this.GetComponent<PlayerControl>().CheckDie();
                    }else{
                        Destroy(this.gameObject);
                    }
                        
                }

            }else{
                transform.position = new Vector3(transform.position.x, 1, transform.position.z + velocity * Time.deltaTime);
            }
        }
    }
}
