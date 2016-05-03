using UnityEngine;
using System.Collections;

public abstract class BaseAffector : MonoBehaviour
{

    //protected float physicalRadius { get; set; }
    [SerializeField]
    private float gravityRadius; // rename to effectiveRadius

    [SerializeField]
    private float initial_mass;

    public float explodeMultiplier;
    public bool explodeOnCollide;
    protected Rigidbody2D rb2d;
    protected PizzaVelocity pv;

    // Use this for initialization
    void Start()
    {
        pv = GameObject.FindGameObjectWithTag("Player").GetComponent<PizzaVelocity>();
        DelayedDestroySelf();
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.mass = initial_mass;
    }

    // Update is called once per frame
    void Update()
    {
        // not sure why this is called here
        // could possibly be moved to when gravity is actually called
        // insteaad of every frame, even when out of range
        //gravityForce = rb2d.mass * .1f;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // if player within planet effective gravity radius then
        // start add force to the player
        if (other.gameObject.tag == "Player")
        {
            AffectObject(other.gameObject);
        }
    }

    protected abstract void AffectObject(GameObject other);

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<ParticleSpawner>().GetComponent<ParticleSpawner>().StopGrowing();
            if (explodeOnCollide)
                Explode();
            Destroy(gameObject);
        }
    }

    //void OnMouseDown()
    //{
    //    Destroy(gameObject);
    //}

    public void IncreaseMass()
    {
        rb2d.mass += .1f;
    }

    public void DecreaseMass()
    {
        rb2d.mass -= .3f;
    }

    public void IncreaseSize(float f)
    {
        transform.localScale += new Vector3(f,f,f);
    }

    public void DecreaseSize(float f)
    {
        IncreaseSize(-f);
    }



    // Explode only if explodeOnCollide is true
    public void TryExplode()
    {
        if (explodeOnCollide)
            Explode();
    }

    // Guaranteened to explode
    public void Explode()
    {
        Vector3 difference = gameObject.transform.position - pv.gameObject.transform.position;
        difference.z = 0;

        float distance = difference.magnitude;

        if (distance < 100f)
        {
            pv.AddForce(-difference * rb2d.mass * explodeMultiplier / distance / distance);
        }

    }


    public void DelayedDestroySelf()
    {
        StartCoroutine("DestroySelf");
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(60);
        DestroyObject(gameObject);
    }

}
