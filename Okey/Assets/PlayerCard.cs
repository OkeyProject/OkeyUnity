using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCard : MonoBehaviour{
	
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
			Card.card dis;
			dis.number = 0;
			dis.color = Color.white;
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
						else{
							dis = CardsInHand[id][i,j];
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

				DiscardShow(id,dis);

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

	private void DiscardShow(int id, Card.card discard){
		GameObject[] removeObjects = GameObject.FindGameObjectsWithTag("P"+id.ToString()+"Discard");
		for(int i=0;i<removeObjects.Length;i++)
			Destroy(removeObjects[i]);

		string objname = "C"+discard.number;
		GameObject newDrawCardObj;
		switch(id){
		case 1:
			newDrawCardObj = Instantiate(Resources.Load(objname),new Vector3(8,1.5f,0),Init.gm.cardStatus.getRotate()) as GameObject;
			break;
		case 2:
			newDrawCardObj = Instantiate(Resources.Load(objname),new Vector3(-8,1.5f,0),Init.gm.cardStatus.getRotate()) as GameObject;
			break;
		case 3:
			newDrawCardObj = Instantiate(Resources.Load(objname),new Vector3(-8,-3,0),Init.gm.cardStatus.getRotate()) as GameObject;
			break;
		default:
			newDrawCardObj = Instantiate(Resources.Load(objname),new Vector3(0,0,0),Init.gm.cardStatus.getRotate()) as GameObject;
			break;
		}
		newDrawCardObj.transform.localScale = Init.gm.cardStatus.getScale();
		newDrawCardObj.AddComponent<Card>();
		newDrawCardObj.GetComponent<Card>().val = discard;
		newDrawCardObj.tag = "P"+id.ToString()+"Discard";

		Renderer newCardRenderer = newDrawCardObj.GetComponent<Renderer>();
		newCardRenderer.materials[1].SetColor("_Color",discard.color);
	}

	public Card.card[,] getPlayerCard(){
		return CardsInHand[0];
	}

	public Card.card[,] getEnemyCard(int id){
		return CardsInHand[id];
	}
}
