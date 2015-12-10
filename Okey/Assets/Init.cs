using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {
	public static GameManager gm;
	// Use this for initialization
	void Start () {
		gm = new GameManager ();
		gm.deal ();
		StartCoroutine(gm.runAI());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
