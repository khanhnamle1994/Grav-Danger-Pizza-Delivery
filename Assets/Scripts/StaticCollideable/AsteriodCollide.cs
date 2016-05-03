using UnityEngine;
using System.Collections;

public class AsteriodCollide : MonoBehaviour {
   

   void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PizzaInventory>().DecreaseHealth();
            Destroy(gameObject);   
        }
    }
}
