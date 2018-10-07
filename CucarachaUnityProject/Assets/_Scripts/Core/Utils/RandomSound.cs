using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour {

	[SerializeField]
	AudioClip[] sounds;

	AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();

		source.clip = sounds[Random.Range(0,sounds.Length)];
		source.Play();
	}
}
