using UnityEngine;

public abstract class Velocitor : BaseAffector {

    public abstract float velocityMultipler { get; }

    protected override void AffectObject(GameObject other)
    {
        Rigidbody2D rb2d = pv.rg2d;

        Vector2 v = rb2d.velocity;

        pv.AddForce(v.normalized * velocityMultipler);

    }
}
