using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TrailRenderer),typeof(BoxCollider))] //TrailRenderer - ������������ ������ �� �����. BoxCollider - ���������.
                                                              //����� ����� �����������, ��� ��� 2 �������� ��������� �� ������� �������, � �������� ��������� ���� ������ (��� �� ���� ������, �� ���� ��� ���������� ������� � ������ ��������� ��� ��� 2 ����������)

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager; //��������� ������ � ������ GameManager
    private Camera cam; //��������� ������ Camera
    private Vector3 mousePos; //��������� Vector3, ���� ����������� ������� �������
    private TrailRenderer trail; //
    private BoxCollider col; //
    private bool swiping = false; //�� ������� ������ �� ��������
    private Target target;

    void Awake() //���������� ����� �������
    {
        cam = Camera.main; //��������� ������
        trail = GetComponent<TrailRenderer>(); //��������� ��������� TrailRenderer (�� ����� ��� �������� ������� �� ����������� � ����� ���������)
        col = GetComponent<BoxCollider>(); //��������� ���������
        trail.enabled = false; //�� ������� ���������� trail ���������
        col.enabled = false; //�� ������� ���������� col ���������
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //��������� ������ GameManager ��� ��������� �������� ������� GameManager � ��������


    }

    void UpdateMousePosition() //������� ��� ������������ ������� �������
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)); //����������� ������� ������� �� ��� X � Y, �� Z ������ ��� �������� (��� ������ �� ����� ������ �� ����������� � ��������� ���������)
        transform.position = mousePos; //��������� � ������� �������, � �������� ��������� ������, ��������� �� ���������� mousePos
    }

    void UpdateComponents() //������� ��� ��������� ���������� � �������� ������ ��� ������
    {
        trail.enabled = swiping; //�������� ������
        col.enabled = swiping; //����������� ���������� ��� ����, ����� ����������������� � ����������
    }

    void Update() //����������� ������ ����� (���������)
    {
        if (gameManager.isGameActive) //���� ���� �������, ��:
        {
            if (Input.GetMouseButtonDown(0)) //���� ������ ����� (0 - �����, 1 - ������) ������ ����, ��: 
            {
                swiping = true; //����� �������� �� ���������
                UpdateComponents(); //����������� ���������� ������� UpdateComponents
            }
            else if (Input.GetMouseButtonUp(0)) //���� ����� �������� ����� ������ ����, ��:
            {
                swiping = false; //������ �������� �� ���������
                UpdateComponents(); //����������� ���������� ������� UpdateComponents
            }
            if (swiping) //���� � ��������� ������ ����� �������� ������, ��:
            {
                UpdateMousePosition(); //����������� ���������� ������� UpdateMousePosition (������ �� ��������)
            }
        }
    }

    void OnCollisionEnter(Collision collision) //���������� �������, ���������� ��� ������������ 2 ������ ��� ��� ������� ���� � ����������. collision - ��, � ��� ������������ ������, � �������� ��������� ���� ������ (�� ���� ���� � �����)
    {
        if (collision.gameObject.GetComponent<Target>()) //���� � �������� �������, � ������� ���������� ������, ���� ��������� Target (� ������ ������ ������ � ����� ���������), ��:
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget(); //����������� ���������� ������� DestroyTarget, ������� ��������� � ������� Target
        }
    }
}
