using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //подключаю библиотеку UI по интерфейсу из модуля UnityEngine, чтоб можно было использовать элементы UI (в данном случае кнопки button)

public class DifficultyButton : MonoBehaviour
{
    //этот скрипт нужен для настройки уровней: лёгкого, среднего и сложного
    private Button button; //кнопки сложности
    private GameManager gameManager; //подключаем сюда скрипт с именем GameManager 
    public int difficulty; //переменная для установки сложности игры. Для EasyButton = 1, MediumButton = 2, HardButton = 3
    void Start()
    {
        button = GetComponent<Button>(); //подключаем компонент кнопки <Button>, чтоб можно было с ней взаимодействовать
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //подключает скрипт GameManager, ссылаясь на него как на отдельный компонент (GetComponent) игрового объекта с именем "GameManager"
        button.onClick.AddListener(SetDifficulty); //указывается, что произойдёт при клике (onClick) на кнопку (button): AddListener - то, что выполняет действие, указанное в скобках (открывает функцию SetDifficulty)
    }

    void Update()
    {
        
    }

    void SetDifficulty() //функция для установки сложности, которую выбрал игрок
    {
        Debug.Log(button.gameObject.name + " нажато");
        gameManager.StartGame(difficulty); //то есть после установки сложности игра начинается (становитсчя активной функция StartGame из скрипта GameManager). Причём игра начинается с учётом выбранной сложности (difficulty)
    }
}
