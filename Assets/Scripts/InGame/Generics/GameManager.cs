using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject player1Paddle;
    public GameObject player1Goal;

    [Header("Player 2")]
    public GameObject player2Paddle;
    public GameObject player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;
    public GameObject[] Player1ScoreBoard;
    public GameObject[] Player2ScoreBoard;
    public GameObject CountDown;

    [Header("AudioFiles")]
    public AudioClip initialCountdowns;
    public AudioClip finalCountdown;
    public AudioClip goalScored;

    [Header("EndGameAction")]

    public UnityEvent EndGameGeneral;
    public UnityEvent EndGamePlayer1Win;
    public UnityEvent EndGamePlayer2Win;
    public UnityEvent EndGamePlayerBotWin;
    public UnityEvent EndGameCustomOptions;
    public UnityEvent EndGameCampaignOptions;
    public UnityEvent EndGameEndlessOptions;

    public static bool countDone = false;

    private AudioSource source;
    private int Player1Score = 0;
    private int Player2Score = 0;
    private int countDownTime = 0;
    private int countDownSeconds = 50;

    void Start()
    {
        source = GetComponent<AudioSource>();

    }



    void FixedUpdate()
    {
        string text = CountDown.GetComponent<TextMeshProUGUI>().text;
        int num = -1;
        if (text != "")
        {
            num = Convert.ToInt32(text);
        }
        if (!countDone && num > 0 && countDownTime > countDownSeconds)
        {
            num--;

            if (num == 0)
            {
                countDone = true;
                CountDown.GetComponent<TextMeshProUGUI>().text = "";
                source.clip = finalCountdown;
                source.Play();
                this.gameObject.GetComponent<PauseAndBackFunction>().enableNecessaryObjects();
            }
            else
            {
                CountDown.GetComponent<TextMeshProUGUI>().text = num.ToString();
                source.clip = initialCountdowns;
                source.Play();
            }
            countDownTime = 0;
        }
        countDownTime++;
    }

    public static void restartCountDown()
    {

    }

    public void PlayerScored(int player)
    {
        if (player == 1)
        {
            Player1Score++;
            Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
            UpdatePlayer1ScoreBoard();
            this.gameObject.GetComponent<GameManagerParticles>().playParticles(1);
        }
        else
        {
            Player2Score++;
            Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
            UpdatePlayer2ScoreBoard();
            this.gameObject.GetComponent<GameManagerParticles>().playParticles(2);
        }

        


        source.clip = goalScored;
        source.Play();

        checkEndGameConditions();

        ResetPosition();
    }

    private void checkEndGameConditions(){
        bool player1Win = Player1Score >= 7;
        bool player2Win = Player2Score >= 7;
        
        if(player1Win || player2Win){
            bool isCampaignGame = PlayerPrefs.GetInt("isCampaignGame", 0) == 1;
            bool isEndlessCampaignGame = PlayerPrefs.GetInt("isEndlessCampaignGame", 0) == 1;

            if(player1Win){
                if(isCampaignGame || isEndlessCampaignGame){
                    if(isCampaignGame){
                        //Campaign Win
                        
                        MapNodeManager.saveLevelGame(true);
                        
                        EndGamePlayer1Win.Invoke();
                        EndGameCampaignOptions.Invoke();
                    }else{
                        //Endless Campaign Win

                        //Add in saving functionality for endless campaign

                        EndGamePlayer1Win.Invoke();
                        EndGameEndlessOptions.Invoke();
                    }
                }else{
                    //Custom Game bottom player win
                    
                    EndGamePlayer1Win.Invoke();
                    EndGameCustomOptions.Invoke();
                }
                print("you win");
            }else if(player2Win){
                if(StaticGameInfo.getPlayerBot()){
                    print("bot won");
                    if(isCampaignGame){
                        //Campaign Loss

                        EndGamePlayerBotWin.Invoke();
                        EndGameCampaignOptions.Invoke();
                    }else if(isEndlessCampaignGame){
                        //Endless Campaign Loss

                        EndGamePlayerBotWin.Invoke();
                        EndGameEndlessOptions.Invoke();
                    }else{
                        //Custom Game with bot loss

                        EndGamePlayerBotWin.Invoke();
                        EndGameCustomOptions.Invoke();
                    }
                }else{
                    print("player2");
                    //Custom Game top player win

                    EndGamePlayer2Win.Invoke();
                    EndGameCustomOptions.Invoke();
                }
            }


            EndGameGeneral.Invoke();
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        Player1Score = 0;
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();

        Player2Score = 0;
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();

        ClearScoreBoards();

        ResetPosition();
    }

    public void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        player1Paddle.GetComponent<Paddle>().Reset();
        player2Paddle.GetComponent<Paddle>().Reset();
        ResetCounter();
    }

    public void ResetCounter()
    {
        countDownTime = 0;
        countDone = false;
        CountDown.GetComponent<TextMeshProUGUI>().text = "3";
    }

    public int getPlayerScore(int player)
    {
        if (player == 1)
        {
            return Player1Score;
        }
        else
        {
            return Player2Score;
        }
    }

    public void UpdatePlayer1ScoreBoard(){
        for(int i = 0; i < Player1Score; i++){
            Player1ScoreBoard[i].SetActive(true);
        }
    }

    public void UpdatePlayer2ScoreBoard(){
        for(int i = 0; i < Player2Score; i++){
            Player2ScoreBoard[i].SetActive(true);
        }
    }

    public void ClearScoreBoards(){
        for(int i = 0 ; i < Player1ScoreBoard.Length; i++){
            Player1ScoreBoard[i].SetActive(false);
            Player2ScoreBoard[i].SetActive(false);
        }
    }


}
