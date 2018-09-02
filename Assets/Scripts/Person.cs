using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour {

	public int TargetFloor;
	public int StartFloor;
	public float StartPatience;
	public float Patience;
	public float PatianceDecay;

	public bool left;
	public float Speed;

	public Text FloorText;

	void Start() {
		Patience = StartPatience;
		FloorText.text = TargetFloor.ToString();
	}

	public void Init(int start, int target) {
		
		StartFloor = start;
		TargetFloor = target;

		World.Instance.Floors [start - 1].WaitLine.Enqueue (this);
		int count = World.Instance.Floors [start - 1].WaitLine.Count;
		transform.position = new Vector3 (World.Instance.WaitLineStartX - (count - 1) * World.Instance.PersonWidth,
										  World.Instance.FloorToFloorY(StartFloor), 0f);
		transform.SetParent (World.Instance.Floors [start - 1].transform);
	}

	public void Leave() {
		if (left)
			return;
		left = true;
		transform.SetParent (null);
		StartCoroutine (LeaveScreen ());
		World.Instance.Money += 10;
	}

	public IEnumerator LeaveScreen() {
		while (transform.position.x < World.Instance.ScreenRightLimit) {
			transform.position = new Vector2 (transform.position.x + Speed * Time.deltaTime, transform.position.y);
			yield return new WaitForEndOfFrame ();
		}
		yield return null;
	}

	public IEnumerator Board(Elevator elevator) {
		elevator.Passengers.Add(this);
		transform.SetParent (elevator.transform);
		float width = elevator.GetComponent<BoxCollider2D> ().size.x;
		float x = -width / 2f + width / (2 * elevator.Capacity) + (elevator.Passengers.Count - 1) * width / elevator.Capacity;

		while (transform.position.x < x) {
			transform.position = new Vector2 (transform.position.x + Speed * Time.deltaTime, transform.position.y);
			yield return new WaitForEndOfFrame ();
		}
		yield return null;
	}

	void Update() {
		
	}
}
