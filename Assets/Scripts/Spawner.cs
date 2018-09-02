using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	private static Spawner instance;
	public static Spawner Instance { 
		get { 	
			return instance; 
		}
	}

	public GameObject[] Pool;
	public float SpawnInterval;

	void Start() {
		if (Spawner.instance == null) {
			Spawner.instance = this;
		} else {
			Debug.LogError ("cannot have two spawner");
		}

		//StartCoroutine (PeriodicalSpawn ());
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			Spawn ();
		}
	}

	public IEnumerator PeriodicalSpawn() {
		while(true) {
			yield return new WaitForSeconds(SpawnInterval);
			Spawn ();
		}
	}

	void Spawn() {
		int personType = Random.Range (0, Pool.Length);
		int startFloor = Random.Range (1, World.Instance.MaxFloor + 1);
		int targetFloor = Random.Range (1, World.Instance.MaxFloor + 1);

		while (targetFloor == startFloor) {
			targetFloor = Random.Range (1, World.Instance.MaxFloor);
		}

		GameObject personObj = Instantiate (Pool[personType]);

		Person person = personObj.GetComponent<Person> ();
		person.Init (startFloor, targetFloor);
	}
}
