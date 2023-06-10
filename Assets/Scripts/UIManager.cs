using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI topText;

    [SerializeField]
    private TextMeshProUGUI blackScoreText;

    [SerializeField]
    private TextMeshProUGUI whiteScoreText;

    [SerializeField]
    private TextMeshProUGUI winnerText;

    [SerializeField]
    private Image blackOverlay;

    [SerializeField]
    private RectTransform playAgainButton;

    [SerializeField]
    private RectTransform restartButton;

    [SerializeField]
    private RectTransform quitInGameButton;

    [SerializeField]
    private RectTransform quitAfterGameButton;

    public void SetPlayerText(Player currentPlayer)
    {
        if (currentPlayer == Player.Black)
        {
            topText.text = "Black's Turn <sprite name=DiscBlackUp>";
        }
        else if (currentPlayer == Player.White)
        {
            topText.text = "White's Turn <sprite name=DiscWhiteUp>";
        }
    }


    public void SetSkippedText(Player skippedPlayer)
    {
        if (skippedPlayer == Player.Black)
        {
            topText.text = "Black can't Move :( <sprite name=DiscBlackUp>";
        }
        else if (skippedPlayer == Player.White)
        {
            topText.text = "White can't Move :( <sprite name=DiscWhiteUp>";
        }
    }


    public void SetTopText(string message)
    {
        topText.text = message;
    }


    public IEnumerator AnimateTopText()
    {
        topText.transform.LeanScale(Vector3.one * 1.2f, 0.25f).setLoopPingPong(4);
        yield return new WaitForSeconds(2);
    }


    private IEnumerator ScaleDown(RectTransform rect)
    {
        rect.LeanScale(Vector3.zero, 0.2f);
        yield return new WaitForSeconds(0.2f);
        rect.gameObject.SetActive(false);
    }

    private IEnumerator ScaleUp(RectTransform rect)
    {
        rect.gameObject.SetActive(true);
        rect.localScale = Vector3.zero;
        rect.LeanScale(Vector3.one, 0.2f);
        yield return new WaitForSeconds(0.2f);
    }


    public IEnumerator HideTopText()
    {
        yield return ScaleDown(topText.rectTransform);
    }


    public void SetBlackScoreText(int score)
    {
        StartCoroutine(ScaleUp(blackScoreText.rectTransform));
        blackScoreText.text = $"<sprite name=DiscBlackUp> {score}";
    }

    public void SetWhiteScoreText(int score)
    {
        StartCoroutine(ScaleUp(whiteScoreText.rectTransform));
        whiteScoreText.text = $"<sprite name=DiscWhiteUp> {score}";
    }

    private IEnumerator ShowOverlay()
    {
        blackOverlay.gameObject.SetActive(true);
        blackOverlay.color = Color.clear;
        blackOverlay.rectTransform.LeanAlpha(0.8f, 1);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HideOverlay()
    {
        blackOverlay.rectTransform.LeanAlpha(0, 1);
        yield return new WaitForSeconds(0.5f);
        blackOverlay.gameObject.SetActive(false);
    }


    private IEnumerator MoveScore()
    {
        blackScoreText.rectTransform.LeanMoveX(-150f, 0.5f);
        whiteScoreText.rectTransform.LeanMoveX(150f, 0.5f);

        yield return new WaitForSeconds(0.5f);
    }


    public void SetWinnerText(Player winner)
    {
        switch (winner)
        {
            case Player.Black:
                winnerText.text = "Black Won :)";
                break;

            case Player.White:
                winnerText.text = "White Won :)";
                break;

            case Player.None:
                winnerText.text = "It's a Tie :(";
                break;

        }
    }


    public IEnumerator ShowEndScreen()
    {
        yield return ShowOverlay();
        yield return MoveScore();

        yield return ScaleDown(restartButton);
        yield return ScaleDown(quitInGameButton);

        yield return ScaleUp(winnerText.rectTransform);
        yield return ScaleUp(playAgainButton);
        yield return ScaleUp(quitAfterGameButton);
    }

    
    public IEnumerator HideEndScreen()
    {
        StartCoroutine(ScaleDown(winnerText.rectTransform));
        StartCoroutine(ScaleDown(blackScoreText.rectTransform));
        StartCoroutine(ScaleDown(whiteScoreText.rectTransform));
        StartCoroutine(ScaleDown(playAgainButton));
        StartCoroutine(ScaleDown(quitAfterGameButton));

        StartCoroutine(ScaleUp(restartButton));
        StartCoroutine(ScaleUp(quitInGameButton));

        yield return new WaitForSeconds(0.5f);
        yield return HideOverlay();
    }

}
