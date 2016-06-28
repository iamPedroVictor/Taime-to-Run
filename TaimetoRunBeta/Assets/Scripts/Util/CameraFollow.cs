using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    
   // public Transform target;
   // private Vector3 shouldPos;
    private float velocity = 0.5f;
    public GameObject faixaRef;
    public int playerDistancia = 10;
    private float time = 0.6f;

	// Update is called once per frame

	void Update () {
        if (GameManager.instance.gameState == GameState.RunnerGame){
            //shouldPos = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 0.5f);
            //transform.position = new Vector3(shouldPos.x, 1, shouldPos.z);
            transform.position = new Vector3(transform.position.x, 1, transform.position.z + velocity * Time.deltaTime);
        }else{
            playerDistancia = 10;
        }

        if(time > 0 && GameManager.instance.gameState == GameState.RunnerGame){
            time -= Time.deltaTime;
        }else if(time <= 0 && GameManager.instance.gameState == GameState.RunnerGame){
            time = 0.6f;
            CreateFaixa();
        }

    }


    void CreateFaixa(){
        playerDistancia += 2;
        GameObject faixas = Instantiate(faixaRef, new Vector3(0, -0.04999995f, playerDistancia), Quaternion.identity) as GameObject;
    }
}
