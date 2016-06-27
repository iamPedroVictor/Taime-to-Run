using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public enum GameState{
    none,
    RunnerGame,
    BattleGame,
    Menu,
    About,
    GameOver
}

public class GameManager : MonoBehaviour {
    public GameState gameState = GameState.none;
    public static GameManager instance = null;

	public Transform localStart;
    public GameObject cameraGame;
    public Transform limp;
    public PlayerControl playerTarget;

	public Camera main;
    public GameObject menuPanel, gameOverPanel, aboutPanel, gamePanel;
    public bool isRunnerGame = false;

	private int highScore;
	public Text highscoreText;
	private int highScoreMax = 0;
	public Text highScoreMaxText;
    private int coinManager;

    public Dictionary<string, string> positionsDictionary = new Dictionary<string, string>();

    void Awake(){
        if (instance == null)
            instance = this;
		if (instance != this)
			Destroy (this);
        DontDestroyOnLoad(this);
        gameState = GameState.Menu;
    }

	// Use this for initialization
	void Start () {
        positionsDictionary.Add("Camera", "-1.5,1.13,-0.26");
        positionsDictionary.Add("Limp", "-1.5,1.13,-10.5");
    }

	public void retry(){
        if (!isRunnerGame){
            gameState = GameState.RunnerGame;
            ReloadScene();
            playerTarget.StartGamePlay();
        }
	}

		
	void ReloadScene(){
        playerTarget.ReloadConfig();
        string[] camerPosition = positionsDictionary["Camera"].Split(',');
        cameraGame.transform.position = new Vector3(float.Parse(camerPosition[0]), float.Parse(camerPosition[1]), float.Parse(camerPosition[2]));
        string[] limpPosition = positionsDictionary["Limp"].Split(',');
        limp.position = new Vector3(float.Parse(limpPosition[0]), float.Parse(limpPosition[1]), float.Parse(limpPosition[2]));
        isRunnerGame = true;

    }

	public void Die(){
        gameState = GameState.GameOver;
    }

    public void verifyScore(){
		if (PlayerControl.scoreGame >= highScoreMax) {
			highScoreMax = PlayerControl.scoreGame;
			highScoreMaxText.text = "Highscore: " + highScoreMax.ToString ();
			highscoreText.text = "Current Score: " + PlayerControl.scoreGame.ToString ();
		} else {
			highscoreText.text = "Current Score: " + PlayerControl.scoreGame.ToString ();
		}

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

    void MenuState(){
        ReloadScene();
    }

    public void StartGame(){
        gameState = GameState.RunnerGame;
        isRunnerGame = true;
        playerTarget.transform.position = localStart.position;
    }

	public void Credits(){
		gameState = GameState.About;
	}

	public void Back(){
		gameState = GameState.Menu;
	}

    public void Restart(){
        ReloadScene();
        StartGame();
    }

	public void Exit(){
		Application.Quit();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        switch (gameState){
            case GameState.Menu:{
                    verifyScore();
                    menuPanel.SetActive(true);
                    gameOverPanel.SetActive(false);
                    aboutPanel.SetActive(false);
                    gamePanel.SetActive(false);
                    MenuState();
                    break;
                }
            case GameState.GameOver:{
                    addCoins(playerTarget.GetComponent<PlayerControl>().coinTotal);
                    isRunnerGame = false;
                    verifyScore();
                    menuPanel.SetActive(false);
                    gameOverPanel.SetActive(true);
                    aboutPanel.SetActive(false);
                    gamePanel.SetActive(false);
                    break;
                }
            case GameState.About:{
					
                    menuPanel.SetActive(false);
                    gameOverPanel.SetActive(false);
                    aboutPanel.SetActive(true);
                    gamePanel.SetActive(false);
                    break;
                }
            case GameState.RunnerGame:{
                    menuPanel.SetActive(false);
                    gameOverPanel.SetActive(false);
                    aboutPanel.SetActive(false);
                    gamePanel.SetActive(true);
                    playerTarget.StartGamePlay();
                    break;
                }
        }

    }
}
