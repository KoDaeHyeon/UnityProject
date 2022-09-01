using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat : MonoBehaviour
{
    //레퍼런스
    public GameObject controller;
    public GameObject movePlate;


    //포지션
    private int xBoard = -1;
    private int yBoard = -1;

    //플레이어 트랙
    private string mat;
    public string matname;
   
    //References for all the sprites that the chesspiece can be
    public Sprite mountain;
    public Sprite river;
    public Sprite grass;
    public Sprite tower;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //tatke the instantiated location and adjust the transform
        SetCoords();

        switch (this.name)
        {
            case "mountain": this.GetComponent<SpriteRenderer>().sprite = mountain; matname = "mountain"; break;
            case "river": this.GetComponent<SpriteRenderer>().sprite = river; matname = "river"; break;
            case "grass": this.GetComponent<SpriteRenderer>().sprite = grass; matname = "grass"; break;
            case "tower": this.GetComponent<SpriteRenderer>().sprite = tower; matname = "tower"; break;
        }

    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        if (this.name == "mountain")
        {
            this.transform.position = new Vector3(x, y, -7.0f);
        }else if (this.name == "grass")
        {
            this.transform.position = new Vector3(x, y, -3.0f);
        }
        else
        {
            this.transform.position = new Vector3(x, y, -1.0f);
        }

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
