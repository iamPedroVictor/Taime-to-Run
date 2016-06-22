using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    public float velocidade = 100;

    // Update is called once per frame
    void Update(){

        this.transform.Rotate(new Vector3(0, velocidade * Time.deltaTime, 0));
    }
}
