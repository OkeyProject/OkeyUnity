using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour{
	public CardStatus cardStatus;
	public CardStack cardstack;
	public static float[] CardAxisX;
	public static float[] CardAxisY;
	public static bool[,] CardExist;
	public Card.card leadCard;
	public static int turing; 
	public static bool takeCard;
	public PlayerCard playerCard;

	public GameManager(){
		cardstack = new CardStack ();
		cardStatus = new CardStatus ();
		CardAxisX = new float[12];
		CardAxisY = new float[2];
		CardExist = new bool[12,2];
		for(int i=0;i<12;i++)
			for(int j=0;j<2;j++)
				CardExist[i,j] = false;
		CardAxisY[0] = -3.75f;
		CardAxisY[1] = -5f;
		getCardAxisX ();
		turing = 0;
		takeCard = false;
	}

	private void getCardAxisX(){
		float cardSacleX = cardStatus.getScale ().x;
		float pos = 0 - cardSacleX * 11f;
		for (int i=0; i<12; i++) {
			CardAxisX[i] = pos; 
			pos+=cardSacleX*2f;
		}
	}

	public void deal(){
		playerCard = new PlayerCard();
		int ay = 0, ax = 0;
		for (int i=0; i<14; i++) {
			for(int id=0;id<4;id++){
				Card.card cardVal = cardstack.draw();
				playerCard.playerDealCard(id,i,cardVal);
				if(id==0){
					//Debug.Log(cardVal.number+" "+cardVal.color);
					ax = i;
					if(i>=12){
						ay = 1;
						ax = i-12;
					}
					string objname = "C"+ cardVal.number;
					GameObject newCard = Instantiate (Resources.Load(objname),new Vector3(CardAxisX[ax],CardAxisY[ay],-0.19f),cardStatus.getRotate()) as GameObject;
					newCard.transform.localScale = cardStatus.getScale();
					newCard.AddComponent<Card>();
					newCard.GetComponent<Card>().val = cardVal;

					Renderer newCardRenderer = newCard.GetComponent<Renderer>();
					newCardRenderer.materials[1].SetColor("_Color",cardVal.color);
					
					CardExist[ax,ay] = true;
				}
			}
		}
		leadCard = cardstack.draw();
		string leadobj = "C"+leadCard.number;
		GameObject newLeadCard = Instantiate(Resources.Load(leadobj),new Vector3(1,1,-0.19f),cardStatus.getRotate()) as GameObject;
		newLeadCard.transform.localScale = cardStatus.getScale();
		newLeadCard.AddComponent<Card>();
		Destroy(newLeadCard.GetComponent<BoxCollider>());
		newLeadCard.GetComponent<Card>().val = leadCard;

		Renderer newLeadCardRenderer = newLeadCard.GetComponent<Renderer>();
		newLeadCardRenderer.materials[1].SetColor("_Color",leadCard.color);
	}

}
