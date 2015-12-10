using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 originPosition;
	private Vector3 curPosition;
	private int originX;
	private int originY;
	private int click;

	public struct card{
		public int number;
		public Color color;
	}

	public card val;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,screenPoint.z));
		if(transform.position.x>-5f&&transform.position.x<5f&&transform.position.y<-3f&&transform.position.y>-5.5f){
			getNearestPoint(transform.position,out originX,out originY);
			//Debug.Log(originX+" "+originY);
			originPosition = transform.position;
			click = 0;
		} else if(transform.position.x>-8.5f&&transform.position.x<-7.5f&&transform.position.y<-2.4f&&transform.position.y>-3.5f){
			originPosition = transform.position;
			click = 1;
		} else if(this.tag=="GameController"&&GameManager.turing==0&&!GameManager.takeCard){
			Debug.Log("Draw!");
			click = 2;
		} else{
			click = -1;
		}
	}

	void OnMouseDrag(){
		if(click!=-1){
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x,Input.mousePosition.y, screenPoint.z);
			curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;
			transform.position = curPosition;
		}
	}

	void OnMouseUp(){
		if(click==0){
			if(curPosition.x<8.5f&&curPosition.x>7.5f&&curPosition.y<-2.4f&&curPosition.y>-3.5f&&GameManager.turing==0&&GameManager.takeCard){
				GameObject[] removeObjects = GameObject.FindGameObjectsWithTag("PlayerDiscard");
				for(int i=0;i<removeObjects.Length;i++)
					Destroy(removeObjects[i]);
				transform.position = new Vector3(8,-3,0);
				Init.gm.playerCard.playerDiscardCard(originX,originY);
				GameManager.CardExist[originX,originY] = false;
				GameManager.turing++;
				GameManager.takeCard = false;
				this.tag = "PlayerDiscard";
			} else if(curPosition.x<-5f||curPosition.x>5f||curPosition.y>-3f||curPosition.y<-5.5f){
				transform.position = originPosition;
			} else {
				int x,y;
				getNearestPoint(curPosition,out x,out y);
				if(GameManager.CardExist[x,y]){
					transform.position = originPosition;
				}else{
					transform.position = new Vector3(GameManager.CardAxisX[x],GameManager.CardAxisY[y],0);
					GameManager.CardExist[x,y] = true;
					GameManager.CardExist[originX,originY] = false;
					Init.gm.playerCard.playerMoveCard(originX,originY,x,y);
				}
			}
		} else if(click==1){
			transform.position = originPosition;
		} else if(click==2){
			if(curPosition.x>-5f&&curPosition.x<5f&&curPosition.y<-3f&&curPosition.y>-5.5f){
				int x,y;
				getNearestPoint(curPosition,out x,out y);
				if(!GameManager.CardExist[x,y]){
					Card.card newDrawCard = Init.gm.cardstack.draw();
					string objname = "C"+newDrawCard.number;
					GameObject newDrawCardObj = Instantiate(Resources.Load(objname),new Vector3(GameManager.CardAxisX[x],GameManager.CardAxisY[y],0),Init.gm.cardStatus.getRotate()) as GameObject;
					newDrawCardObj.transform.localScale = Init.gm.cardStatus.getScale();
					newDrawCardObj.AddComponent<Card>();
					newDrawCardObj.GetComponent<Card>().val = newDrawCard;
					Init.gm.playerCard.playerDrawCard(newDrawCard,x,y);
					GameManager.CardExist[x,y] = true;
					GameManager.takeCard = true;
				}
			}
			transform.position = new Vector3(-1,1,0);
		}
	}

	private void getNearestPoint(Vector3 vec, out int x,out int y){
		if(vec.x<-5f||vec.x>5f){
			x = -1; 
			y =-1;
		}else {
			if (vec.y<-3f&&vec.y>=-4.25f){
				y = 0;
			} else if (vec.y<-4.25f&&vec.y>=-5.5f){
				y = 1;
			} else
				y = -1;


			if(vec.x<=GameManager.CardAxisX[0]){
				x = 0;
				return;
			}else if(vec.x>GameManager.CardAxisX[11]){
				x = 11;
				return;
			} else{
				for(int i=0;i<11;i++){
					if(vec.x>GameManager.CardAxisX[i]&&vec.x<=GameManager.CardAxisX[i+1]){
						if(Mathf.Abs(GameManager.CardAxisX[i]-vec.x)<Mathf.Abs(GameManager.CardAxisX[i+1]-vec.x))
							x = i;
						else
							x =i+1;

						return;
					}
				}
				x = -1;
			}
		}
	}
}
