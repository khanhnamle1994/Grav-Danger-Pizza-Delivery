using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ParticleSpawner : MonoBehaviour {

    public List<GameObject> particles;
    public float growthRate;
    public float decreaseRate;
    public float eraserRadius = 2f;
    bool particleGrowing;
    GameObject currentParticle;
    float cameraSize, aspectRatio;
    Vector3 increase;

    // Use this for initialization
    void Start()
    {
        growthRate = .1f;
        //increase = new Vector3(growthRate, growthRate, growthRate);
        cameraSize = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    // Should the screen resize, the spawner surface will also resize
    // Planet is growing while mouse is clicked
    void Update()
    {

        HandleResizingScreen();

        if (particleGrowing)
            GrowParticle(currentParticle);

        if (!Input.GetButton("Shift"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = ClickLocation();
                Collider2D[] colls = Physics2D.OverlapCircleAll(pos,eraserRadius);
                bool foundParticle = false;
                foreach (Collider2D col in colls)
                {
                    if(col.gameObject.tag=="Particle")
                    {
                        Destroy(col.gameObject);
                        foundParticle = true;
                    }
                }

                if(!foundParticle)
                {
                    currentParticle = CreateParticleCloneAt(particles[0], pos);
                    StartGrowing();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopGrowing();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Vector3 pos = ClickLocation();
                currentParticle = CreateParticleCloneAt(particles[1], pos);
                StartGrowing();
            }

            if (Input.GetMouseButtonUp(1))
            {
                StopGrowing();
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 pos = ClickLocation();
                currentParticle = CreateParticleCloneAt(particles[2], pos);
                StartGrowing();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopGrowing();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Vector3 pos = ClickLocation();
                currentParticle = CreateParticleCloneAt(particles[3], pos);
                StartGrowing();
            }

            if (Input.GetMouseButtonUp(1))
            {
                StopGrowing();
            }
        }
    }

    void HandleResizingScreen()
    {
        aspectRatio = 1f * Screen.width / Screen.height;
        float height = cameraSize / 5f;
        float width = height * aspectRatio;
        gameObject.transform.localScale = new Vector3(width, 1f, height);
    }

    void StartGrowing()
    {
        particleGrowing = true;
    }

    /// <summary>
    /// Stops the current particle from growing
    /// 
    /// public to allow BaseAffector to stop growing if collides with the player
    /// </summary>
    public void StopGrowing()
    {
        particleGrowing = false;
    }


    GameObject CreateParticleCloneAt(GameObject particle, Vector3 pos)
    {
        return (GameObject)Instantiate(particle, pos, Quaternion.identity);
    }
    
    Vector3 ClickLocation()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }


    void GrowParticle(GameObject particle)
    {
        float limit = 20f;

        BaseAffector baseParticle = particle.GetComponent<BaseAffector>();

        // if planet larger than limit
        // start decreasing the planet
        if (particle.transform.localScale.x >= limit)
        {
            baseParticle.DecreaseSize(decreaseRate);
        }
        if (particle.transform.localScale.x <= 0)
        {
            particleGrowing = false;
            particle.GetComponent<BaseAffector>().TryExplode();
            
        }
        baseParticle.IncreaseSize(growthRate);
        baseParticle.IncreaseMass();
    }
}
