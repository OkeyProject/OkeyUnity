using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCard{
	
	private Card.card[][,] CardsInHand;
	private Card.card drawed;
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

	public Card.card enemyDrawCard(){
		if(!GameManager.takeCard){
			Card.card draw = Init.gm.cardstack.draw();
			GameManager.takeCard = true;
			drawed = draw;
			//Debug.Log ("Drawed: "+draw.number.ToString()+" "+draw.color);
			return draw;
		}
		else{
			Card.card empty;
			empty.color = Color.white;
			empty.number = 0;
			Debug.LogError("You have got a card");
			return empty;
		}
	}

	public void enemyDiscardCard(int id,Card.card[,] discard){
		if(GameManager.takeCard){
			List<Card.card> compare = new List<Card.card>();
			for(int j=0;j<2;j++){
				for(int i=0;i<12;i++){
					if(discard[i,j].number!=0&&discard[i,j].color!=Color.white){
						compare.Add(discard[i,j]);
					}
				}
			}

			for(int j=0;j<2;j++){
				for(int i=0;i<12;i++){
					if(CardsInHand[id][i,j].number!=0&&CardsInHand[id][i,j].color!=Color.white){
						if(compare.Contains(CardsInHand[id][i,j])){
							compare.RemoveAt(compare.IndexOf(CardsInHand[id][i,j]));
						}
					}
				}
			}

			/*for(int i=0;i<compare.Count;i++){
				Debug.Log ("Remain: "+compare[i].number.ToString()+" "+compare[i].color);
			}

			Debug.Log ("Drawed: "+drawed.number+" "+drawed.color);*/

			if(compare.Count<=1 && compare.Contains(drawed)){
				for(int j=0;j<2;j++){
					for(int i=0;i<12;i++){
						CardsInHand[id][i,j] = discard[i,j];
					}
				}
				GameManager.takeCard = false;
				if(GameManager.turing == 3)
					GameManager.turing = 0;
				else
					GameManager.turing++;
			}else{
				Debug.LogError("Illegal Discard");
			}
		}
	}

	public Card.card[,] getPlayerCard(){
		return CardsInHand[0];
	}

	public Card.card[,] getEnemyCard(int id){
		return CardsInHand[id];
	}
}
