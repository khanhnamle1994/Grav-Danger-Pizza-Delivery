using UnityEngine;

public abstract class Velocitor : BaseAffector {

    public abstract float velocityMultipler { get; }

    protected override void AffectObject(GameObject other)
    {
        Vector2 v = playerRb2d.velocity;

		other.GetComponent<Rigidbody2D>().AddForce(v.normalized * velocityMultipler,ForceMode2D.Force);
		Debug.Log (velocityMultipler);
    }
}
