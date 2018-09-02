using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	public Queue<Person> WaitLine;
	public int FloorNum;
	public int WaitLineCount;

	void Start() {
		WaitLine = new Queue<Person> ();
	}

	void Update() {
		WaitLineCount = WaitLine.Count;
	}

	public void Dequeue(Elevator elevator) {
		if (elevator.Floor != FloorNum) {
			return;
		}
		if (elevator.Passengers.Count == elevator.Capacity) {
			return;
		}
		if (WaitLine.Count == 0) {
			return;
		}

		while (WaitLine.Count > 0 && elevator.Passengers.Count < elevator.Capacity) {
			Person person = WaitLine.Dequeue ();
			person.Board (elevator);
		}
	}
}
