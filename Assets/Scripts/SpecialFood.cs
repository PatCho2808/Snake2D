using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFood: MonoBehaviour {

	public float blinkingTime; 
	public float blinkingWait; 
	public float lifeTimeMin = 10 ; 
	public float lifeTimeMax =  30; 

	private float lifeTime;
	private Renderer renderer; 
	private bool blinking = false; 
	private float dead;

	void Start()
	{
		renderer = GetComponent<Renderer> (); 
		lifeTime = Random.Range (lifeTimeMin, lifeTimeMax);
		dead = Time.time + lifeTime; 
	}

	void Update()
	{
		if (Time.time > dead) 
		{
			Destroy (this.gameObject); 
			SpawnFood.isSpecialOnBoard= false; 
			SpawnFood.wait = Random.Range (SpawnFood.specialWaitMin,SpawnFood.specialWaitMax); 
			SpawnFood.nextSpecialSpawn = Time.time + SpawnFood.wait;
		}

		if (Time.time > dead - blinkingTime && !blinking) 
		{
			InvokeRepeating ("Blink", 0f,blinkingWait); 
			blinking = true; 
		}
	}

	void Blink()
	{
		if (renderer.enabled)
			renderer.enabled = false;
		else
			renderer.enabled = true; 
	}
}
