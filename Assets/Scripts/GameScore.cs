﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameScore : MonoBehaviour
{
    Text scoreTextUI;
    int score;
    int scoreTarget = 2000;
    int bossSpawnCount = 0;
    public GameObject bossSpawner; 
    public GameObject bossObj;
    [SerializeField] TextMeshProUGUI highScoreText;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            CheckHighScore();
            if (score >= scoreTarget)
            {
                SpawnBossAndSetHits();
                bossSpawnCount++;
                scoreTarget += 2000 + (bossSpawnCount * 1000);
            }
            UpdateScoreTextUI();
        }
    }

    void Start()
    {
        scoreTextUI = GetComponent<Text>();
        UpdateHighScoreText();
    }

    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:0000000}", score);
        scoreTextUI.text = scoreStr;
    }

    void SpawnBossAndSetHits()
    {

        // Spawn boss và lấy tham chiếu từ BossSpawer
        GameObject spawnedBoss = bossSpawner.GetComponent<BossSpawer>().SpawnBoss();
        if (spawnedBoss == null)
        {
            Debug.LogError("Failed to spawn boss!");
            return;
        }

        // Set requiredHits cho boss vừa spawn
        BossController bossController = spawnedBoss.GetComponent<BossController>();
        if (bossController != null)
        {
            int newRequiredHits = 50 + ((score / 2000) - 1) * 20; // 50, 70, 90, v.v.
            bossController.SetRequiredHits(newRequiredHits);
            Debug.Log($"Spawned boss with requiredHits: {newRequiredHits}");
        }
        else
        {
            Debug.LogError("BossController not found on spawned boss!");
        }
    }

    void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}