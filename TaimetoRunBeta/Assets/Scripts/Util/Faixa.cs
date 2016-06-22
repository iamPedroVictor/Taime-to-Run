using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Faixa : MonoBehaviour {

    public GameObject[] obstaculos;
    private List<Vector3> faixaGrid = new List<Vector3>();
    private Vector3 thisVector;
    public bool needCreate = true;
    private int numerosObstaculos;

    void GridList(){
        faixaGrid.Clear();
        thisVector = GetComponent<Transform>().position;
        numerosObstaculos = Random.Range(0, 4);
        for (int i = -6; i < 8; i += 2){
            faixaGrid.Add(new Vector3(i, thisVector.y, thisVector.z));
        }
    }

    void CriarObstaculos(){
        if (numerosObstaculos != 0){
            for (int i = 0; i < numerosObstaculos; i++){
                int obstaculo = Random.Range(0, obstaculos.Length);
                int posicao = Random.Range(0, faixaGrid.Count);
                GameObject instiateObs = Instantiate(obstaculos[obstaculo], faixaGrid[posicao], Quaternion.identity) as GameObject;
            }
        }
    }

	// Use this for initialization
	void Start () {
        if (needCreate){
            GridList();
            CriarObstaculos();
        }
    }


}
