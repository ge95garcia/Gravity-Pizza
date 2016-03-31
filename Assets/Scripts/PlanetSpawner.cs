﻿using UnityEngine;
using System.Collections;

public class PlanetSpawner : MonoBehaviour {

	public GameObject planet;
	public float growthRate;
	bool planetGrowing;
	GameObject newPlanet;
	float cameraSize, aspectRatio;
	Vector3 increase;

	// Use this for initialization
	void Start () {
		growthRate = .1f;
		increase = new Vector3 (growthRate, growthRate, growthRate);
		cameraSize = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		aspectRatio = 1f * Screen.width / Screen.height;
		float height = cameraSize / 5f;
		float width = height * aspectRatio;
		gameObject.transform.localScale = new Vector3(width, 1f, height);
		if (planetGrowing)
			GrowPlanet ();
	}

	void OnMouseDown() {
		planetGrowing = true;
		Shoot ();
	}

	void OnMouseUp() {
		planetGrowing = false;
		increase = new Vector3 (growthRate, growthRate, growthRate);
	}

	void Shoot()
	{
		float y = 2f * Input.mousePosition.y / Screen.height * cameraSize - cameraSize;
		float x = 2f * Input.mousePosition.x / Screen.width * cameraSize * aspectRatio - cameraSize * aspectRatio;
		newPlanet = (GameObject) (Instantiate (planet, new Vector3 (x, y, 0), new Quaternion ()));
	}

	void GrowPlanet()
	{
		if(newPlanet.transform.localScale.x >= 20f)
		{
			//planetGrowing = false;
			increase *= -.5f;
		}
		newPlanet.transform.localScale += increase;
		if(newPlanet.transform.localScale.x <= 0)
		{
			planetGrowing = false;
			Destroy (newPlanet);
			increase = new Vector3 (growthRate, growthRate, growthRate);
		}
		newPlanet.GetComponent<PlanetGravity> ().IncreaseMass ();
	}
}