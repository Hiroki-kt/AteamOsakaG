using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// ピースクラス
public class Piece : MonoBehaviour
{
	// public.
	public bool deleteFlag;

    /*
	public GameObject Blue_de;
	public GameObject Blue_of;
	public GameObject Blue_sk;
	public GameObject Green_de;
	public GameObject Green_of;
	public GameObject Green_sk;
	public GameObject Purple_de;
	public GameObject Purple_of;
	public GameObject Purple_sk;
	public GameObject Red_de;
	public GameObject Red_of;
	public GameObject Red_sk;
	public GameObject Yellow_de;
	public GameObject Yellow_of;
	public GameObject Yellow_sk;
    */

	// private.
	private Image thisImage;
	private RectTransform thisRectTransform;
	private PieceKind kind;
	private string color;
	private string type;

	private Sprite Red_off;
	private Sprite Red_def;
	private Sprite Red_ski;
	private Sprite Blue_off;
	private Sprite Blue_def;
	private Sprite Blue_ski;
	private Sprite Yellow_off;
	private Sprite Yellow_def;
	private Sprite Yellow_ski;
	private Sprite Purple_off;
	private Sprite Purple_def;
	private Sprite Purple_ski;
	private Sprite Green_off;
	private Sprite Green_def;
	private Sprite Green_ski;

	/*
	private Blue_de blue_de;
	private Blue_of blue_of;
	private Blue_sk blue_sk;
	private Green_de green_de;
	private Green_of green_of;
	private Green_sk green_sk;
	private Purple_de purple_de;
	private Purple_of purple_of;
	private Purple_sk purple_sk;
	private Red_de red_de;
	private Red_of red_of;
	private Red_sk red_sk;
	private Yellow_de yellow_de;
	private Yellow_of yellow_of;
	private Yellow_sk yellow_sk;
	*/

	//-------------------------------------------------------
	// MonoBehaviour Function
	//-------------------------------------------------------
	// 初期化処理
	private void Awake()
	{
		// アタッチされている各コンポーネントを取得
		thisImage = GetComponent<Image>();
		thisRectTransform = GetComponent<RectTransform>();

		// フラグを初期化
		deleteFlag = false;

		Red_off = Resources.Load<Sprite>("red_offence");
		Red_def = Resources.Load<Sprite>("red_defence");
		Red_ski =  Resources.Load<Sprite>("red_skill");
		Blue_off = Resources.Load<Sprite>("blue_offence");
		Blue_def = Resources.Load<Sprite>("blue_defence");
		Blue_ski = Resources.Load<Sprite>("blue_skill");
		Yellow_off = Resources.Load<Sprite>("yellow_offence");
		Yellow_def = Resources.Load<Sprite>("yellow_defence");
		Yellow_ski = Resources.Load<Sprite>("yellow_skill");
		Purple_off = Resources.Load<Sprite>("purple_offence");
		Purple_def = Resources.Load<Sprite>("purple_defence");
		Purple_ski = Resources.Load<Sprite>("purple_skill");
		Green_off = Resources.Load<Sprite>("green_offence");
		Green_def = Resources.Load<Sprite>("green_defence");
		Green_ski = Resources.Load<Sprite>("green_skill");
	}

	//-------------------------------------------------------
	// Public Function
	//-------------------------------------------------------
	// ピースの種類とそれに応じた色をセットする
	public void SetKind(PieceKind pieceKind)
	{
		kind = pieceKind;
		SetColor();
	}

    public PieceKind GetKind(){
        return kind;
    }

	// ピースの種類を返す
	public string GetKindColor()
	{
		GetTypeColor ();
		return color;
	}

	public string GetKindType()
	{
		GetTypeColor ();
		return type;
	}

	// ピースのサイズをセットする
	public void SetSize(int size)
	{
		this.thisRectTransform.sizeDelta = Vector2.one * size;
	}

	// ピースの透過を設定
	public void SetPieceAlpha(float alpha)
	{
		var col = thisImage.color;
		col.a = alpha;
		thisImage.color = col;
	}

	// 選択したピースの保存
	public void SavePiece(Piece piece){;
	}

	// 選択したピースの拡大
	public void ToBig(Piece piece){
	}

	//-------------------------------------------------------
	// Private Function
	//-------------------------------------------------------
	// ピースの色を自身の種類の物に変える
	private void SetColor()
	{
		switch (kind)
		{
		case PieceKind.Red_off:
			thisImage.sprite = Red_off;
			break;	
		case PieceKind.Red_def:
			thisImage.sprite = Red_def;
			break;
		case PieceKind.Red_ski:
			thisImage.sprite = Red_ski;
			break;
		case PieceKind.Blue_off:
			thisImage.sprite = Blue_off;
			break;
		case PieceKind.Blue_def:
			thisImage.sprite =Blue_def;
			break;
		case PieceKind.Blue_ski:
			thisImage.sprite =Blue_ski;
			break;
		case PieceKind.Yellow_off:
			thisImage.sprite =Yellow_off;
			break;
		case PieceKind.Yellow_def:
			thisImage.sprite =Yellow_def;
			break;
		case PieceKind.Yellow_ski:
			thisImage.sprite =Yellow_ski;
			break;
		case PieceKind.Purple_off:
			thisImage.sprite =Purple_off;
			break;
		case PieceKind.Purple_def:
			thisImage.sprite =Purple_def;
			break;
		case PieceKind.Purple_ski:
			thisImage.sprite =Purple_ski;
			break;
		case PieceKind.Green_off:
			thisImage.sprite =Green_off;
			break;
		case PieceKind.Green_def:
			thisImage.sprite =Green_def;
			break;
		case PieceKind.Green_ski:
			thisImage.sprite =Green_ski;
			break;
		
		default:
			break;
		}
	}

	private void GetTypeColor()
	{
		switch (kind)
		{
		case PieceKind.Red_off:
			color = "Red";
			type = "Offence";
			break;

		case PieceKind.Red_def:
			color = "Red";
			type = "Defence";
			break;

		case PieceKind.Red_ski:
			color = "Red";
			type = "Skill";
			break;

		case PieceKind.Blue_off:
			color = "Blue";
			type = "Offence";
			break;

		case PieceKind.Blue_def:
			color = "Blue";
			type = "Defence";
			break;

		case PieceKind.Blue_ski:
			color = "Blue";
			type = "Skill";
			break;

		case PieceKind.Yellow_off:
			color = "Yellow";
			type = "Offence";
			break;

		case PieceKind.Yellow_def:
			color = "Yellow";
			type = "Defence";
			break;

		case PieceKind.Yellow_ski:
			color = "Yellow";
			type = "Skill";
			break;

		case PieceKind.Purple_off:
			color = "Purple";
			type = "Offence";
			break;

		case PieceKind.Purple_def:
			color = "Purple";
			type = "Defence";
			break;

		case PieceKind.Purple_ski:
			color = "Purple";
			type = "Skill";
			break;

		case PieceKind.Green_off:
			color = "Green";
			type = "Offence";
			break;

		case PieceKind.Green_def:
			color = "Green";
			type = "Defence";
			break;

		case PieceKind.Green_ski:
			color = "Green";
			type = "Skill";
			break;


		default:
			break;
		}
	}
}