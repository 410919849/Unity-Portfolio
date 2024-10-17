using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Thread receiveThread;
    private UdpClient client;
    private int port;
    private bool endGame = false;
    private bool reconnecting = false;
    private float reconnectInterval = 3f; 

    private string receivedText = "none";



    public Text countDownTimer;


    private int round = 1;
    private int winCount = 0;
    private int loseCount = 0;
    private float roundTimer = 4.99f;

    private int playerChoice = 0;

    private float idleTimer = 0;

    public Color trans;
    public Color original;

    public GameObject rock_ready;
    public GameObject paper_ready;
    public GameObject scissors_ready;

    public GameObject playerReady;
    public GameObject playerSelect;
    public GameObject aiSelect;

    public GameObject player_rock_select;
    public GameObject player_paper_select;
    public GameObject player_scissors_select;

    public GameObject ai_rock_select;
    public GameObject ai_paper_select;
    public GameObject ai_scissors_select;

    public GameObject winIcon;
    public GameObject loseIcon;
    public GameObject drawIcon;

    public Sprite winProgress;
    public Sprite loseProgress;

    public Image progress1;
    public Image progress2;
    public Image progress3;

    public GameObject winText;
    public GameObject loseText;
    public GameObject restartBtn;

    private float handSignTime;
    private float idleTimer2;
    public Text systemText;

    private int resultVal;

    public List<GameObject> signSet;
    private string[] signName = new string[10];
    private string[] signChineseName = new string[10];
    public Text toDoSignStr;
    private int targetSign;




    // Start is called before the first frame update
    void Start()
    {
        signName[0] = "girl";
        signName[1] = "boy";
        signName[2] = "brother";
        signName[3] = "dragon";
        signName[4] = "gun";
        signName[5] = "lighter";
        signName[6] = "ok";
        signName[7] = "paper";
        signName[8] = "plane";
        signName[9] = "sister";

        signChineseName[0] = "女";
        signChineseName[1] = "男";
        signChineseName[2] = "兄";
        signChineseName[3] = "龍";
        signChineseName[4] = "你好";
        signChineseName[5] = "謝謝";
        signChineseName[6] = "錢";
        signChineseName[7] = "不客氣";
        signChineseName[8] = "飛機";
        signChineseName[9] = "姊";


        port = 7777;
        InitUDP();
    }

    private void InitUDP()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (endGame == false)
        {
            try
            {

                if (client == null)
                    client = new UdpClient(port);

                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
                byte[] data = client.Receive(ref anyIP);

                receivedText = Encoding.UTF8.GetString(data);

            }
            catch (SocketException se)
            {
                Debug.Log("SocketException: " + se.Message);
                if (!reconnecting)
                {
                    reconnecting = true;
                    StartCoroutine(Reconnect());
                }
            }
            catch (Exception e)
            {
                Debug.Log("Exception: " + e.Message);
            }
        }
        client.Close();
        client.Dispose();
    }

    private IEnumerator Reconnect()
    {
        while (reconnecting)
        {
            Debug.Log("Attempting to reconnect...");
            yield return new WaitForSeconds(reconnectInterval);

            try
            {
                if (client != null)
                {
                    client.Close();
                    client.Dispose();
                }

                client = new UdpClient(port);
                reconnecting = false;
            }
            catch (Exception e)
            {
                Debug.Log("Reconnect failed: " + e.Message);
            }
        }
    }

    private void endTheGame(bool win)
    {
        endGame = true;
        playerReady.SetActive(false);
        playerSelect.SetActive(false);
        aiSelect.SetActive(false);
        winIcon.SetActive(false);
        drawIcon.SetActive(false);
        loseIcon.SetActive(false);
        systemText.text = "";
        for (int i = 0; i < 10; i++)
        {
            signSet[i].SetActive(false);
        }

        restartBtn.SetActive(true);
        if(win)
        {
            winText.SetActive(true);
        }
        else
        {
            loseText.SetActive(true);
        }
    }

    private void result(int num)
    {
        if(num == 0)
        {
        }
        else if(num == 1)
        {
            if(round == 1)
            {
                progress1.sprite = winProgress;
                round++;
            }
            else if (round == 2)
            {
                progress2.sprite = winProgress;
                round++;
            }
            else if (round == 3)
            {
                progress3.sprite = winProgress;
                round++;
            }
            winCount++;
            if(winCount == 2)
            {
                endTheGame(true);
            }
        }
        else if(num == -1)
        {
            if (round == 1)
            {
                progress1.sprite = loseProgress;
                round++;
            }
            else if (round == 2)
            {
                progress2.sprite = loseProgress;
                round++;
            }
            else if (round == 3)
            {
                progress3.sprite = loseProgress;
                round++;
            }
            loseCount++;
            if (loseCount == 2)
            {
                endTheGame(false);
            }
        }
    }

    private void battle()
    {
        idleTimer = 3.0f;

        int aiChoice = UnityEngine.Random.Range(0, 3);

        playerReady.SetActive(false);
        playerSelect.SetActive(true);
        aiSelect.SetActive(true);

        ai_rock_select.SetActive(false);
        ai_paper_select.SetActive(false);
        ai_scissors_select.SetActive(false);

        player_rock_select.SetActive(false);
        player_paper_select.SetActive(false);
        player_scissors_select.SetActive(false);

        if(playerChoice == 0)
        {
            playerChoice = UnityEngine.Random.Range(1, 4);
        }

        if (aiChoice == 0) //rock
        {
            ai_rock_select.SetActive(true);
            if(playerChoice == 1) //rock
            {
                player_rock_select.SetActive(true);
                resultVal = 0;
                drawIcon.SetActive(true);
            }
            else if (playerChoice == 2) //paper
            {
                player_paper_select.SetActive(true);
                resultVal = 1;
                winIcon.SetActive(true);
            }
            else if (playerChoice == 3) //scissors
            {
                player_scissors_select.SetActive(true);
                resultVal = -1;
                loseIcon.SetActive(true);
            }
        }
        else if (aiChoice == 1) //paper
        {
            ai_paper_select.SetActive(true);
            if (playerChoice == 1) //rock
            {
                player_rock_select.SetActive(true);
                resultVal = -1;
                loseIcon.SetActive(true);
            }
            else if (playerChoice == 2) //paper
            {
                player_paper_select.SetActive(true);
                resultVal = 0;
                drawIcon.SetActive(true);
            }
            else if (playerChoice == 3) //scissors
            {
                player_scissors_select.SetActive(true);
                resultVal = 1;
                winIcon.SetActive(true);
            }
        }
        else if (aiChoice == 2) //scissors
        {
            ai_scissors_select.SetActive(true);
            if (playerChoice == 1) //rock
            {
                player_rock_select.SetActive(true);
                resultVal = 1;
                winIcon.SetActive(true);
            }
            else if (playerChoice == 2) //paper
            {
                player_paper_select.SetActive(true);
                resultVal = -1;
                loseIcon.SetActive(true);
            }
            else if (playerChoice == 3) //scissors
            {
                player_scissors_select.SetActive(true);
                resultVal = 0;
                drawIcon.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(endGame == false) //出完拳階段
        {
            if(idleTimer2 > 0)
            {
                idleTimer2 -= Time.deltaTime;
                if(idleTimer2 < 0)
                {
                    idleTimer2 = 0;
                    playerReady.SetActive(true);
                    playerSelect.SetActive(false);
                    aiSelect.SetActive(false);
                    winIcon.SetActive(false);
                    drawIcon.SetActive(false);
                    loseIcon.SetActive(false);
                    systemText.text = "";
                    for (int i = 0; i < 10; i++)
                    {
                        signSet[i].SetActive(false);
                    }
                    result(resultVal);
                }
            }
            else if (handSignTime > 0)
            {
                handSignTime -= Time.deltaTime;
                
                if(resultVal == -1)
                {
                    systemText.text = "請比出下方的手語";
                    toDoSignStr.text = signChineseName[targetSign];
                    countDownTimer.text = "";
                }
                else if(resultVal == 1)
                {
                    systemText.text = "請比一個手語";
                    countDownTimer.text = ((int)(handSignTime + 1)).ToString();
                }

                

                if (handSignTime <= 0)
                {
                    handSignTime = 0;
                    countDownTimer.text = "";

                    idleTimer2 = 3.0f;

                    toDoSignStr.text = "";

                    if (resultVal == 1)
                    {
                        if(receivedText == "none")
                        {
                            systemText.text = "您沒有比任何手勢";
                        }
                        else
                        {
                            systemText.text = "您比的是：";
                            for(int i = 0; i < 10; i++)
                            {
                                if(receivedText == signName[i])
                                {
                                    signSet[i].SetActive(true);
                                    break;
                                }
                            }
                        }
                        
                    }
                    else if (resultVal == -1)
                    {
                        if (receivedText == signName[targetSign])
                        {
                            systemText.text = "手勢正確";
                            for (int i = 0; i < 10; i++)
                            {
                                signSet[i].SetActive(false);
                            }
                            resultVal = 1;
                            winIcon.SetActive(true);
                        }
                        else
                        {
                            systemText.text = "手勢錯誤";
                            loseIcon.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                if (idleTimer > 0)
                {
                    idleTimer -= Time.deltaTime;
                    if (idleTimer < 0)
                    {
                        idleTimer = 0;

                        if(resultVal != 0)
                        {
                            handSignTime = 4.99f;
                            targetSign = UnityEngine.Random.Range(0, 10);
                            countDownTimer.text = "";
                            playerChoice = 0;
                            playerReady.SetActive(false);
                            playerSelect.SetActive(false);
                            aiSelect.SetActive(false);
                            winIcon.SetActive(false);
                            drawIcon.SetActive(false);
                            loseIcon.SetActive(false);
                        }
                        else
                        {
                            playerChoice = 0;
                            countDownTimer.text = "";
                            playerReady.SetActive(true);
                            playerSelect.SetActive(false);
                            aiSelect.SetActive(false);
                            winIcon.SetActive(false);
                            drawIcon.SetActive(false);
                            loseIcon.SetActive(false);
                        }
                    }
                }
                else //出拳階段
                {
                    roundTimer -= Time.deltaTime;
                    if (roundTimer <= 0)
                    {
                        battle();
                        roundTimer = 4.99f;
                        countDownTimer.text = "";
                    }
                    else
                    {
                        countDownTimer.text = ((int)(roundTimer + 1)).ToString();

                        if (receivedText == "rock")
                        {
                            playerChoice = 1;
                        }
                        else if (receivedText == "paper")
                        {
                            playerChoice = 2;
                        }
                        else if (receivedText == "scissors")
                        {
                            playerChoice = 3;
                        }

                        rock_ready.GetComponent<SpriteRenderer>().color = trans;
                        paper_ready.GetComponent<SpriteRenderer>().color = trans;
                        scissors_ready.GetComponent<SpriteRenderer>().color = trans;

                        if (playerChoice == 1)
                        {
                            rock_ready.GetComponent<SpriteRenderer>().color = original;
                        }
                        else if (playerChoice == 2)
                        {
                            paper_ready.GetComponent<SpriteRenderer>().color = original;
                        }
                        else if (playerChoice == 3)
                        {
                            scissors_ready.GetComponent<SpriteRenderer>().color = original;
                        }
                    }
                }
            }

           
        }
        


    }

}
