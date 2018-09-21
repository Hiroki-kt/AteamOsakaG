using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// 盤面クラス
public class Board : MonoBehaviour {

	// serialize field.
	[SerializeField]
	private GameObject piecePrefab;
	[SerializeField]
	private GameObject Red_of;

	// private.
	private Piece[,] board;
    private Piece[,] rotboard;
	private int width;
	private int height;
	private int pieceWidth;
	private int randomSeed;
	private List<Piece> removableBallList;
    private List<string> ColorList;
    private List<string> ColorTypeList;
    private List<int> ColorIdList;
    private List<string> TypeList;
	private Piece LastPiece;
	private string currentType;
	private string currentColor;
	private Piece firstPiece;
    private string TF;

	//-------------------------------------------------------
	// Public Function
	//-------------------------------------------------------
	// 特定の幅と高さに盤面を初期化する
	public void InitializeBoard(int boardWidth, int boardHeight)
	{
		width = boardWidth ;
		height = boardHeight;

		pieceWidth = Screen.width / (boardWidth + 2);

		board = new Piece[width, height];

		for (int i = 0; i < boardWidth; i++)
		{
			for (int j = 0; j < boardHeight; j++)
			{
				CreatePiece(new Vector2(i, j));
			}
		}
	}

	// 入力されたクリック(タップ)位置から最も近いピースの位置を返す
	public Piece GetNearestPiece(Vector3 input)
	{
		var minDist = float.MaxValue;
		Piece nearestPiece = null;

		// 入力値と盤面のピース位置との距離を計算し、一番距離が短いピースを探す
		foreach (var p in board)
		{
			var dist = Vector3.Distance(input, p.transform.position);
			if (dist < minDist)
			{
				minDist = dist;
				nearestPiece = p;
			}
		}

		return nearestPiece;
	}

	// 盤面上のピースを交換する
	public void SwitchPiece(Piece p1, Piece p2)
	{
		// 位置を移動する
		var p1Position = p1.transform.position;
		p1.transform.position = p2.transform.position;
		p2.transform.position = p1Position;

		// 盤面データを更新する
		var p1BoardPos = GetPieceBoardPos(p1);
		var p2BoardPos = GetPieceBoardPos(p2);
		board[(int)p1BoardPos.x, (int)p1BoardPos.y] = p2;
		board[(int)p2BoardPos.x, (int)p2BoardPos.y] = p1;
	}

	// 盤面上にマッチングしているピースがあるかどうかを判断する
	public bool HasMatch()
	{
		foreach (var piece in board)
		{
			if (IsMatchPiece(piece))
			{
				return true;
			}
		}
		return false;
	}

	// マッチングしているピースを削除する
	public void DeleteMatchPiece()
	{
		// マッチしているピースの削除フラグを立てる
		foreach (var piece in board)
		{
			piece.deleteFlag = IsMatchPiece(piece);
		}

		// 削除フラグが立っているオブジェクトを削除する
		foreach (var piece in board)
		{
			if (piece != null && piece.deleteFlag)
			{
				Destroy(piece.gameObject);
			}
		}
	}

