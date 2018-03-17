using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement; 

public class SnakeController : MonoBehaviour {

	public static bool ate; 
	public static bool ateSpecial ;

	[Range(1,4)]
	public float speed; 
	public GameObject tailPrefab;
	public AudioClip foodAudio; 
	public AudioClip specialFoodAudio;


	private AudioSource audioSource;
	private List<Transform> tail = new List<Transform>();
	private int startTail =4; 
	private float space = 0.3f; 
	private bool dontCheckCollision; 
	private float dontCheckCollisionTime; 
	private float enableCheckingCollision; 
	private bool turnRight; 
	private bool turnLeft; 


	void Start()
	{ 
		ate = false;
		ateSpecial = false;
		dontCheckCollision = false;
		dontCheckCollisionTime = (1 / speed) + 0.1f;
		turnRight = false;
		turnLeft = false; 

		audioSource = GetComponent<AudioSource>();

		for (int i = 0; i < startTail; i++) 
		{
			AddTail ();
		}

		InvokeRepeating ("Move", 1/speed, 1/speed); 
	}

	void Update()
	{
		if(Input.GetKeyUp("right"))
			turnRight = true; 
		
		else if(Input.GetKeyUp("left"))
			turnLeft = true; 

		if (Input.touchCount > 0) 
		{
			foreach (Touch touch in Input.touches) 
			{
				if (touch.position.x > (Screen.width/2) && !turnRight && !turnLeft) 
				{
					turnRight = true; 

				}
				if (touch.position.x < (Screen.width / 2) && !turnRight && !turnLeft) 
				{
					turnLeft = true; 
				}
			}
		}
			
		if (dontCheckCollision && Time.time > enableCheckingCollision)
			dontCheckCollision = false; 
	}


	void Move()
	{
		if (turnLeft) 
		{
			transform.Rotate (Vector3.forward * 90, Space.Self);
			turnLeft = false; 
		} 
		else if (turnRight) 
		{
			transform.Rotate (-Vector3.forward* 90,Space.Self);
			turnRight = false; 
		}
			
		Vector2 prevPosition = transform.position; 
			
		transform.Translate (Vector2.up*space);  

		if (ate) 
		{
			AddTail (); 
			ate = false; 
			SpawnFood.spawn = true; 
			ScoreController.score +=1;
		}

		if (ateSpecial) 
		{
			AddTail (); 
			ateSpecial = false; 
			ScoreController.score += 10; 
		}

		if (tail.Count > 0) 
		{
			tail.Last ().position = prevPosition; 
			tail.Insert (0, tail.Last ()); 
			tail.RemoveAt (tail.Count- 1); 
		}
			
	}


	void AddTail()
	{ 
		GameObject g = Instantiate (tailPrefab, transform.position, Quaternion.identity) as GameObject;
		tail.Insert (0,g.transform); 
		dontCheckCollision = true; 
		enableCheckingCollision = Time.time + dontCheckCollisionTime;
		
	}
		
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Food") 
		{
			audioSource.PlayOneShot (foodAudio,1); 
			Destroy (coll.gameObject);
			ate = true; 
		}
		if (coll.gameObject.tag =="SpecialFood") 
		{
			audioSource.PlayOneShot (specialFoodAudio,1); 
			Destroy (coll.gameObject);
			ateSpecial = true;
			SpawnFood.isSpecialOnBoard= false;
			SpawnFood.wait = Random.Range (SpawnFood.specialWaitMin,SpawnFood.specialWaitMax); 
			SpawnFood.nextSpecialSpawn = Time.time + SpawnFood.wait;
		}
		if (coll.gameObject.tag == "Border") 
		{
			SceneManager.LoadScene ("GameOver"); 
		}

		if (coll.gameObject.tag == "Player" && !dontCheckCollision) 
		{
			SceneManager.LoadScene ("GameOver"); 
		}
	}

}
