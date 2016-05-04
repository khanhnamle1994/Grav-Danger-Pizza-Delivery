using UnityEngine;

public abstract class Velocitor : BaseAffector {

    public abstract float velocityMultipler { get; }

    protected override void AffectObject(GameObject other)
    {
        Rigidbody2D rb2d = pv.rg2d;

        Vector2 v = rb2d.velocity;

        rb2d.AddForce(v.normalized * rb2d.mass * velocityMultipler,ForceMode2D.Force);

    }
}
