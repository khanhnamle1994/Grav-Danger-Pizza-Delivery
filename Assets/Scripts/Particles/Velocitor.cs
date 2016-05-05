using UnityEngine;

public abstract class Velocitor : BaseAffector {

    public abstract float velocityMultipler { get; }

    protected override void AffectObject(GameObject other)
    {
        Vector2 v = playerRb2d.velocity;

        rb2d.AddForce(v.normalized * rb2d.mass * velocityMultipler,ForceMode2D.Force);

    }
}
