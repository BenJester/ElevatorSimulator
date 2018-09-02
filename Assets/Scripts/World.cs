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
	public int Money;

	public float ScreenRightLimit;

	public float FloorHeight;
	public float FloorStartY;

	public float WaitLineStartX;
	public float PersonWidth;

	public int MaxFloor;
	public List<Elevator> Elevators;
	public List<Floor> Floors;

	void Start() {
		if (World.instance == null) {
			World.instance = this;
		} else {
			Debug.LogError ("cannot have two worlds");
		}

		Money = 0;

		Elevators = new List<Elevator> ();
		Floors = new List<Floor> ();

		GameObject[] elevatorGameObj = GameObject.FindGameObjectsWithTag ("Elevator");
		for (int i = 0; i < elevatorGameObj.Length; i ++) {
			Elevators.Add (elevatorGameObj [i].GetComponent<Elevator> ());
		}

		GameObject[] floorGameObj = GameObject.FindGameObjectsWithTag ("Floor");
		for (int i = 0; i < floorGameObj.Length; i ++) {
			Floors.Add (floorGameObj [i].GetComponent<Floor> ());
			floorGameObj [i].GetComponent<Floor> ().FloorNum = i + 1;
		}
	}

	public ElevatorPos TouchToElevatorPos(Vector2 pos) {
		Vector3 worldPos = Camera.main.ScreenToWorldPoint (pos);
		Debug.Log (worldPos + " - " + pos);
		ElevatorPos elevatorPos = new ElevatorPos(-1,-1);
		for (int i = 0; i < Elevators.Count; i ++) {
			if (worldPos.x >= Elevators[i].ScreenPosRange.x && worldPos.x < Elevators[i].ScreenPosRange.y) {
				elevatorPos.ID = Elevators [i].ID;
			}
		}

		elevatorPos.Floor = Mathf.CeilToInt ((worldPos.y - FloorStartY) / FloorHeight);

		if (elevatorPos.Floor < 1)
			elevatorPos.Floor = 1;
		return elevatorPos;
	}

	public float FloorToFloorY(int y) {
		return (y - 1) * FloorHeight + FloorStartY + FloorHeight / 2f;
	}

	public Elevator FindElevator(int id) {
		for (int i = 0; i < Elevators.Count; i ++) {
			if (Elevators[i].ID == id) {
				return Elevators [i];
			}
		}
		Debug.LogError("elevator id " + id + " not found");
		return null;
	}

	void Update() {
		/*
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector3.forward, Mathf.Infinity);
		if (hit){
			
		}
		*/
		if (Input.GetMouseButtonDown(0)) {
			ElevatorPos elevatorPos = TouchToElevatorPos(Input.mousePosition);
			if (elevatorPos.ID != -1 && !FindElevator (elevatorPos.ID).Frozen) {
				FindElevator (elevatorPos.ID).TargetFloor = elevatorPos.Floor;
			}
		}

		if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
		{
			ElevatorPos elevatorPos = TouchToElevatorPos(Input.GetTouch (0).position);
			if (elevatorPos.ID != -1 && !FindElevator (elevatorPos.ID).Frozen) {
				FindElevator (elevatorPos.ID).TargetFloor = elevatorPos.Floor;
			}
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
