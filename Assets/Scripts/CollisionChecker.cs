using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour {
    [SerializeField]
    Movement movement;
    [SerializeField]
    Movement.Direction direction;

	// Use this for initialization
	void Start () {
        if (movement == null)
        {
            movement = GetComponentInParent<Movement>();
        }
	}
	
    void OnTriggerStay2D()
    {
        movement.CollEnter(direction);
    }

    void OnTriggerExit2D()
    {
        movement.CollExit(direction);
    }
}
