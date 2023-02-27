using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb; //������������� ������ ���� � ������� ���������� targetRb
    private GameManager gameManager; //��������� �� ������ � ������ GameManager
    private float minSpeed = 102; //����������� �������� �������, � �������� ��������� ���� ������
    private float maxSpeed = 106; //������������ �������� �������, � �������� ��������� ���� ������
    private float maxTorque = 10; //������������ �������� �������, � �������� ��������� ���� ������
    private float xRange = 4; //������� �� ��� �
    private float ySpawnPos = -2; //������� �� ��� Y

    public int pointValue; //������� ����� ���������� ������, � �������� ��������� ���� ������ (������ ������ ������ ���-�� ����� ���)
    public ParticleSystem explosionParticle; //������ ������

    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); //�������������� ������ ����
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //���������� ���� ������ � ��������� GameManager (GetComponent<GameManager>()). ��������� ��� �� �������� ������� � �������� � ��������� GameManager (GameObject.Find("GameManager"))
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);  //�������� �������, � �������� ��������� ������. ForceMode.Impulse - ��� ����� ������ ������. ��� ����������� ��������� ������� ���������� ������� RandomForce()
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse); //AddTorque - �������� ������ ��� ������� ����. ForceMode.Impulse - ��� ����� ������ ������. ��� ����������� �������� ���������� ������� RandomTorque()
        transform.position = RandomSpawnPos(); //������������ ������� �������. ��� ��������� ������� � �������-������� RandomSpawnPos()
    }

    private void OnMouseDown() //��� ����� ������ �� ������, � �������� ��������� ������:
    {
        if (gameManager.isGameActive&&!gameManager.paused) //���� ���� ������� (���������� ����� �� ������� GameManager) � ���� ���� �� �� �����, ��:
        {
            Destroy(gameObject); //���� ������� ������ ��������
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); //��������� ������ ������ �� ������� �������, � �������� ��������� ���� ������ (transform.position), � �� ����� ��������� (explosionParticle.transform.rotation)
            gameManager.UpdateScore(pointValue); //���-�� ����� ������������� �� ��, ������� ������� � �������� ������� �������, � ������� ��������� ���� ������
        }
    }

    private void OnTriggerEnter(Collider other) //��� ������������ �������, � �������� ��������� ������, � ��������� (� �� ���� ������ � ������� Sensor)
    {
        Destroy(gameObject); //������� ������, � �������� ��������� ������, ���������
        if (!gameObject.CompareTag("Bad")) //���� ������ �� � ����� Bad ���������� � ��������� (� ������ ������ ������), ��:
        {
            gameManager.lives -= 1; //���������� lives �� ������� GameManager ����������� �� ������� (����� 1 �����)
            gameManager.UpdateLives(3); //������������ ������� UpdateLives() �� ������� GameManager. � ������� �������, ������� ������ ���������� � ������.
        }
    }

    public void DestroyTarget() //������� ��� �������� ��������, � ������� ��������� ���� ������
    {
        if (gameManager.isGameActive) //���� ���������� isGameActive �� ������� GameManager ����� true, ������ ���� �� ��������, �:
        {
            Destroy(gameObject); //��������� ������� ������, � �������� ��������� ���� ������
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); //��������� ������ ������
            gameManager.UpdateScore(pointValue); //������������ ���� (���-�� ������� � ���������� ������� ������� � �����������)
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed) * Time.deltaTime; //Vector3.up - �����������, � ������� �������� ������ (�����),
                                                                               //Random.Range(12, 16) - ��������� �������� �������� �������� �������
                                                                               //Time.deltaTime - ���� �� ������ ����������� �������� ������� ���� ���������

    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque); //�� ��� X Y Z ���� ������ �������������� �� -10 �� 10 (Random.Range(-10, 10)).
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0); //��� �������� �� ��� � (Random.Range(-4, 4)), �� ��� Y ����� -6 (�������� ����� ������), �� ��� Z ����� 0

    }
}
