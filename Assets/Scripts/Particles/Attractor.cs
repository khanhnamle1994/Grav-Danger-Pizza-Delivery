

public class Attractor : Gravitor {
    protected override bool isAwayCenterForce
    {
        get
        {
            return false;
        }
    }
}
