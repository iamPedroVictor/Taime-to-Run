using UnityEngine;
using System.Collections;

public class OnStop : MonoBehaviour {
	public string PauseSplashTAG;
	private bool Paused;
	private GameObject PauseSplash;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnApplicationPause(bool pauseState)
	{
		Paused = pauseState;
		
		if (Paused == true) {
			Time.timeScale = 0;
			AudioListener.volume = 0.0f;
			//Caso desejem por Splash de pause no meio do jogo DESCOMENTAR abaixo
//			PauseSplash=GameObject.FindGameObjectWithTag(PauseSplashTAG);
//			PauseSplash.SetActive(true);
		} else {
			//Caso desejem por Splash de pause no meio do jogo COMENTAR abaixo
			//(Seus "pauses gameobjects) devem despausar o jogo
			Time.timeScale = 1;

			AudioListener.volume = 1.0f;
		}
	}
}
