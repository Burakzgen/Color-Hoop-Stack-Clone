using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public GameObject locatedStand;
    public GameObject locatedCircle;

    public bool isMove;

    public string color;

    private GameObject _movedPosition, _locatedStand;

    private bool _selected, _changePosition, _socketOtur, _socketBack;

    public void Move(string process, GameObject stand = null, GameObject socket = null, GameObject objectToGo = null)
    {
        switch (process)
        {
            case "Selected":
                _movedPosition = objectToGo;
                _selected = true;
                break;
            case "ChangePosition":
                _locatedStand = stand;
                locatedCircle = socket;
                _movedPosition = objectToGo;
                _changePosition = true;
                break;
            case "SocketBack":
                _socketBack = true;
                break;
        }
    }
    void Update()
    {
        if (_selected) // Soketi secilme islemi
        {
            transform.position = Vector3.Lerp(transform.position, _movedPosition.transform.position, .2f);
            if (Vector3.Distance(transform.position, _movedPosition.transform.position) < .10)
            {
                _selected = false;
            }
        }

        if (_changePosition) // Soketi diger konumlardaki pozisyona hareket ettirme
        {
            transform.position = Vector3.Lerp(transform.position, _movedPosition.transform.position, .2f);
            if (Vector3.Distance(transform.position, _movedPosition.transform.position) < .10)
            {
                _changePosition = false;
                _socketOtur = true;
            }
        }

        if (_socketOtur) // Soketi diger konumlardaki yerine oturtma
        {
            transform.position = Vector3.Lerp(transform.position, locatedCircle.transform.position, .2f);
            if (Vector3.Distance(transform.position, locatedCircle.transform.position) < .10)
            {
                transform.position = locatedCircle.transform.position;
                _socketOtur = false;

                locatedStand = _locatedStand;

                if (locatedStand.GetComponent<Stand>().circle.Count > 1)
                {
                    locatedStand.GetComponent<Stand>().circle[locatedStand.GetComponent<Stand>().circle.Count - 2].GetComponent<Circle>().isMove = false;
                }
                gameManager.isMoved = false;
            }
        }

        if (_socketBack) // Soketi tekrar eski yerine getirme
        {
            transform.position = Vector3.Lerp(transform.position, locatedCircle.transform.position, .2f);
            if (Vector3.Distance(transform.position, locatedCircle.transform.position) < .10)
            {
                transform.position = locatedCircle.transform.position;
                _socketBack = false;

                gameManager.isMoved = false;
            }
        }
    }
}
