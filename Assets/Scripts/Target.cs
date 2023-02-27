using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb; //устанавливаем жёсткое тело с помощью переменной targetRb
    private GameManager gameManager; //ссылаемся на скрипт с именем GameManager
    private float minSpeed = 102; //минимальная скорость объекта, к которому прикреплён этот скрипт
    private float maxSpeed = 106; //максимальная скорость объекта, к которому прикреплён этот скрипт
    private float maxTorque = 10; //максимальное вращение объекта, к которому прикреплён этот скрипт
    private float xRange = 4; //позиция по оси Х
    private float ySpawnPos = -2; //позиция по оси Y

    public int pointValue; //сколько очков прибавляет объект, к которому прикреплён этот скрипт (каждый объект разное кол-во очков даёт)
    public ParticleSystem explosionParticle; //эффект взрыва

    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); //инициализируем жёсткое тело
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //подключаем сюда скрипт с названием GameManager (GetComponent<GameManager>()). Извлекаем его из игрового объекта в иерархии с названием GameManager (GameObject.Find("GameManager"))
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);  //движение объекта, к которому прикреплён скрипт. ForceMode.Impulse - для более чистой физики. Для подключения остальных функций используем функцию RandomForce()
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse); //AddTorque - крутящий момент для жёсткого тела. ForceMode.Impulse - для более чистой физики. Для подключения вращения используем функцию RandomTorque()
        transform.position = RandomSpawnPos(); //устанавливаю позицию объекта. Все параметры указаны в функции-векторе RandomSpawnPos()
    }

    private void OnMouseDown() //при клике мышкой на объект, к которому прикреплён скрипт:
    {
        if (gameManager.isGameActive&&!gameManager.paused) //если игра активна (переменная взята из скрипта GameManager) и если игра не на паузе, то:
        {
            Destroy(gameObject); //этот игровой объект удалится
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); //спаунится эффект взрыва по позиции объекта, к которому прикреплён этот скрипт (transform.position), и со своим вращением (explosionParticle.transform.rotation)
            gameManager.UpdateScore(pointValue); //кол-во очков увеличивается на то, которое указано в иерархии каждого объекта, к которым прикреплён этот скрипт
        }
    }

    private void OnTriggerEnter(Collider other) //при столкновении объекта, к которому прикреплён скрипт, с триггером (а он есть только у объекта Sensor)
    {
        Destroy(gameObject); //игровой объект, к которому прикреплён скрипт, удаляется
        if (!gameObject.CompareTag("Bad")) //если объект НЕ с тегом Bad столкнулся с триггером (с нижней частью экрана), то:
        {
            gameManager.lives -= 1; //переменная lives из скрипта GameManager уменьшается на единицу (минус 1 жизнь)
            gameManager.UpdateLives(3); //используется функция UpdateLives() из скрипта GameManager. В скобках указано, сколько жизней изначально у игрока.
        }
    }

    public void DestroyTarget() //функция для удаления объектов, к которым прикреплён этот скрипт
    {
        if (gameManager.isGameActive) //если переменная isGameActive из скрипта GameManager равна true, значит игра не окончена, и:
        {
            Destroy(gameObject); //удаляется игровой объект, к которому прикреплён этот скрипт
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); //спаунится эффект взрыва
            gameManager.UpdateScore(pointValue); //прибавляются очки (кол-во указано в инспекторе каждого объекта в отдельности)
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed) * Time.deltaTime; //Vector3.up - направление, в котором движется объект (вверх),
                                                                               //Random.Range(12, 16) - рандомное значение скорости движения объекта
                                                                               //Time.deltaTime - чтоб на разных устройствах скорость объекта была одинакова

    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque); //По оси X Y Z этот момент рандомизирован от -10 до 10 (Random.Range(-10, 10)).
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0); //Она рандомна по оси Х (Random.Range(-4, 4)), по оси Y равна -6 (появится снизу экрана), по оси Z равна 0

    }
}
