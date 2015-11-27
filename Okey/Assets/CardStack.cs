using UnityEngine;
using System.Collections;

public class CardStack{
	private int size;
	private Card.card[] cards;

	public CardStack(){
		Debug.Log ("CardStack Created");
		size = 104;
		cards = new Card.card[105];
		for (int i=1; i<=104; i++) {
			cards[i].number = i%13;
			if(i%13==0)
				cards[i].number = 13;
			if(i/26==0){
				cards[i].color = Color.blue;
			}else if(i/26==1){
				cards[i].color = Color.red;
			}else if(i/26==2){
				cards[i].color = Color.yellow;
			}else{
				cards[i].color = Color.black;
			}
		}
		shuffle ();
	}

	private void shuffle(){
		for (int i=0; i<40000; i++) {
			int ran1 = Random.Range(1,105);
			int ran2 = Random.Range(1,105);
			Card.card tmp = cards[ran1];
			cards[ran1] = cards[ran2];
			cards[ran2] = tmp;
		}
		Debug.Log("Shuffle!");
		return;
	}

	public Card.card draw(){
		if (size <= 0) {
			Debug.LogError ("CardStack Empty");
		} else {
			size--;
			return cards[size+1];
		}
		return cards[0];
	}
}
