using UnityEngine;
using System.Collections;

public class PlanetSpawner : MonoBehaviour {

	public GameObject planet;
	public float growthRate;
    public float decreaseRate;
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

    public void StopGrowing()
    {
        planetGrowing = false;
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

        //Camera.main.ScreenToWorldPoint(Input.mousePosition);
		newPlanet = (GameObject) (Instantiate (planet, new Vector3 (x, y, 0), new Quaternion ()));
	}

    /*
        
    */
	void GrowPlanet()
	{
        // if planet larger than 20 
        // start decreasing the planet
		if(newPlanet.transform.localScale.x >= 20f)
		{
			//planetGrowing = false;
			increase *= decreaseRate;
		}
		newPlanet.transform.localScale += increase;
		if(newPlanet.transform.localScale.x <= 0)
		{
            Debug.Log("attractor dead");
			planetGrowing = false;
            newPlanet.GetComponent<PlanetGravity>().Explode();
			//Destroy (newPlanet);
			increase = new Vector3 (growthRate, growthRate, growthRate);
		}
		newPlanet.GetComponent<PlanetGravity> ().IncreaseMass ();
	}
}
