using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //��������� ���������� UI �� ���������� �� ������ UnityEngine, ���� ����� ���� ������������ �������� UI (� ������ ������ ������ button)

public class DifficultyButton : MonoBehaviour
{
    //���� ������ ����� ��� ��������� �������: ������, �������� � ��������
    private Button button; //������ ���������
    private GameManager gameManager; //���������� ���� ������ � ������ GameManager 
    public int difficulty; //���������� ��� ��������� ��������� ����. ��� EasyButton = 1, MediumButton = 2, HardButton = 3
    void Start()
    {
        button = GetComponent<Button>(); //���������� ��������� ������ <Button>, ���� ����� ���� � ��� �����������������
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //���������� ������ GameManager, �������� �� ���� ��� �� ��������� ��������� (GetComponent) �������� ������� � ������ "GameManager"
        button.onClick.AddListener(SetDifficulty); //�����������, ��� ��������� ��� ����� (onClick) �� ������ (button): AddListener - ��, ��� ��������� ��������, ��������� � ������� (��������� ������� SetDifficulty)
    }

    void Update()
    {
        
    }

    void SetDifficulty() //������� ��� ��������� ���������, ������� ������ �����
    {
        Debug.Log(button.gameObject.name + " ������");
        gameManager.StartGame(difficulty); //�� ���� ����� ��������� ��������� ���� ���������� (����������� �������� ������� StartGame �� ������� GameManager). ������ ���� ���������� � ������ ��������� ��������� (difficulty)
    }
}
