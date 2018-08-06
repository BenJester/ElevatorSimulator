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
	
	public ElevatorPos Pos;
	public float Speed;


}
