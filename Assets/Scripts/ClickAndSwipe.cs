using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TrailRenderer),typeof(BoxCollider))] //TrailRenderer - визуализация следов от мышки. BoxCollider - коллайдер.
                                                              //Такая шняга гарантирует, что эти 2 элемента находятся на игровом объекте, к которому прикреплён этот скрипт (она их прям создаёт, то есть при добавлении скрипта в объект добавятся ещё эти 2 компонента)

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager; //подключаю скрипт с именем GameManager
    private Camera cam; //подключаю объект Camera
    private Vector3 mousePos; //подключаю Vector3, чтоб отслеживать позицию курсора
    private TrailRenderer trail; //
    private BoxCollider col; //
    private bool swiping = false; //по дефолту курсор не свайпают
    private Target target;

    void Awake() //вызывается перед стартом
    {
        cam = Camera.main; //подключаю камеру
        trail = GetComponent<TrailRenderer>(); //подключаю компонент TrailRenderer (он нужен для создания шлейфов за движущимися в сцене объектами)
        col = GetComponent<BoxCollider>(); //подключаю коллайдер
        trail.enabled = false; //по дефолту переменная trail отключена
        col.enabled = false; //по дефолту переменная col отключена
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //подключаю скрипт GameManager как компонент игрового объекта GameManager в иерархии


    }

    void UpdateMousePosition() //функция для отслеживания позиции курсора
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)); //отслеживаем позицию курсора по оси X и Y, по Z ставим своё значение (ибо курсор всё равно только по горизонтали и вертикали двигается)
        transform.position = mousePos; //применяем к позиции объекта, к которому прикреплён скрипт, изменения из переменной mousePos
    }

    void UpdateComponents() //функция для включения коллайдера и создания шлейфа при свайпе
    {
        trail.enabled = swiping; //создание шлейфа
        col.enabled = swiping; //подключение коллайдера для того, чтобы взаимодействовать с триггерами
    }

    void Update() //обновляется каждый фрейм (постоянно)
    {
        if (gameManager.isGameActive) //если игра активна, то:
        {
            if (Input.GetMouseButtonDown(0)) //если нажата левая (0 - левая, 1 - правая) кнопка мыши, то: 
            {
                swiping = true; //можно свайпать по предметам
                UpdateComponents(); //выполняется содержимое функции UpdateComponents
            }
            else if (Input.GetMouseButtonUp(0)) //если игрок отпустил левую кнопку мыши, то:
            {
                swiping = false; //нельзя свайпать по предметам
                UpdateComponents(); //выполняется содержимое функции UpdateComponents
            }
            if (swiping) //если в настоящий момент игрок свайпает мышкой, то:
            {
                UpdateMousePosition(); //выполняется содержимое функции UpdateMousePosition (следим за курсором)
            }
        }
    }

    void OnCollisionEnter(Collision collision) //юнитовская функция, вызывается при столкновении 2 жёстких тел или жёсткого тела и коллайдера. collision - то, с чем сталкивается объект, к которому прикреплён этот скрипт (то есть кубы и бомбы)
    {
        if (collision.gameObject.GetComponent<Target>()) //если у игрового объекта, с которым столкнулся курсор, есть компонент Target (в данном случае скрипт с таким названием), то:
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget(); //выполняется содержимое функции DestroyTarget, которая находится в скрипте Target
        }
    }
}
