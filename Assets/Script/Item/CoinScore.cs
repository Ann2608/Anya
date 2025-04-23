using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text coinText;

    private int coin = 0;

    private void Start()
    {

    }
    public void ThemDiem(int diem)
    {
        coin += diem;
        UpdateScoreDisplay();
    }
    private void UpdateScoreDisplay()
    {
        coinText.text = "Score: " + coin.ToString();
    }
}
