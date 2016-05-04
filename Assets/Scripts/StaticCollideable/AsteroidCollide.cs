using UnityEngine;
using System.Collections;

public class AsteroidCollide : MonoBehaviour {

    bool recentlyHit = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Player" && !recentlyHit)
        {
            go.GetComponent<PizzaInventory>().DecreaseHealth();
            recentlyHit = true;
            StartCoroutine("HandleCollide");
        }
    }

    IEnumerator HandleCollide()
    {
        yield return new WaitForSeconds(3);
        recentlyHit = false;
    }
}
