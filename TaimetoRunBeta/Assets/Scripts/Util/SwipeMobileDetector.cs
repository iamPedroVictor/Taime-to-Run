using UnityEngine;
using System.Collections;

public class SwipeMobileDetector : MonoBehaviour {
	
	
	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
	private float minSwipeDist  = 50.0f;
	private float maxSwipeTime = 0.5f;

	public AudioSource voice1;
	public AudioSource voice2;
	public AudioSource voice3;

    public PlayerControl scriptPlayer;

    void Start(){
        scriptPlayer = GetComponent<PlayerControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.gameState != GameState.RunnerGame)
            return;

        if (Input.touchCount > 0){
			
			foreach (Touch touch in Input.touches)
			{
				switch (touch.phase)
				{
				case TouchPhase.Began :
					/* this is a new touch */
					isSwipe = true;
					fingerStartTime = Time.time;
					fingerStartPos = touch.position;
					break;
					
				case TouchPhase.Canceled :
					/* The touch is being canceled */
					isSwipe = false;
					break;
					
				case TouchPhase.Ended :
					
					float gestureTime = Time.time - fingerStartTime;
					float gestureDist = (touch.position - fingerStartPos).magnitude;
					
					if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
						Vector2 direction = touch.position - fingerStartPos;
						Vector2 swipeType = Vector2.zero;
						
						if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
							// the swipe is horizontal:
							swipeType = Vector2.right * Mathf.Sign(direction.x);
						}else{
							// the swipe is vertical:
							swipeType = Vector2.up * Mathf.Sign(direction.y);
						}
						
						if(swipeType.x != 0.0f){
							if(swipeType.x > 0.0f){
								// MOVE RIGHT
								int rand = Random.Range (1, 15);
								if (rand == 2){
									voice1.Play ();
								}if (rand == 7){
									voice2.Play ();
								}if (rand == 14){
									voice3.Play ();
								}
								BroadcastMessage("MoveRight");
                                BroadcastMessage("OkPodeAcontecer");
                                }
                                else{
								// MOVE LEFT
								int rand = Random.Range (1, 15);
								if (rand == 2){
									voice1.Play ();
								}if (rand == 7){
									voice2.Play ();
								}if (rand == 14){
									voice3.Play ();
								}
								BroadcastMessage("MoveLeft");
                                BroadcastMessage("OkPodeAcontecer");
                            }
						}
						
						if(swipeType.y != 0.0f ){
							if(swipeType.y > 0.0f){
								// MOVE UP
								int rand = Random.Range (1, 15);
								if (rand == 2){
									voice1.Play ();
								}if (rand == 7){
									voice2.Play ();
								}if (rand == 14){
									voice3.Play ();
								}
								BroadcastMessage("MoveUp");
                                BroadcastMessage("OkPodeAcontecer");
                              }
                                else{
								// MOVE DOWN
								int rand = Random.Range (1, 15);
								if (rand == 2){
									voice1.Play ();
								}if (rand == 7){
									voice2.Play ();
								}if (rand == 14){
									voice3.Play ();
								}
								BroadcastMessage("MoveDown");
                                BroadcastMessage("OkPodeAcontecer");
                           }
						}
						
					}
					
					break;
				}
			}
		}
		
	}
}