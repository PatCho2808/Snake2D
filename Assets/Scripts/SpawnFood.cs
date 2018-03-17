using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

	public static bool spawn; 
	public static bool isSpecialOnBoard; 
	public static float wait; 
	public static float nextSpecialSpawn; 
	public static float specialWaitMin = 5; 
	public static float specialWaitMax = 15;

	public GameObject prefab; 
	public GameObject specialPrefab;
	public float minWidth; 
	public float maxWith; 
	public float minHeight; 
	public float maxHeight; 
	public int waitSpawn; 

	void Start()
	{
		wait = Random.Range (specialWaitMin,specialWaitMax); 
		spawn = true;
		isSpecialOnBoard = false;
	}

	
	void Update()
	{
		if (spawn) 
		{
			Spawn ();
			spawn = false; 
		}
			
		if (Time.time > nextSpecialSpawn && !isSpecialOnBoard) 
		{
			isSpecialOnBoard = true;  
			SpecialSpawn (); 
		}
			  
	}

	
	 void Spawn()
	{
		float x = Random.Range (minWidth, maxWith); 
		float y = Random.Range (minHeight, maxHeight); 

		GameObject newFood = Instantiate (prefab, new Vector2 (x, y), Quaternion.identity) as GameObject; 

		newFood.transform.parent = transform; 
	}

	
	void SpecialSpawn()
	{
		float x = Random.Range (minWidth, maxWith); 
		float y = Random.Range (minHeight, maxHeight); 

		GameObject newFood = Instantiate (specialPrefab, new Vector2 (x, y), Quaternion.identity) as GameObject; 

		newFood.transform.parent = transform; 
	}


}
