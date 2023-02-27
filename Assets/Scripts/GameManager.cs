using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //используем модуль TextMeshPro
using UnityEngine.SceneManagement; //подключаю библиотеку SceneManagement из модуля UnityEngine, чтоб можно было взаимодействовать со сценами из этого скрипта
using UnityEngine.UI; //подключаю библиотеку UI по интерфейсу из модуля UnityEngine, чтоб можно было использовать элементы UI

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets; //список игровых объектов (по функциям похоже на массив, но разница между ними всё-таки есть)
    public TextMeshProUGUI scoreText; //вывод текста, сколько очков набрал игрок
    public TextMeshProUGUI livesText; //вывод текста, сколько жизней осталось у игрока до конца игры
    public TextMeshProUGUI gameOverText; //вывод текста "Игра закончена" по окончании игры
    public Button restartButton; //кнопка Restart (рестарт по окончании игры)
    public bool isGameActive; //проверка, активна ли сейчас игра
    public GameObject titleScreen; //переменная titleScreen для игрового объекта стартового экрана (где нам предлагают выбрать уровень сложности)
    public GameObject pauseScreen; //игровой объект для паузы игры
    public bool paused; //проверка, пауза сейчас или нет
    private int score; //кол-во очков
    public int lives; //кол-во жизней
    private float spawnRate = 1.0f; //таймер для появления объектов (кубов, врагов)


    IEnumerator SpawnTarget() //последовательность действий для спауна объектов
    {
        //цикл while - это слияние цикла for и условия if. То есть он работает как for, но только при выполнении определённых условий if, просто так работать не будет
        //for работает определённое количество раз, а while работает при выполнении условий.
        while (isGameActive) //если игра активна, то цикл работает
        {
            yield return new WaitForSeconds(spawnRate); //через spawnRate секунд будет активно действие на строке чуть ниже
            int index = Random.Range(0, targets.Count); //рандомизируем индексы объектов из листа targets от нуля до последнего элемента (targets.Count)
            Instantiate(targets[index]); //спаун объектов. targets[index] - объект[индекс объекта]
        }
    }

    public void UpdateScore(int scoreToAdd) //функция для обновления кол-ва очков при изменении. int scoreToAdd - меняющееся значение очков.
    {
        score += scoreToAdd; //кол-во очков меняется, прибавляется
        scoreText.text = "Очки: " + score; //то, что выводится на экран при помощи TextMeshPro
    }

    public void UpdateLives(int livesToAdd) //функция для отображения кол-ва жизней. Параметр int livesToAdd отвечает за отображение переменной (без этого кол-во жизней не будет показываться)
    {
        if (lives <= 0) //если жизней меньше или равно нулю, то:
        {
            lives = 0; //эта самая переменная жизней равна нулю (чтоб не было отрицательных значений, когда игра уже закончилась)
            livesToAdd=0; //отображаемое кол-во жизней = 0 (чтоб не отображались отрицательные значения, когда игра уже закончилась)
            GameOver(); //игра заканчивается (используется функция GameOver)
        }
        livesText.text = "Жизни: " + lives; //то, что выводится на экран при помощи TextMeshPro
    }

    public void GameOver() //функция для вывода текста "Игра окончена" и в целом завершения игры
    {
        gameOverText.gameObject.SetActive(true); //включаем (тобишь показываем на экране) игровой объект "Игра окончена" 
        restartButton.gameObject.SetActive(true); //включаем игровой объект: кнопку Рестарт
        isGameActive = false; //игра не активна
    }

    public void RestartGame() //функция для рестарта игры
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //перезагрузка сцены, которую мы сейчас используем. SceneManager.LoadScene - загрузка сцены.
                                                                    //SceneManager.GetActiveScene().name - загружаем сцену с определённым именем (поскольку оно не указано, значит загружаем сцену, когда используется в настоящий момент)
    }

    public void StartGame(int difficulty) //стартовый экран игры. Параметр difficulty устанавливает сложность исходя из той, которую выбрал игрок
    {
        titleScreen.gameObject.SetActive(false); //отключаем экран с выбором сложности игры
        isGameActive = true; //при старте игра активна
        lives = 3; //при старте кол-во жизней = 3
        score = 0; //при старте кол-во очков = 0
        spawnRate /= difficulty; //устанавливаем сложность таким образом: spawnRate = spawnRate / difficulty;. Чем меньше сложность, тем меньше difficulty, соответственно итоговое время для спауна увеличится и будет легче играть. Пример: если установлена сложность 1, то spawnRate / 1 = spawnRate секунд. Если сложность 2, то spawnRate / 2 = 1/2(spawnRate) секунд (поменьше значение как бы)
        StartCoroutine(SpawnTarget()); //используем нумератор с именем SpawnTarget
        UpdateScore(0); //это надо для визуального отображения. вызываем функцию с именем UpdateScore. Параметр int scoreToAdd изначально равен 0, то есть при старте у нас 0 очков.
        UpdateLives(3); //это надо для визуального отображения. вызываем функцию с именем UpdateLives. Параметр int livesToAdd изначально равен 3, то есть при старте у нас 3 жизни.
    }

    void ChangePaused() //функция, вызывающаяся, если нужно поставить игру на паузу
    {
        if (!paused) //если сейчас не пауза, то:
        {
            paused = true; //включаем паузу
            pauseScreen.SetActive(true); //включаем панель для экрана паузы
            Time.timeScale = 0; //приостановка физических вычислений (всё замирает)
        }
        else //в обратном случае (если пауза включена):
        {
            paused = false; //отключаем паузу
            pauseScreen.SetActive(false); //выключаем панель экрана паузы
            Time.timeScale = 1; //активация физических вычислений (игра снова запускается)

        }
    }

    void Update() //функция, вызывающаяся постоянно
    {
        if (isGameActive) //если игра активна, то:
        {
            if (Input.GetKeyDown(KeyCode.P)) //если нажать на кнопку P, то:
            {
                ChangePaused(); //включится экран паузы (и если пауза выключена, то она включится, а если включена, то выключится)
            }

        }
    }
}
