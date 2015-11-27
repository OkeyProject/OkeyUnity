using UnityEngine;
using System.Collections;

public class CardStatus{
	private Vector3 cardScale;
	private Quaternion cardRotate;
	public CardStatus(){
		cardScale = new Vector3(0.4079833f,0.1563119f,0.6334575f);
		cardRotate = Quaternion.Euler (new Vector3 (270, -180, 0));
	}

	public Vector3 getScale(){
		return cardScale;
	}

	public Quaternion getRotate(){
		return cardRotate;
	}
}
