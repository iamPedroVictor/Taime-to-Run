using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState{
    none,
    RunnerGame,
    BattleGame,
    Menu,
    About,
    GameOver
}

public class GameManager : MonoBehaviour {

    public static GameManager instace = null;

	public static bool isDead;
	public Transform localStart;

    public bool isRunGame = false, isGameOver = false;

    public GameObject player;

    public GameObject Menu, GameOver, About;

    private int highScore;
    private int coinManager;

    public GameState gameState = GameState.none;

    void Awake(){
        if (instace == null)
            instace = this;
		if (instace != this)
			Destroy (this);
        DontDestroyOnLoad(this);
        gameState = GameState.Menu;
    }

	// Use this for initialization
	void Start () {
        
	
	}

	public void retry(){
		gameState = GameState.Menu;
		SceneManager.LoadScene ("Main");
		isRunGame = false;
	}
		
	void ReloadScene(){
		
	}

	public void Die(){
		isRunGame = false;
        gameState = GameState.GameOver;
    }

    public void verifyScore(){
        if (PlayerPrefs.HasKey("HighScore")){
            highScore = PlayerPrefs.GetInt("HighScore");
        }else{
          highScore = 0;
        }
    }

    public void verifyCoin(){
        if (PlayerPrefs.HasKey("Coin")){
            coinManager = PlayerPrefs.GetInt("Coin");
        }else{
            coinManager = 0;
        }
    }

    public void addCoins(int coinCurrent){
        coinManager += coinCurrent;
        PlayerPrefs.SetInt("Coin", coinManager);
    }

    public void verifyHighScore(int score){
        if(score > highScore){
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void StartGame(){
        gameState = GameState.RunnerGame;
    }

	public void Credits(){
		gameState = GameState.About;
	}

	public void Back(){
		gameState = GameState.Menu;
	}

	public void Exit(){
		Application.Quit();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if (isRunGame == false) {
		//	isRunGame = false;
		//}
        switch (gameState){
            case GameState.Menu:{
				Debug.Log ("Estado menu");
                    verifyScore();
                    Menu.SetActive(true);
                    GameOver.SetActive(false);
                    About.SetActive(false);
                    break;
                }
            case GameState.GameOver:{
				Debug.Log ("Estado game over");
                    verifyScore();
                    Menu.SetActive(false);
                    GameOver.SetActive(true);
                    About.SetActive(false);
                    
                    break;
                }
            case GameState.About:{
                    Menu.SetActive(false);
                    GameOver.SetActive(false);
                    About.SetActive(true);
                    break;
                }
            case GameState.RunnerGame:{
				Debug.Log ("Estado runnerGame");
                    Menu.SetActive(false);
                    GameOver.SetActive(false);
                    About.SetActive(false);
                    isRunGame = true;
                    break;
                }
        }

    }
}
