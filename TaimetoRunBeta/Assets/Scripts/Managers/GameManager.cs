using UnityEngine;
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

    public bool isRunGame = false, isGameOver = false;

    public GameObject player;

    public GameObject Menu, GameOver, About;

    private int highScore;
    private int coinManager;

    public GameState gameState = GameState.none;

    void Awake(){
        if (instace == null)
            instace = this;
        DontDestroyOnLoad(this);
        gameState = GameState.Menu;
    }

	// Use this for initialization
	void Start () {
        
	
	}

    public void Die(){
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
        switch (gameState){
            case GameState.Menu:{
                    verifyScore();
                    Menu.SetActive(true);
                    GameOver.SetActive(false);
                    About.SetActive(false);
                    break;
                }
            case GameState.GameOver:{
                    verifyScore();
                    Menu.SetActive(false);
                    GameOver.SetActive(true);
                    About.SetActive(false);
                    isRunGame = false;
                    break;
                }
            case GameState.About:{
                    Menu.SetActive(false);
                    GameOver.SetActive(false);
                    About.SetActive(true);
                    break;
                }
            case GameState.RunnerGame:{
                    Menu.SetActive(false);
                    GameOver.SetActive(false);
                    About.SetActive(false);
                    isRunGame = true;
                    break;
                }
        }

    }
}
