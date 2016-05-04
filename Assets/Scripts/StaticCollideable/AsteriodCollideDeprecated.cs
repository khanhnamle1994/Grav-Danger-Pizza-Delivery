using UnityEngine;
using System.Collections;

public class AsteriodCollideDeprecated : MonoBehaviour {
   

   void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PizzaInventory>().DecreaseHealth();
            Destroy(gameObject);   
        }
    }
}
