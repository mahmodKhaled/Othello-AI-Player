using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Disc discBlackUp;

    [SerializeField]
    private Disc discWhiteUp;

    [SerializeField]
    private GameObject highlightPrefab;

    [SerializeField]
    private UIManager uiManager;

    private Dictionary<Player, Disc> discPrefabs = new Dictionary<Player, Disc>();

    private GameState gameState = new GameState();

    private Disc[,] discs = new Disc[8, 8];

    private List<GameObject> highlights = new List<GameObject>();

    private GamePlayingAlgorithms game_algo = new GamePlayingAlgorithms();

    int black_current_score, white_current_score;

    int turnEndFlag = 0;

    // Start is called before the first frame update
    void Start()
    {
        discPrefabs[Player.Black] = discBlackUp;
        discPrefabs[Player.White] = discWhiteUp;

        AddStartDiscs();
        ShowLegalMoves();
        uiManager.SetPlayerText(gameState.CurrentPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (DifficultyMode.gameMode == GameModes.MultiPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    Vector3 impact = hitInfo.point;
                    Position boardPos = SceneToBoardPos(impact);
                    OnboardClicked(boardPos);
                }
            }
        }
        else if (DifficultyMode.gameMode == GameModes.SinglePlayerEasy)
        {
            GamePlayVsPC(1);
        }
        else if (DifficultyMode.gameMode == GameModes.SinglePlayerMedium)
        {
            GamePlayVsPC(2);
        }
        else if (DifficultyMode.gameMode == GameModes.SinglePlayerHard)
        {
            GamePlayVsPC(3);
        }
        else if (DifficultyMode.gameMode == GameModes.EasyPCEasyPC)
        {
            GamePlayPCvsPC(1, 1);
        }
        else if (DifficultyMode.gameMode == GameModes.EasyPCMediumPC)
        {
            GamePlayPCvsPC(1, 2);
        }
        else if (DifficultyMode.gameMode == GameModes.EasyPCHardPC)
        {
            GamePlayPCvsPC(1, 3);
        }
        else if (DifficultyMode.gameMode == GameModes.MediumPCEasyPC)
        {
            GamePlayPCvsPC(2, 1);
        }
        else if (DifficultyMode.gameMode == GameModes.MediumPCMediumPC)
        {
            GamePlayPCvsPC(2, 2);
        }
        else if (DifficultyMode.gameMode == GameModes.MediumPCHardPC)
        {
            GamePlayPCvsPC(2, 3);
        }
        else if (DifficultyMode.gameMode == GameModes.HardPCEasyPC)
        {
            GamePlayPCvsPC(3, 1);
        }
        else if (DifficultyMode.gameMode == GameModes.HardPCMediumPC)
        {
            GamePlayPCvsPC(3, 2);
        }
        else if (DifficultyMode.gameMode == GameModes.HardPCHardPC)
        {
            GamePlayPCvsPC(3, 3);
        }
    }


    private void ShowLegalMoves()
    {
        foreach (Position boardPos in gameState.LegalMoves.Keys)
        {
            Vector3 scenePos = BoardToScenePos(boardPos) + Vector3.up * 0.01f;
            GameObject highlight = Instantiate(highlightPrefab, scenePos, Quaternion.identity);
            highlights.Add(highlight);
        }
    }


    private void HideLegalMoves()
    {
        highlights.ForEach(Destroy);
        highlights.Clear();
    }


    private void OnboardClicked(Position boardPos)
    {
        turnEndFlag = 2;

        if (gameState.MakeMove(boardPos, out MoveInfo moveInfo))
        {
            StartCoroutine(OnMoveMade(moveInfo));
        }
    }


    private IEnumerator OnMoveMade(MoveInfo moveInfo)
    {
        HideLegalMoves();
        yield return ShowMove(moveInfo);

        yield return ShowTurnOutcome(moveInfo);
        ShowLegalMoves();

        if (gameState.CurrentPlayer == Player.White)
        {
            turnEndFlag = 1;
        }
        else if (gameState.CurrentPlayer == Player.Black)
        {
            turnEndFlag = 0;
        }
    }


    private Position SceneToBoardPos(Vector3 scenePos)
    {
        int col = (int)(scenePos.x - 0.25f);
        int row = 7 - (int)(scenePos.z - 0.25f);
        return new Position(row, col);
    }

    private Vector3 BoardToScenePos(Position boardPos)
    {
        return new Vector3(boardPos.Col + 0.75f, 0, 7 - boardPos.Row + 0.75f);
    }


    private void SpawnDisc(Disc prefab, Position boardPos)
    {
        Vector3 scenePos = BoardToScenePos(boardPos) + Vector3.up * 0.1f;
        discs[boardPos.Row, boardPos.Col] = Instantiate(prefab, scenePos, Quaternion.identity);
    }

    private void AddStartDiscs()
    {
        black_current_score = 2;
        white_current_score = 2;

        foreach (Position boardPos in gameState.OccupiedPositions())
        {
            Player player = gameState.Board[boardPos.Row, boardPos.Col];
            SpawnDisc(discPrefabs[player], boardPos);
        }
        uiManager.SetBlackScoreText(black_current_score);
        uiManager.SetWhiteScoreText(white_current_score);
    }
    

    private void FlibDiscs(List<Position> positions)
    {
        foreach (Position boardPos in positions)
        {
            discs[boardPos.Row, boardPos.Col].Flip();

            Player player = gameState.Board[boardPos.Row, boardPos.Col];

            if (player == Player.Black)
            {
                black_current_score++;
                white_current_score--;
            }
            else if (player == Player.White)
            {
                white_current_score++;
                black_current_score--;
            }
        }

        uiManager.SetBlackScoreText(black_current_score);
        uiManager.SetWhiteScoreText(white_current_score);
    }


    private IEnumerator ShowMove(MoveInfo moveInfo)
    {
        SpawnDisc(discPrefabs[moveInfo.Player], moveInfo.Position);
        yield return new WaitForSeconds(0.33f);

        if (moveInfo.Player == Player.Black)
        {
            black_current_score++;
        }
        else
        {
            white_current_score++;
        }

        FlibDiscs(moveInfo.Outflanked);
        yield return new WaitForSeconds(0.83f);
    }


    private IEnumerator ShowTurnSkipped(Player skippedPlayer)
    {
        uiManager.SetSkippedText(skippedPlayer);
        yield return uiManager.AnimateTopText();
    }


    private IEnumerator ShowGameOver(Player winner)
    {
        uiManager.SetTopText("Neither Player Can Move");
        yield return uiManager.AnimateTopText();

        yield return uiManager.HideTopText();
        yield return new WaitForSeconds(0.5f);

        uiManager.SetWinnerText(winner);
        yield return uiManager.ShowEndScreen();
    }


    private IEnumerator ShowTurnOutcome(MoveInfo moveInfo)
    {
        if(gameState.GameOver)
        {
            yield return ShowGameOver(gameState.Winner);

            yield break;
        }

        Player currentPlayer = gameState.CurrentPlayer;

        if(currentPlayer == moveInfo.Player)
        {
            yield return ShowTurnSkipped(currentPlayer.Opponent());
        }

        uiManager.SetPlayerText(currentPlayer);
    }


    private IEnumerator RestartGame()
    {
        yield return uiManager.HideEndScreen();
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }


    public void OnPlayAgainClicked()
    {
        StartCoroutine(RestartGame());
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(1);
    }


    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    
    private IEnumerator DelayInSeconds(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
    }


    void GamePlayPCvsPC(int firstPlayerDifficulty, int secondPlayerDifficulty)
    {
        MoveInfo tempMoveInfo;

        if (turnEndFlag == 0)
        {
            StartCoroutine(DelayInSeconds(3));

            List<Position> legalPositionsPC1 = new List<Position>(this.gameState.LegalMoves.Keys);

            if(legalPositionsPC1.Count == 1)
            {
                OnboardClicked(legalPositionsPC1[0]);
            }
            else
            {
                tempMoveInfo = game_algo.AlphaBetaPruning(gameState, firstPlayerDifficulty);

                OnboardClicked(tempMoveInfo.Position);
            }
            
        }
        else if (turnEndFlag == 1)
        {
            StartCoroutine(DelayInSeconds(3));

            List<Position> legalPositionsPC2 = new List<Position>(this.gameState.LegalMoves.Keys);

            if (legalPositionsPC2.Count == 1)
            {
                OnboardClicked(legalPositionsPC2[0]);
            }
            else
            {
                tempMoveInfo = game_algo.AlphaBetaPruning(gameState, secondPlayerDifficulty);

                OnboardClicked(tempMoveInfo.Position);
            }
        }
    }


    void GamePlayVsPC(int PCdifficulty)
    {
        if (Input.GetMouseButtonDown(0) && turnEndFlag == 0)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 impact = hitInfo.point;
                Position boardPos = SceneToBoardPos(impact);
                OnboardClicked(boardPos);
            }
        }
        else if (turnEndFlag == 1)
        {
            StartCoroutine(DelayInSeconds(3));

            List<Position> legalPositionsPC = new List<Position>(this.gameState.LegalMoves.Keys);

            if (legalPositionsPC.Count == 1)
            {
                OnboardClicked(legalPositionsPC[0]);
            }
            else
            {
                MoveInfo tempMoveInfo = game_algo.AlphaBetaPruning(gameState, PCdifficulty);

                OnboardClicked(tempMoveInfo.Position);
            }
  
        }
    }

}