	// ピースが消えている場所を詰めて、新しいピースを生成する
	public void FillPiece()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				FillPiece(new Vector2(i, j));
			}
		}
	}

	// 回転
    // 初期座標の調整が本来は必要
    public void Rotation(float rot){
        
        // 回転します。ボードごと回転。
        float speed = 1f;
        float step = speed * Time.deltaTime;
        var target = GameObject.Find("Board").transform;

        while (transform.rotation != Quaternion.Euler(0,0,rot)){
            // 指定した方向にゆっくり回転する場合
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rot), step);
              
        }

        // 回転後の座標の変換
        rotboard = new Piece[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                RotationPiece(new Vector2 (i,j));

            }
        }
        board = rotboard;
        //Debug.Log(board[0, 0].transform.position);
	}


    //　一旦赤色をキャラの属性と考える
    // キャラの属性導入後、キャラの属性取得実装
    public void GetCalculation()
    {
        ColorTypeList = new List<string>();
        ColorIdList = new List<int>();
        // 一番目の同属性のピースの番号
        int ColorId = ColorList.IndexOf("Red");
        if (ColorId != -1){
            ColorIdList.Add(ColorId);
    
        }

        // キャラと同属性のピース探し
        while (ColorId != -1)
        {
            ColorId = ColorList.IndexOf("Red", ColorId + 1);
            ColorIdList.Add(ColorId);

        }

        if (ColorIdList.Count > 0)
        {
            // キャラと同属性のピースの番号と同じ番号のタイプを取得
            for (int i = 0; i > ColorIdList.Count(); i++)
            {
                ColorTypeList.Add(TypeList[ColorIdList[i]]);
            }
            //GetCalulationOff();
            Debug.Log("Offence");
            Debug.Log(GetCalulationOff());
            //GetCalulationDef();
            Debug.Log("Defence");
            Debug.Log(GetCalulationDef());
            //GetCalulationSki();
            Debug.Log("Skill");
            Debug.Log(GetCalulationSki());
        }else{
            //GetCalulationOffNotColor();
            Debug.Log("Offence");
            Debug.Log(GetCalulationOffNotColor());
            //GetCalulationDefNotColor();
            Debug.Log("Defence");
            Debug.Log(GetCalulationDefNotColor());
            //GetCalulationSkiNotColor();
            Debug.Log("Skill");
            Debug.Log(GetCalulationSkiNotColor());
        }



    }

		
	//---------------------------------------------------------
	// Publi  なぞり動作
	//---------------------------------------------------------

	public void OnDragStart(Piece piece) {
		//var col = piece;
		if (piece != null) {
			//var colObj = (GameObject)col;
			removableBallList = new List<Piece>();//初期化
            ColorList = new List<string>();
            TypeList = new List<string>();
			firstPiece = piece;
			currentType = piece.GetKindType();
			currentColor = piece.GetKindColor();
			PushToList(piece, currentColor, currentType);
		}
	}

	public void OnDragging(Piece piece) {
		if (piece != null) {
			//なにかをドラッグしているとき
			//var colObj = col.gameObject;
			var piececolor = piece.GetKindColor ();
			var piecetype = piece.GetKindType ();
			if (piececolor == currentColor || piecetype == currentType) {
				//現在リストに追加している色と同じ色のボールのとき
				if (LastPiece != piece) {
					//直前にリストにいれたのと異なるボールのとき
					var dist = Vector3.Distance (LastPiece.transform.position, piece.transform.position); //直前のボールと現在のボールの距離を計算
                    if (dist <= 30) {
						//ボール間の距離が一定値以下のとき
						PushToList (piece, piececolor, piecetype); //消去するリストにボールを追加
                        currentType = piecetype;
                        currentColor = piececolor;
					}
				}
			}
		}
	}

    // スタートとゴールの値があったら、消すそれ以外は戻す
	public void OnDragEnd() {
		if (firstPiece != null) {
			//1つ以上のボールをなぞっているとき
			var length = removableBallList.Count;
			for (var i = 0; i < length; i++) {
				Destroy(removableBallList[i].gameObject); //リストにあるボールを消去
			}
			
			firstPiece = null; //変数の初期化
		}
	}

    public void OnDragEndNG()
    {
        for (var j = 0; j < removableBallList.Count;j++){
            var listedBall = removableBallList[j];
            listedBall.SetPieceAlpha(1.0f);
        }

    }

    // スタートとゴールのピースが含まれているか？
    public string GetStartGoal(){
        TF = null;
        //Debug.Log(removableBallList[0].transform.position);
        //Debug.Log(board[5, 5].transform.position);
        //Debug.Log(board[0, 0].transform.position);
        //Debug.Log(removableBallList[removableBallList.Count -1].transform.position);
        if(removableBallList[removableBallList.Count - 1].transform.position == board[0,0].transform.position && removableBallList[0].transform.position == board[5,5].transform.position){
            TF = "True";
        }
        else{
            TF = "False";
        }
        return TF;
    }
    //-------------------------------------------------------
    // Private Function
    //-------------------------------------------------------
    // 特定の位置にピースを作成する
    private void CreatePiece(Vector2 position)
	{
		// ピースの生成位置を求める
		var createPos = GetPieceWorldPos(position);
		// 45°回転
		Quaternion rot = Quaternion.AngleAxis(45.0f,Vector3.forward);
		createPos = rot * createPos + (float)Screen.width/2 * new Vector3(1,0,0);
		// 生成するピースの種類をランダムに決める
		var kind = (PieceKind)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PieceKind)).Length);

		// ピースを生成、ボードの子オブジェクトにする
		var piece = Instantiate(piecePrefab, createPos, Quaternion.identity).GetComponent<Piece>();
		piece.transform.SetParent(transform);
		piece.SetSize(pieceWidth);
		piece.SetKind(kind);

		// 盤面にピースの情報をセットする
		board[(int)position.x, (int)position.y] = piece;
        //Debug.Log(piece.transform.position);
	
	}

	// 盤面上の位置からピースオブジェクトのワールド座標での位置を返す
	private Vector3 GetPieceWorldPos(Vector2 boardPos)
	{
        return new Vector3(boardPos.x * pieceWidth + (pieceWidth / 2), boardPos.y * pieceWidth + (pieceWidth / 2), 0);

	}

    private Vector3 GetPieceRealWorldPos(Vector2 boardPos)
    {
        var worldpos = new Vector3(boardPos.x * pieceWidth + (pieceWidth / 2), boardPos.y * pieceWidth + (pieceWidth / 2), 0);
        Quaternion rot = Quaternion.AngleAxis(45.0f, Vector3.forward);
        return rot * worldpos + (float)Screen.width / 2 * new Vector3(1, 0, 0);

    }


	// ピースが盤面上のどの位置にあるのかを返す
	private Vector2 GetPieceBoardPos(Piece piece)
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (board[i, j] == piece)
				{
					return new Vector2(i, j);
				}
			}
		}

		return Vector2.zero;
	}

    // 対象のピースがマッチしているかの判定を行う
    private bool IsMatchPiece(Piece piece)
    {
        // ピースの情報を取得
        var pos = GetPieceBoardPos(piece);
        //var color = piece.GetKindColor();
        //var type = piece.GetKindType ();
        var kind = piece.GetKind();

        // 縦方向にマッチするかの判定 MEMO: 自分自身をカウントするため +1 する
        var verticalMatchCount = GetSameKindPieceNum(kind, pos, Vector2.up) + GetSameKindPieceNum(kind, pos, Vector2.down) + 1;
        //var verticalMatchCountType = GetSameKindPieceNumType(type, pos, Vector2.up) + GetSameKindPieceNumType(type, pos, Vector2.down) + 1;

        // 横方向にマッチするかの判定 MEMO: 自分自身をカウントするため +1 する
        var horizontalMatchCount = GetSameKindPieceNum(kind, pos, Vector2.right) + GetSameKindPieceNum(kind, pos, Vector2.left) + 1;
        //var horizontalMatchCountType = GetSameKindPieceNumType(type, pos, Vector2.right) + GetSameKindPieceNumType(type, pos, Vector2.left) + 1;

        return verticalMatchCount >= GameManager.MachingCount || horizontalMatchCount >= GameManager.MachingCount;
    }

	// 対象の方向に引数で指定したの種類のピースがいくつあるかを返す
	// ピース色の確認
	private int GetSameKindPieceNum(PieceKind kind, Vector2 piecePos, Vector2 searchDir)
	{
		var count = 0;
		while (true)
		{
			piecePos += searchDir;
			if (IsInBoard(piecePos) && board[(int)piecePos.x, (int)piecePos.y].GetKind() == kind)
			{
				count++;
			}
			else
			{
				break;
			}
		}
		return count;
	}



	// 対象の座標がボードに存在するか(ボードからはみ出していないか)を判定する
	private bool IsInBoard(Vector2 pos)
	{
		return pos.x >= 0 && pos.y >= 0 && pos.x < width && pos.y < height;
	}

	// 特定のピースのが削除されているかを判断し、削除されているなら詰めるか、それができなければ新しく生成する
	private void FillPiece(Vector2 pos)
	{
		var piece = board[(int)pos.x, (int)pos.y];
		if (piece != null && !piece.deleteFlag)
		{
			// ピースが削除されていなければ何もしない
			return;
		}

		/*
		// 実際はこの部分もしっかり考える必要有り。一旦パス
		// 対象のピースより上方向に有効なピースがあるかを確認、あるなら場所を移動させる
		var checkPos = pos + Vector2.up;
		while (IsInBoard(checkPos))
		{
			var checkPiece = board[(int)checkPos.x, (int)checkPos.y];
			if (checkPiece != null && !checkPiece.deleteFlag)
			{
				checkPiece.transform.position = GetPieceWorldPos(pos);
				board[(int)pos.x, (int)pos.y] = checkPiece;
				board[(int)checkPos.x, (int)checkPos.y] = null;
				return;
			}
			checkPos += Vector2.up;
		}
		*/

		// 有効なピースがなければ新しく作る
		CreatePiece(pos);
	}
		

	private void PushToList(Piece obj, string color, string type) {
		LastPiece = obj;
		// 色半透明に変更
		obj.SetPieceAlpha (0.5f);
		removableBallList.Add(obj);
        ColorList.Add(color);
        TypeList.Add(type);
	}

	// 回転後の座標更新
    private void RotationPiece(Vector2 Pos){
        
        var worldPos = GetPieceRealWorldPos(Pos);
        // 90°回転

		Quaternion rot = Quaternion.AngleAxis(90.0f, Vector3.forward);
        Quaternion rotback = Quaternion.AngleAxis(-45.0f, Vector3.forward);
        // ピースの生成位置を求める

        var createPos = rot * worldPos + (float)Screen.width / 2 * new Vector3(1, 0, 0);

        createPos = rotback * createPos ;

        var boardPos = new Vector2((createPos.x - (pieceWidth / 2)) / pieceWidth + 0.35f , (createPos.y - (pieceWidth / 2)) / pieceWidth + 0.35f);

        if (boardPos.x < 0){
            boardPos.x = 0.0f;
        }
       
        //Debug.Log((int)Pos.x);
        //Debug.Log((int)Pos.y);

        //Debug.Log(board[(int)Pos.x,(int)Pos.y].transform.position);
        //Debug.Log(board[(int)boardPos.x, (int)boardPos.y].transform.position);
        rotboard[(int)boardPos.x, (int)boardPos.y] = board[(int)Pos.x,(int)Pos.y];
        //Debug.Log(rotboard[(int)boardPos.x, (int)boardPos.y].transform.position);
	}

    // キャラと同属性は個数×３、その他は個数×１　それぞれのタイプのステータス上昇
    private int GetCalulationOff()
    {
        return TypeList.Count(x => x == "Offence") + 2 * ColorTypeList.Count(x => x == "Offence");
    }

    private int GetCalulationDef()
    {
        return TypeList.Count(x => x == "Defence") + 2 * ColorTypeList.Count(x => x == "Defence");
    }

    private int GetCalulationSki()
    {
        return TypeList.Count(x => x == "Skill") + 2 * ColorTypeList.Count(x => x == "Skill");
    }

    private int GetCalulationOffNotColor()
    {
        return TypeList.Count(x => x == "Offence");
    }

    private int GetCalulationDefNotColor()
    {
        return TypeList.Count(x => x == "Defence");
    }

    private int GetCalulationSkiNotColor()
    {
        return TypeList.Count(x => x == "Skill");
    }


}