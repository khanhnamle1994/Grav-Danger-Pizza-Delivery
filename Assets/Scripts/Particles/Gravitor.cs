using UnityEngine;
using System.Collections;

public abstract class Gravitor : BaseAffector {

    
    protected abstract bool isAwayCenterForce { get; }


    protected override void AffectObject(GameObject other)
    {
        Vector2 directionVector = PlayerToParticleVector2(other);
        float gravityForce = rb2d.mass * .1f;
        float force;
        if (isAwayCenterForce)
            force = gravityForce * -1;
        else
            force = gravityForce;
        pv.AddForce(directionVector.normalized * force);
    }

    protected Vector2 PlayerToParticleVector2(GameObject other)
    {
        Vector3 gravitate3D = gameObject.transform.position - other.transform.position;
        // vector from player position to the planet position
        Vector2 gravitate = new Vector2(gravitate3D.x, gravitate3D.y);
        return gravitate;
    }
}
