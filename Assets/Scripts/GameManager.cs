﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //定数定義：壁方向
    public const int WALL_FRONT = 1; //前
    public const int WALL_RIGHT = 2; //右
    public const int WALL_BACK  = 3; //後
    public const int WALL_LEFT  = 4; //左

    //定数定義：ボタンカラー
    public const int COLOR_GREEN = 0; //緑
    public const int COLOR_RED = 1;   //赤
    public const int COLOR_BLUE = 2;  //青
    public const int COLOR_WHITE = 3; //白

    public GameObject panelWalls; //壁全体

    public GameObject buttonHammer; //ボタン：トンカチ
    public GameObject buttonKey; //ボタン：鍵

    public GameObject imageHammerIcon; //アイコン：トンカチ
    public GameObject imageKeyIcon; //アイコン：鍵

    public GameObject buttonPig; //ボタン：ブタの貯金箱

    public GameObject buttonMessage; //ボタン:メッセージ
    public GameObject buttonMessageText; //メッセージテキスト

    public GameObject[] buttonLamp = new GameObject[3]; //ボタン：金庫
    public Sprite[] buttonPicture = new Sprite[4]; //ボタンの絵
    public Sprite hammerPicture;  //トンカチの絵
    public Sprite keyPicture; //鍵の絵

    private int wallNo; //現在の向いている方向
    private bool doesHaveHammer; //トンカチを持っているか
    private bool doesHaveKey; //鍵を持っているか？
    private int[] buttonColor = new int[3]; //金庫のボタン

	// Use this for initialization
	void Start () {
        wallNo = WALL_FRONT; //スタート時点では「前」を向く
        doesHaveHammer = false; //トンカチは持っていない
        doesHaveKey = false; //鍵は「持っていない」

        buttonColor[0] = COLOR_GREEN; //ボタン1の色は「緑」
        buttonColor[1] = COLOR_RED;　//ボタン2の色は「赤」
        buttonColor[2] = COLOR_BLUE; //ボタン3の色は「青」
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //ボックスをタップ
    public void PushButtonBox()
    {
        if (doesHaveKey == false)
        {
            //鍵を持っていない
            DisplayMessage("鍵がかかっている。");
        } else
        {
            //鍵を持っている
            SceneManager.LoadScene("ClearScene");
        }
    }

    //金庫のボタン1をタップ
    public void PushButtonLamp1 ()
    {
        ChangeButtonColor(0);
    }

    //金庫のボタン2をタップ
    public void PushButtonLamp2 ()
    {
        ChangeButtonColor(1);
    }

    //金庫のボタン3をタップ
    public void PushButtonLamp3 ()
    {
        ChangeButtonColor(2);
    }

    //金庫のボタンの色を変更
    void ChangeButtonColor (int buttonNo)
    {
        buttonColor[buttonNo]++;
        //「白」のときにボタンを押したら「緑」に
        if(buttonColor [buttonNo] > COLOR_WHITE)
        {
            buttonColor[buttonNo] = COLOR_GREEN;
        }

        //ボタンの画像を変更
        buttonLamp[buttonNo].GetComponent<Image>().sprite =
            buttonPicture[buttonColor[buttonNo]];

        //ボタンの色順をチェック
        if ((buttonColor [0] == COLOR_BLUE) &&
            (buttonColor [1] == COLOR_WHITE) &&
            (buttonColor [2] == COLOR_RED))
        {
            //まだトンカチを持っていない？
            if (doesHaveHammer == false)
            {
                DisplayMessage("金庫の中にトンカチが入っていた。");
                buttonHammer.SetActive(true); //トンカチの絵を表示
                imageHammerIcon.GetComponent<Image>().sprite = hammerPicture;

                doesHaveHammer = true;
            }
        }
    }

    // メモをタップ
    public void PushButtonMemo ()
    {
        DisplayMessage(" エッフェル塔と書いてある。");
    }

    //貯金箱をタップ
    public void PushButtonPig()
        //トンカチを持っているか
    {
        if (doesHaveHammer == false)
        {
            //トンカチを持っていない
            DisplayMessage("素手では割れない。");
        } else
        {
            //トンカチを持っている
            DisplayMessage("貯金箱が割れて中から鍵が出てきた。");
            buttonPig.SetActive(false); //貯金箱を消す
            buttonKey.SetActive(true); //鍵の絵を表示
            imageKeyIcon.GetComponent<Image>().sprite = keyPicture;

            doesHaveKey = true;
        }
    }

    //　トンカチの絵をタップ
    public void PushButtonHammer ()
    {
        buttonHammer.SetActive(false);
    }

    // 鍵の絵をタップ
    public void PushButtonKey()
    {
        buttonKey.SetActive(false);
    }

    // メッセージをタップ
    public void PushButtonMessage()
    {
        buttonMessage.SetActive(false); //メッセージを消す
    }


    //右(>)ボタンを押した
    public void PushButtonRight()
    {
        wallNo++; //方向を１つ右に
        //「左」の1つ右は「前」
        if (wallNo > WALL_LEFT)
        {
            wallNo = WALL_FRONT;
        }
        DisplayWall (); //壁表示更新
        ClearButtons(); //いらない物を消す
    }

    public void PushButtonLeft()
    {
        wallNo--; //方向を１つ左に
        //「前」の１つ左は「左」
        if (wallNo < WALL_FRONT)
        {
            wallNo = WALL_LEFT;
        }
        DisplayWall(); //壁表示更新
        ClearButtons(); //いらない物を消す
    }

    //各種表示をクリア
    void ClearButtons()
    {
        buttonHammer.SetActive(false);
        buttonKey.SetActive(false);
        buttonMessage.SetActive(false);
    }

    //メッセージを表示
    void DisplayMessage(string mes)
    {
        buttonMessage.SetActive(true);
        buttonMessageText.GetComponent<Text>().text = mes;
    }

    //向いている方向の壁を表示
    void DisplayWall()
    {
        switch (wallNo)
        {
            case WALL_FRONT: //前
                panelWalls.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case WALL_RIGHT: //右
                panelWalls.transform.localPosition = new Vector3(-1000.0f, 0.0f, 0.0f);
                break;
            case WALL_BACK: //後
                panelWalls.transform.localPosition = new Vector3(-2000.0f, 0.0f, 0.0f);
                break;
            case WALL_LEFT: //左
                panelWalls.transform.localPosition = new Vector3(-3000.0f, 0.0f, 0.0f);
                break;
        }
    }
}

