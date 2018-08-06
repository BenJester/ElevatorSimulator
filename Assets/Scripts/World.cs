using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World : MonoBehaviour {

	private static World instance;
	public static World Instance { 
		get { 	
			return instance; 
		}
	}

	public List<Elevator> Elevators;

	void Start() {
		if (World.instance == null) {
			World.instance = this;
		} else {
			Debug.LogError ("cannot have two worlds");
		}

		Elevators = new List<Elevator> ();
	}

	public ElevatorPos TouchToElevatorPos(Vector2 pos) {
		
		return new ElevatorPos(1,1);
	}

	void LeftClick() {
		/*
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector3.forward, Mathf.Infinity);
		if (hit){
			
		}
		*/
		if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
		{
			
			/*
			Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit raycastHit;
			if (Physics.Raycast(raycast, out raycastHit))
			{
				if (raycastHit.collider.CompareTag("Elevator"))
				{
					Debug.Log("Elevator clicked");
				}
			}
			*/
		}
	}
}
