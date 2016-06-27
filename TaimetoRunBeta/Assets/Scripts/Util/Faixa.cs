using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Faixa : MonoBehaviour {

    public GameObject[] obstaculos;
    private List<Vector3> faixaGrid = new List<Vector3>();
    private Vector3 thisVector;
    public bool needCreate = true;
    private int numerosObstaculos;
    private Transform parentThis;


    private bool freeCreate;
    private List<Vector3> locaisPosiveis = new List<Vector3>();

    public GameObject[] inimigosArray;

    void InimigosStart(){
        int inimigoIndex = Random.Range(0, inimigosArray.Length);
        int gridPosision = Random.Range(0, this.faixaGrid.Count);
		GameObject enemy = Instantiate(inimigosArray[inimigoIndex], this.faixaGrid[gridPosision], Quaternion.identity) as GameObject;

    }

    void GridList(){
        faixaGrid.Clear();
        thisVector = GetComponent<Transform>().position;
        numerosObstaculos = Random.Range(0, 3);
        int y = 0;
        for (int i = -6; i < 8; i += 2){
            faixaGrid.Add(new Vector3(i, thisVector.y, thisVector.z));
        }
    }

    void PossiveisLocais(){
        for (int i = -1; i < 1; i++){
            RaycastHit objetos;
            Vector3 rayPoint = new Vector3(transform.position.x + i, transform.position.y + 0.1f, transform.position.z);
            Ray rayHit = new Ray(rayPoint, Vector3.back);
            Debug.DrawRay(transform.position, new Vector3(transform.position.x + i, 0, 0), Color.red, 2f);
            if (Physics.Raycast(rayHit, out objetos, 2)){
                if (objetos.collider != null && objetos.collider.tag == "Obstaculos" && locaisPosiveis.Count < 2){
                    locaisPosiveis.Add(new Vector3(transform.position.x + i, transform.position.y + 0.1f, transform.position.z));
                }
            }
        }
        Debug.Log(locaisPosiveis);
    }

    void CriarObstaculos(){
        PossiveisLocais();
        if (numerosObstaculos != 0){
            for (int i = 0; i < numerosObstaculos; i++){
                int obstaculo = Random.Range(0, obstaculos.Length);
                int posicao = Random.Range(0, faixaGrid.Count);
                GameObject instiateObs = Instantiate(obstaculos[obstaculo], faixaGrid[posicao], Quaternion.identity) as GameObject;
                faixaGrid.Remove(faixaGrid[posicao]);
                instiateObs.transform.SetParent(parentThis);
            }
        }
    }

    void Awake(){
        parentThis = GetComponent<Transform>();
        int random = Random.Range(0, 100);
        if (random > 50){
            freeCreate = true;
        }else{
            freeCreate = false;
        }
    }

	// Use this for initialization
	void Start () {
        if (freeCreate && needCreate){
            GridList();
            CriarObstaculos();
        }else if(!freeCreate && needCreate){
            GridList();
            InimigosStart();
        }
    }


}
