using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ElevatorPos {
	public int ID;
	public int Floor;

	public ElevatorPos(int id, int floor) {
		ID = id;
		Floor = floor;
	}
}

public class Elevator : MonoBehaviour {
	
	public int ID;
	public int Floor;
	public int TargetFloor;
	public float Speed;
	public Vector2 ScreenPosRange;

	//TODO: generate ID, Screen Range automatically

	public int Capacity;
	public List<Person> Passengers;

	public bool Frozen;

	void Start() {
		Passengers = new List<Person> ();
		ScreenPosRange = new Vector2 (transform.position.x - GetComponent<BoxCollider2D> ().size.x, transform.position.x + GetComponent<BoxCollider2D> ().size.x);
	}

	void Update() {
		if (Mathf.Abs(transform.position.y - World.Instance.FloorToFloorY(TargetFloor)) < 5) {
			transform.position = new Vector2 (transform.position.x, World.Instance.FloorToFloorY (TargetFloor));
			Floor = TargetFloor;
			Arrive ();
			return;
		}

		// has not arrived yet
		Floor = 0;
		if (transform.position.y > World.Instance.FloorToFloorY (TargetFloor)) {
			transform.position = new Vector2 (transform.position.x, transform.position.y - Speed * Time.deltaTime);
		} else if (transform.position.y < World.Instance.FloorToFloorY (TargetFloor)) {
			transform.position = new Vector2 (transform.position.x, transform.position.y + Speed * Time.deltaTime);
	
		}
	}

	void Arrive() {
		if (Frozen)
			return;
		Frozen = true;
		Unload ();
		Board ();
		Frozen = false;
	}

	void Board() {
		World.Instance.Floors [Floor - 1].Dequeue (this);
	}

	void Unload() {
		if (Floor == 0)
			return;
		for (int i = 0; i < Passengers.Count; i ++) {
			if (Passengers[i].TargetFloor == Floor) {
				Passengers [i].Leave ();
				Passengers.Remove (Passengers [i]);
			}
		}
	}

}
