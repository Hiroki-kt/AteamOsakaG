using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

// ゲーム管理クラス
public class GameManager : MonoBehaviour
{

    // const.
    public const int MachingCount = 3;

    // public
    public static float AllTime = 300;
    public static float TurnTime = 60;

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
        TimeOver,
        Wait,
    }

    // serialize field.
    [SerializeField]
    private Board board;
    [SerializeField]
    private Text stateText;
    //[SerializeField]
    //private Manticore manticore;
    [SerializeField]
    private Text _textCountdown;
    [SerializeField]
    private Image _imageMask;




    // private.
    private GameState currentState;
    private Piece selectedPiece;
    //private Piece firstPiece;
    public static float countRotaion = 1.0f;
    private int countMove;
    public static float GameTime;
    public static float turnTime;
    public Button Trac;
    public Button Pass;
    private const float SelectedPieceAlpha = 0.5f;
    private GameObject selectedPieceObject;

    //float step;
    //float speed = 1f;
    //Transform target;

    //-------------------------------------------------------
    // MonoBehaviour Function
    //-------------------------------------------------------
    // ゲームの初期化処理
    private void Start()
    {
        _textCountdown.text = "";

        GameTime = AllTime;
        turnTime = TurnTime;
        // カウントダウン表示
        StartCoroutine(CountdownCoroutine(() => StartBoard()));

    }

    // ゲームのメインループ
    private void Update()
    {
        GameTime -= Time.deltaTime;
        turnTime -= Time.deltaTime;
        if (GameTime < 0) GameTime = 0;
        if (turnTime < 0) turnTime = 0;
        //Debug.Log(GameTime);


        if (GameTime > 0)
        {
            if (turnTime > 0)
            {
                // ボード画面
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
                        //step = speed * Time.deltaTime;
                        //Debug.Log(step);
                        //target.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 90f), step);
                        Rotation();
                        break;
                    case GameState.Tracing:
                        Tracing();
                        break;
                    case GameState.TracingIdle:
                        TracingIdle();
                        break;
                    case GameState.TracingMove:
                        TracingMove();
                        break;
                    case GameState.DeleteTracingPiece:
                        DeleteTracingPiece();
                        break;
                    case GameState.Wait:
                        turnTime = TurnTime;
                        break;
                    default:
                        break;
                }
                stateText.text = currentState.ToString();
            }
            else
            {
                if(currentState == GameState.TracingMove){
                    DeleteTracingPiece();
                }
                Rotation();
                turnTime = TurnTime; //制限時間初期化
            }

        }
        else
        {
            GameOver();
        }

    }

    //-----------------------------------------
    // Public
    //-----------------------------------------

    //-------------------------------------------------------
    // Private Function
    //-------------------------------------------------------
    // プレイヤーの入力を検知し、ピースを選択状態にする
    // a → 3マッチチェック（デバッグ用）
    // t → なぞりモードい以降
    private void Idle()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentState = GameState.MatchCheck;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            currentState = GameState.TracingIdle;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentState = GameState.Rotation;
        }

        if (Input.GetMouseButtonDown(0))
        {

            selectedPiece = board.GetNearestPiece(Input.mousePosition);
            //selectedPiece.SetPieceAlpha(SelectedPieceAlpha);
            countMove = 1;
			currentState = GameState.PieceMove;

            //SelectPiece();
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
                if (countMove < 2)
                {
                    board.SwitchPiece(selectedPiece, piece);
                    countMove += 1;
                    //Debug.Log("OK");
                    //selectedPieceObject.transform.position = Input.mousePosition + Vector3.up * 10;
                }
                else
                {
                    selectedPiece.SetPieceAlpha(1f);
                    //Destroy(selectedPieceObject);
                    currentState = GameState.Idle;
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectedPiece.SetPieceAlpha(1f);
            //Destroy(selectedPieceObject);
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
            Debug.Log("rot");
            currentState = GameState.Rotation;
        }
    }

    // マッチングしているピースを削除する
    // 倍率の計算
    // スタートとゴールがつながっているのか条件分布
    // 関数作成×2
    private void DeleteTracingPiece()
    {
        currentState = GameState.Wait;
        if (board.GetStartGoal() == "True")
        {
            board.GetCalculation();
            StartCoroutine(board.OnDragEnd(() => currentState = GameState.FillPiece));
            //board.OnDragEnd();
            //currentState = GameState.FillPiece;
        }
        else if (board.GetStartGoal() == "False")
        {
            board.OnDragEndNG();
            currentState = GameState.TracingIdle;
        }
    }

    // 降ってきたピースが全く同じだった場合、３マッチパズル
    private void DeletePiece()
    {
        currentState = GameState.Wait;
        //board.DeleteMatchPiece();
        StartCoroutine(board.DeleteMatchPiece(() => currentState = GameState.FillPiece));
        //currentState = GameState.FillPiece;
    }

    // 盤面上のかけている部分にピースを補充する
    // 一旦、空いているところに単純に補充
    private void FillPiece()
    {
        currentState = GameState.Wait;
        //board.FillPiece();
        //Debug.Log("fill");
        StartCoroutine(board.FillPiece(() => currentState = GameState.MatchCheck));
        //currentState = GameState.MatchCheck;
    }

    // 関数作成×2
    // なぞり動作はじめ
    private void Tracing()
    {
        if (Input.GetMouseButton(0))
        {
            //firstPiece = board.GetNearestPiece(Input.mousePosition);
            board.OnDragStart(selectedPiece);
            currentState = GameState.TracingMove;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentState = GameState.TracingIdle;
        }
    }

    // なぞったピースの色半透明に
    // ドラッグしたところで同じ色若しくは、同じタイプが一番新しいピースと被っていれば、つながる。
    private void TracingMove()
    {
        if (Input.GetMouseButton(0))
        {
            var piece = board.GetNearestPiece(Input.mousePosition);
            if (piece != selectedPiece)
            {
                board.OnDragging(piece);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {

            currentState = GameState.DeleteTracingPiece;
        }
    }

    // なぞりモードのアイドリング
    // g → なぞったピースを除去
    // 初期化ボタン作成
    private void TracingIdle()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            currentState = GameState.DeleteTracingPiece;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            currentState = GameState.Idle;
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
        //Debug.Log("TurnCount");
        Debug.Log(countRotaion);
        var No1 = GameObject.Find("No1L").transform;
        var No2 = GameObject.Find("No2L").transform;
        var No3 = GameObject.Find("No3L").transform;
        var No4 = GameObject.Find("No4L").transform;

        var No1pos = No1.position;
        var No2pos = No2.position;
        var NO3pos = No3.position;
        var NO4pos = No4.position;

        No1.position = No2pos;
        No2.position = NO4pos;
        No3.position = No1pos;
        No4.position = NO3pos;

        currentState = GameState.Wait;
        StartCoroutine(board.Rotation(countRotaion * 90.0f,()=> Afterrot()));

    }

    private void Afterrot(){

        countRotaion += 1.0f;
        turnTime = TurnTime; // 制限時間初期化
        currentState = GameState.Idle;
    }

    public void TracClick()
    {
        if(currentState == GameState.PieceMove || currentState == GameState.Idle){
            Debug.Log("trac");
            currentState = GameState.TracingIdle;
        }else{
            Debug.Log("exchange");
            currentState = GameState.Idle;
        }
    }

    public void PassClick()
    {
        Debug.Log("pass");
        currentState = GameState.Rotation;
    }

    /*
    // ピースを選択する処理
    private void SelectPiece()
    {
        selectedPiece = board.GetNearestPiece(Input.mousePosition);
        var piece = board.InstantiatePiece(Input.mousePosition);
        piece.SetKind(selectedPiece.GetKind());
        piece.SetSize((int)(board.pieceWidth * 1.2f));
        piece.SetPieceAlpha(SelectedPieceAlpha);
        selectedPieceObject = piece.gameObject;
        countMove = 1;

        selectedPiece.SetPieceAlpha(SelectedPieceAlpha);
        currentState = GameState.PieceMove;


    }
    */

    private IEnumerator CountdownCoroutine(Action endCallBack)
    {
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "3";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "2";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "1";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "GO!";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "";
        _textCountdown.gameObject.SetActive(false);
        _imageMask.gameObject.SetActive(false);

        endCallBack();
    }

    private void StartBoard(){
        board.InitializeBoard(6, 6);

        currentState = GameState.Idle;

        Trac = GetComponent<Button>();
        //Trac.onClick.AddListener(TracClick());

        GameTime = AllTime;
        turnTime = TurnTime;

        SceneManager.LoadScene("baattleanim", LoadSceneMode.Additive);
        //target = GameObject.Find("Board").transform;

    }

    private void GameOver()
    {
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "GameOver";

        if(Input.GetMouseButtonDown(0)){
            _textCountdown.gameObject.SetActive(false);
            _imageMask.gameObject.SetActive(false);
            SceneManager.LoadScene("ResultSceneBud");
        }

    }

    private void GameClear()
    {
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "GameClear";

        if(Input.GetMouseButtonDown(0)){
            _textCountdown.gameObject.SetActive(false);
            _imageMask.gameObject.SetActive(false);
            SceneManager.LoadScene("ResultSceneGood");

        }
    }

    public float GetRollCount(){
        return countRotaion;
    }
}