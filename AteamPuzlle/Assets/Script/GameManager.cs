using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// ゲーム管理クラス
public class GameManager : MonoBehaviour {

	// const.
	public const int MachingCount = 3;

	// enum.
	private enum GameState
	{
		Idle,
		PieceMove,
		MatchCheck,
		DeletePiece,
		FillPiece,
		Rotation,
		Tracing,
		TracingIdle,
		TracingMove,
        DeleteTracingPiece,
	}

	// serialize field.
	[SerializeField]
	private Board board;
	[SerializeField]
	private Text stateText;
	[SerializeField]
	private Manticore manticore;



	// private.
	private GameState currentState;
	private Piece selectedPiece;
	private Piece firstPiece;

	//-------------------------------------------------------
	// MonoBehaviour Function
	//-------------------------------------------------------
	// ゲームの初期化処理
	private void Start()
	{
		board.InitializeBoard(6, 6);

		currentState = GameState.Idle;

	}

	// ゲームのメインループ
	private void Update()
	{
		switch (currentState)
		{
		case GameState.Idle:
			Idle();
			break;
		case GameState.PieceMove:
			PieceMove();
			break;
		case GameState.MatchCheck:
			MatchCheck();
			break;
		case GameState.DeletePiece:
			DeletePiece();
			break;
		case GameState.FillPiece:
			FillPiece();
			break;
		case GameState.Rotation:
			Rotation ();
			break;
		case GameState.Tracing:
			Tracing ();
			break;
		case GameState.TracingIdle:
			TracingIdle ();
			break;
		case GameState.TracingMove:
			TracingMove ();
			break;
		case GameState.DeleteTracingPiece:
			DeleteTracingPiece ();
			break;
		default:
			break;
		}
		stateText.text = currentState.ToString();
	}

	//-------------------------------------------------------
	// Private Function
	//-------------------------------------------------------
	// プレイヤーの入力を検知し、ピースを選択状態にする
	// a → 3マッチチェック（デバッグ用）
	// t → なぞりモードい以降
	private void Idle()
	{
		if (Input.GetKeyDown(KeyCode.A)) {
			currentState = GameState.MatchCheck;
		}
		if (Input.GetKeyDown(KeyCode.T)) {
			currentState = GameState.TracingIdle;
		}
        if(Input.GetKeyDown(KeyCode.R)){
            currentState = GameState.Rotation;
        }
		if (Input.GetMouseButtonDown(0))
		{
			selectedPiece = board.GetNearestPiece(Input.mousePosition);
			currentState = GameState.PieceMove;
		}
	}

	// プレイヤーがピースを選択しているときの処理、入力終了を検知したら盤面のチェックの状態に移行する
	// 一個ずつ動かす機構にする必要ある
	private void PieceMove()
	{
		if (Input.GetMouseButton(0))
		{
			var piece = board.GetNearestPiece(Input.mousePosition);
			if (piece != selectedPiece)
			{
				board.SwitchPiece(selectedPiece, piece);
			}
		}
		else if (Input.GetMouseButtonUp(0)) {
			currentState = GameState.Idle;
		}
	}

	// 盤面上にマッチングしているピースがあるかどうかを判断する（デバッグ用）
	private void MatchCheck()
	{
		if (board.HasMatch())
		{
			currentState = GameState.DeletePiece;
		}
		else
		{
			currentState = GameState.Rotation;
		}
	}

	// マッチングしているピースを削除する
	// 倍率の計算
	// スタートとゴールがつながっているのか条件分布
	// 関数作成×2
	private void DeleteTracingPiece()
	{
		board.OnDragEnd ();
		board.GetCalculation ();
		currentState = GameState.FillPiece;
	}

    // 降ってきたピースが全く同じだった場合、３マッチパズル
    private void DeletePiece(){
        board.DeleteMatchPiece();
        currentState = GameState.FillPiece;
    }

	// 盤面上のかけている部分にピースを補充する
	// 一旦、空いているところに単純に補充
	private void FillPiece()
	{
		board.FillPiece();
		currentState = GameState.MatchCheck;
	}
		
	// 関数作成×2
	// なぞり動作はじめ
	private void Tracing(){
		if (Input.GetMouseButton (0)) {
			//firstPiece = board.GetNearestPiece(Input.mousePosition);
			board.OnDragStart(selectedPiece);
			currentState = GameState.TracingMove;
		} 
		else if (Input.GetMouseButtonUp (0)) {
			currentState = GameState.TracingIdle;
		}
	}

    // なぞったピースの色半透明に
    // ドラッグしたところで同じ色若しくは、同じタイプが一番新しいピースと被っていれば、つながる。
	private void TracingMove(){
		if (Input.GetMouseButton(0))
		{
			var piece = board.GetNearestPiece(Input.mousePosition);
			if (piece != selectedPiece)
			{
				board.OnDragging(piece);
			}
		}
		else if (Input.GetMouseButtonUp(0)) {
			currentState = GameState.TracingIdle;
		}
	}

	// なぞりモードのアイドリング
	// g → なぞったピースを除去
    // 初期化ボタン作成
	private void TracingIdle()
	{
		if (Input.GetKeyDown(KeyCode.G)) {
			currentState = GameState.DeleteTracingPiece;
		}
		if (Input.GetMouseButtonDown(0))
		{
			selectedPiece = board.GetNearestPiece(Input.mousePosition);
			currentState = GameState.Tracing;
		}
	}


	// 90°回転右回り
	private void Rotation()
	{
		board.Rotation ();
		currentState = GameState.Idle;
	}
}