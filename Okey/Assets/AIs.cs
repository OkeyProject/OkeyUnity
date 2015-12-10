using UnityEngine;
using System.Collections;

public class AIs{
	public PlayerCard AIplayerCard;
	public Card.card[,] hand;
	public AIs(){
		AIplayerCard = Init.gm.playerCard;
	}

	public void runAI(int id){
		hand = new Card.card[12,2];
		Card.card[,] tmpret = AIplayerCard.getEnemyCard(id);
		for(int i=0;i<2;i++)
			for(int j=0;j<12;j++)
				hand[j,i] = tmpret[j,i];
		switch(id){
		case 1:
			AI1();
			break;
		case 2:
			AI2();
			break;
		case 3:
			AI3();
			break;
		}
	}

	private void AI1(){
		Card.card tmp = AIplayerCard.enemyDrawCard();
		hand[0,0] = tmp;
		AIplayerCard.enemyDiscardCard(1,hand);
	}

	private void AI2(){
		Card.card tmp = AIplayerCard.enemyDrawCard();
		hand[0,0] = tmp;
		AIplayerCard.enemyDiscardCard(2,hand);
	}

	private void AI3(){
		Card.card tmp = AIplayerCard.enemyDrawCard();
		hand[0,0] = tmp;
		AIplayerCard.enemyDiscardCard(3,hand);
	}
}
