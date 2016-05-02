using UnityEngine;
using System.Collections;

public class AsteriodCollide : MonoBehaviour {

   // public string item;
    //public string item2;

    //string[] items = new string[2]; 

   void Start()
    {
        //items[0] = item;
        //items[1] = item2; 
    }

   

   void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            other.GetComponent<PizzaInventory>().Decrement("Pizza"); 
            //Destroy(gameObject);
            
        }
    }
}
