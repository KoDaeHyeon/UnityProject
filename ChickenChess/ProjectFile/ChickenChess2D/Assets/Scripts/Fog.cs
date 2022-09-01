using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //보드 포지션
    private int xBoard = -1;
    private int yBoard = -1;

    //false : 이동, true : 공격

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //tatke the instantiated location and adjust the transform
        SetCoords();

    }



    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -5.0f);
    }
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }



}
