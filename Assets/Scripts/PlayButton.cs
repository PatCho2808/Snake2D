using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayButton : MonoBehaviour {

	AudioSource audio; 

	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	public void PlayGame()
	{
		StartCoroutine(PlayAudio());
		SceneManager.LoadScene ("Game");
	}

	IEnumerator PlayAudio()
	{
		
		audio.Play();

		yield return new WaitForSeconds(audio.clip.length);
	}
}
