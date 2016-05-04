using UnityEngine;
using System.Collections;

public abstract class Gravitor : BaseAffector {

    
    protected abstract bool isAwayCenterForce { get; }

    public float forceMultiplier = 1;

    protected override void AffectObject(GameObject other)
    {
        Vector2 directionVector = OtherToParticleVector2(other);
        float gravityForce = rb2d.mass * .1f * forceMultiplier;
        float force;
        if (isAwayCenterForce)
            force = gravityForce * -1;
        else
            force = gravityForce;
        other.GetComponent<Rigidbody2D>().AddForce(directionVector.normalized * force,ForceMode2D.Force);
    }

    protected Vector2 OtherToParticleVector2(GameObject other)
    {
        Vector3 gravitate3D = gameObject.transform.position - other.transform.position;
        // vector from other (player or asteroid) position to the planet position
        Vector2 gravitate = new Vector2(gravitate3D.x, gravitate3D.y);
        return gravitate;
    }
}
