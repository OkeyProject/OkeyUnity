using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {
	public static GameManager gm;
	// Use this for initialization
	void Start () {
		gm = new GameManager ();
		gm.deal ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
