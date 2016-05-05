using UnityEngine;
using System.Collections.Generic;

public class PointToGoal : MonoBehaviour {

	internal GameObject finish { get; set; }
	public GameObject player;
	SpriteRenderer arrow;
	Vector3 direction;
	public bool enableAtStart;

    internal Goal goal { get; set; }
    Transform currentTarget;

    private float lastReallocationTime = 0f;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag ("Player");


		arrow = gameObject.GetComponent<SpriteRenderer> ();
		arrow.enabled = enableAtStart;

        if(goal.IsCheckIngredients)
        {
            // checking for ingredients
            PizzaInventory pi = player.GetComponent<PizzaInventory>();
            pi.onInventoryChange += () =>
            {
                ReallocateTarget();
            };


            ReallocateTarget();

        }
        else
        {
            currentTarget = finish.transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (finish!= null)
        {
            TurnArrowTo(currentTarget);
            if ((Time.time - lastReallocationTime) > 6f)
                ReallocateTarget();
        }
    }

    private void TurnArrowTo(Transform currentTarget)
    {
        Vector3 dir = currentTarget.position - player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);
    }

    private void ReallocateTarget()
    {
        currentTarget = FindCurrentTarget();
        lastReallocationTime = Time.time;
    }

    private Transform FindCurrentTarget()
    {
        List<string> requirements = goal.RemainingIngredients();
        if (requirements.Count == 0)
            return finish.transform;
        else
        {
            Transform tempTarget = NearestTransform(PossibleTargets());
			if (tempTarget == null)
				return null;
            tempTarget.GetComponent<Pickup>().onPickUpDeath += () =>
            {
                ReallocateTarget();
            };
            return tempTarget;
        }
    }
    
    // acquires possible targets
    // namely possible ingredients to find
    private List<Transform> PossibleTargets()
    {
        List<string> requirements = goal.RemainingIngredients();
        List<Transform> targets = new List<Transform>();
        Pickup [] allPickups = GameObject.FindObjectsOfType<Pickup>();
        
        foreach (Pickup pickup in allPickups)
        {
            if (requirements.Contains(pickup.ItemName))
                targets.Add(pickup.gameObject.transform);
        }
        if (targets.Count == 0)
            //throw new UnityException("No possible target found for ingredients");
			Debug.Log("FORGET ABOUT IT");
        return targets;
    }

    // REturns nearest transform of list transforms to the current transform held
    private Transform NearestTransform(List<Transform> transforms)
    {
        Transform nearestTransform = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach( Transform curTransform in transforms)
        {
            Vector3 directionToTarget = curTransform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                nearestTransform = curTransform;
            }
        }
        return nearestTransform;
    }

	public void ToggleArrow(bool on)
	{
		arrow.enabled = on;
	}

    void OnDestroy()
    {

    }
}
