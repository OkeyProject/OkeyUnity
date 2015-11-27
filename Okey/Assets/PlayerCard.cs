using UnityEngine;
using System.Collections;

public class PlayerCard{
	
	private Card.card[][,] CardsInHand;
	public PlayerCard(){
		CardsInHand = new Card.card[4][,];
		for(int i=0;i<4;i++){
			CardsInHand[i] = new Card.card[12,2];
			for(int j=0;j<12;j++){
				for(int k=0;k<2;k++){
					CardsInHand[i][j,k].number = 0;
					CardsInHand[i][j,k].color = Color.white;				
				}
			}
		}
	}

	public void playerDealCard(int id,int order , Card.card cd){
		CardsInHand[id][order%12,order/12] = cd;
	}

	public void playerMoveCard(int originX, int originY,int newX,int newY){
		Card.card tmp = CardsInHand[0][newX,newY];
		CardsInHand[0][newX,newY] = CardsInHand[0][originX,originY];
		CardsInHand[0][originX,originY] = tmp;
	}

	public void playerDrawCard(Card.card cd,int x,int y){
		CardsInHand[0][x,y] = cd;
	}

	public void playerDiscardCard(int x,int y){
		CardsInHand[0][x,y].number = 0;
		CardsInHand[0][x,y].color = Color.white;
	}

	public Card.card[,] getPlayerCard(){
		return CardsInHand[0];
	}

	public Card.card[,] getEnemyCard(int id){
		return CardsInHand[id];
	}
}
