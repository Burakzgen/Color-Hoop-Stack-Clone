using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stand : MonoBehaviour
{
    public GameObject movePosition;
    public GameObject[] sockets;
    public int emptySocket;
    public List<GameObject> circle = new List<GameObject>();

    [SerializeField] private GameManager gameManager;

    int _completedCircleValue;
    public GameObject GetFirstCircle()
    {
        return circle[circle.Count - 1];
    }
    public GameObject GetEmptySocket()
    {
        return sockets[emptySocket];
    }
    public void ChangeSocketControls(GameObject deletedObject)
    {
        circle.Remove(deletedObject);
        if (circle.Count != 0)
        {
            emptySocket--;
            circle[circle.Count - 1].GetComponent<Circle>().isMove = true;
        }
        else
        {
            emptySocket = 0;
        }
    }

    public void CheckCircle()
    {
        if (circle.Count == 4)
        {
            string color = circle[0].GetComponent<Circle>().color;
            foreach (var item in circle)
            {
                if (color == item.GetComponent<Circle>().color)
                    _completedCircleValue++;
            }
            if (_completedCircleValue == 4)
            {
                gameManager.CompletedStand();
                CompletedStandSetting();
            }
            else
            {
                _completedCircleValue = 0;
            }
        }
    }
    void CompletedStandSetting()
    {
        foreach (var item in circle)
        {
            item.GetComponent<Circle>().isMove = false;

            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color");
            color.a = 150;
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);

            gameObject.tag = "CompletedStand";
        }
    }


}
