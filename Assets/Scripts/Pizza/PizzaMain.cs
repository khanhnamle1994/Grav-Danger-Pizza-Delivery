using UnityEngine;
using System.Collections;

public class PizzaMain : MonoBehaviour {

    public Vector2 initialVelocity;

    bool helpOn = false;
    string helpString = "Particle Help\nLeftClick or RightClick creates:\nAttractor    | Repulsor\nFastor        | Slowor(Hold Shift)\n        \nLeftClick also\nErases Particles";
    string helpStringNone = "";



    internal Rigidbody2D rb2d { get; private set; }
    internal TextController tc { get; private set; }
    internal PizzaInventory pi { get; private set; }
    internal PizzaMove pm { get; private set; }
    internal PizzaSceneManager psm { get; private set; }
    internal PointToGoal ptg { get; private set; }
    internal Goal gl { get; private set; }


    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        tc = FindObjectOfType<TextController>();

        pi = GetComponent<PizzaInventory>();
        pm = GetComponent<PizzaMove>();
        psm = GetComponent<PizzaSceneManager>();
        ptg = GetComponentInChildren<PointToGoal>();


        rb2d.velocity = initialVelocity;

        gl = FindObjectOfType<Goal>();
        
        
        // if goal doesn't exist, but point to goal exists
        // disable the point to goal
        if(gl == null && ptg == null)
        {
            ptg.gameObject.SetActive(false);
        }

        // if goal and point to goal exist, 
        // assign point to goal
        if(gl != null && ptg != null)
        {
            ptg.goal = gl;
            ptg.finish = gl.gameObject;

        }

        pm.rg2d = rb2d;
        pm.pi = pi;
        tc.pi = pi;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {

            if (!helpOn)
            {
                tc.SetHelpText(helpString);
                helpOn = true;
            }
            else
            {
                tc.SetHelpText(helpStringNone);
                helpOn = false;
            }
        }
    }
}
