using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

	public Text scoreText;  
	public AudioClip gameOverSound; 
	public AudioClip menuSound; 

	AudioSource audioSource; 

	void Awake()
	{
		audioSource = GetComponent<AudioSource> (); 

		audioSource.PlayOneShot (gameOverSound, 1); 
	}

	public void PlayAgain()
	{
		StartCoroutine(PlayMenuAudio());
		SceneManager.LoadScene ("Game");
	}
		
	public void Quit()
	{
		StartCoroutine(PlayMenuAudio());
		SceneManager.LoadScene ("Menu");
	}

	void Start()
	{
		UpdateScore (); 
	}
		
	void UpdateScore()
	{
		scoreText.text = "Score: " + ScoreController.score.ToString(); 
	}

	IEnumerator PlayMenuAudio()
	{

		audioSource.PlayOneShot (menuSound, 1); 

		yield return new WaitForSeconds(menuSound.length);
	}



}
