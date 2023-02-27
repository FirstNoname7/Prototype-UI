using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //���������� ������ TextMeshPro
using UnityEngine.SceneManagement; //��������� ���������� SceneManagement �� ������ UnityEngine, ���� ����� ���� ����������������� �� ������� �� ����� �������
using UnityEngine.UI; //��������� ���������� UI �� ���������� �� ������ UnityEngine, ���� ����� ���� ������������ �������� UI

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets; //������ ������� �������� (�� �������� ������ �� ������, �� ������� ����� ���� ��-���� ����)
    public TextMeshProUGUI scoreText; //����� ������, ������� ����� ������ �����
    public TextMeshProUGUI livesText; //����� ������, ������� ������ �������� � ������ �� ����� ����
    public TextMeshProUGUI gameOverText; //����� ������ "���� ���������" �� ��������� ����
    public Button restartButton; //������ Restart (������� �� ��������� ����)
    public bool isGameActive; //��������, ������� �� ������ ����
    public GameObject titleScreen; //���������� titleScreen ��� �������� ������� ���������� ������ (��� ��� ���������� ������� ������� ���������)
    public GameObject pauseScreen; //������� ������ ��� ����� ����
    public bool paused; //��������, ����� ������ ��� ���
    private int score; //���-�� �����
    public int lives; //���-�� ������
    private float spawnRate = 1.0f; //������ ��� ��������� �������� (�����, ������)


    IEnumerator SpawnTarget() //������������������ �������� ��� ������ ��������
    {
        //���� while - ��� ������� ����� for � ������� if. �� ���� �� �������� ��� for, �� ������ ��� ���������� ����������� ������� if, ������ ��� �������� �� �����
        //for �������� ����������� ���������� ���, � while �������� ��� ���������� �������.
        while (isGameActive) //���� ���� �������, �� ���� ��������
        {
            yield return new WaitForSeconds(spawnRate); //����� spawnRate ������ ����� ������� �������� �� ������ ���� ����
            int index = Random.Range(0, targets.Count); //������������� ������� �������� �� ����� targets �� ���� �� ���������� �������� (targets.Count)
            Instantiate(targets[index]); //����� ��������. targets[index] - ������[������ �������]
        }
    }

    public void UpdateScore(int scoreToAdd) //������� ��� ���������� ���-�� ����� ��� ���������. int scoreToAdd - ���������� �������� �����.
    {
        score += scoreToAdd; //���-�� ����� ��������, ������������
        scoreText.text = "����: " + score; //��, ��� ��������� �� ����� ��� ������ TextMeshPro
    }

    public void UpdateLives(int livesToAdd) //������� ��� ����������� ���-�� ������. �������� int livesToAdd �������� �� ����������� ���������� (��� ����� ���-�� ������ �� ����� ������������)
    {
        if (lives <= 0) //���� ������ ������ ��� ����� ����, ��:
        {
            lives = 0; //��� ����� ���������� ������ ����� ���� (���� �� ���� ������������� ��������, ����� ���� ��� �����������)
            livesToAdd=0; //������������ ���-�� ������ = 0 (���� �� ������������ ������������� ��������, ����� ���� ��� �����������)
            GameOver(); //���� ������������� (������������ ������� GameOver)
        }
        livesText.text = "�����: " + lives; //��, ��� ��������� �� ����� ��� ������ TextMeshPro
    }

    public void GameOver() //������� ��� ������ ������ "���� ��������" � � ����� ���������� ����
    {
        gameOverText.gameObject.SetActive(true); //�������� (������ ���������� �� ������) ������� ������ "���� ��������" 
        restartButton.gameObject.SetActive(true); //�������� ������� ������: ������ �������
        isGameActive = false; //���� �� �������
    }

    public void RestartGame() //������� ��� �������� ����
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //������������ �����, ������� �� ������ ����������. SceneManager.LoadScene - �������� �����.
                                                                    //SceneManager.GetActiveScene().name - ��������� ����� � ����������� ������ (��������� ��� �� �������, ������ ��������� �����, ����� ������������ � ��������� ������)
    }

    public void StartGame(int difficulty) //��������� ����� ����. �������� difficulty ������������� ��������� ������ �� ���, ������� ������ �����
    {
        titleScreen.gameObject.SetActive(false); //��������� ����� � ������� ��������� ����
        isGameActive = true; //��� ������ ���� �������
        lives = 3; //��� ������ ���-�� ������ = 3
        score = 0; //��� ������ ���-�� ����� = 0
        spawnRate /= difficulty; //������������� ��������� ����� �������: spawnRate = spawnRate / difficulty;. ��� ������ ���������, ��� ������ difficulty, �������������� �������� ����� ��� ������ ���������� � ����� ����� ������. ������: ���� ����������� ��������� 1, �� spawnRate / 1 = spawnRate ������. ���� ��������� 2, �� spawnRate / 2 = 1/2(spawnRate) ������ (�������� �������� ��� ��)
        StartCoroutine(SpawnTarget()); //���������� ��������� � ������ SpawnTarget
        UpdateScore(0); //��� ���� ��� ����������� �����������. �������� ������� � ������ UpdateScore. �������� int scoreToAdd ���������� ����� 0, �� ���� ��� ������ � ��� 0 �����.
        UpdateLives(3); //��� ���� ��� ����������� �����������. �������� ������� � ������ UpdateLives. �������� int livesToAdd ���������� ����� 3, �� ���� ��� ������ � ��� 3 �����.
    }

    void ChangePaused() //�������, ������������, ���� ����� ��������� ���� �� �����
    {
        if (!paused) //���� ������ �� �����, ��:
        {
            paused = true; //�������� �����
            pauseScreen.SetActive(true); //�������� ������ ��� ������ �����
            Time.timeScale = 0; //������������ ���������� ���������� (�� ��������)
        }
        else //� �������� ������ (���� ����� ��������):
        {
            paused = false; //��������� �����
            pauseScreen.SetActive(false); //��������� ������ ������ �����
            Time.timeScale = 1; //��������� ���������� ���������� (���� ����� �����������)

        }
    }

    void Update() //�������, ������������ ���������
    {
        if (isGameActive) //���� ���� �������, ��:
        {
            if (Input.GetKeyDown(KeyCode.P)) //���� ������ �� ������ P, ��:
            {
                ChangePaused(); //��������� ����� ����� (� ���� ����� ���������, �� ��� ���������, � ���� ��������, �� ����������)
            }

        }
    }
}
