using UnityEngine;
using System.Collections;

public class BaseAffector : MonoBehaviour
{

    float physicalRadius, gravityRadius;
    public float mass;
    protected bool isAwayCenterForce;
    //float gravityForce; // deprecated not used
    /// </summary>
    public float explodeMultiplier;
    public bool explodeOnCollide;
    Rigidbody2D rb2d;

    PizzaVelocity pv;

    // Use this for initialization
    void Start()
    {
        pv = GameObject.FindGameObjectWithTag("Player").GetComponent<PizzaVelocity>();
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.mass = mass;
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

    void AffectObject(GameObject other)
    {
        Vector3 gravitate3D = gameObject.transform.position - other.transform.position;


        // vector from player position to the planet position
        Vector2 gravitate = new Vector2(gravitate3D.x, gravitate3D.y);

        float distance = gravitate.magnitude;

        float gravityForce = rb2d.mass * .1f;
        float force;
        if (isAwayCenterForce)
            force = gravityForce * -1;
        else
            force = gravityForce;
        pv.AddForce2(gravitate.normalized * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindObjectOfType<PlanetSpawner>().GetComponent<PlanetSpawner>().StopGrowing();
            if (explodeOnCollide)
                Explode();
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }

    public void IncreaseMass()
    {
        rb2d.mass += .1f;
        //Debug.Log (mass);
    }

    public void Explode()
    {
        Vector3 difference = gameObject.transform.position - pv.gameObject.transform.position;
        difference.z = 0;

        float distance = difference.magnitude;

        Debug.Log("try explode " + distance);
        if (distance < 100f)
        {
            pv.AddForce2(-difference * mass * explodeMultiplier / distance / distance);
        }

    }
}
