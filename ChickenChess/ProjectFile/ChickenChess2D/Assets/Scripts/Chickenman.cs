using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chickenman : MonoBehaviour
{
    //레퍼런스
    public GameObject controller;
    public GameObject movePlate;


    //포지션
    private int xBoard = -1;
    private int yBoard = -1;

    //플레이어 트랙
    public string player = "black";

    public string cname;

    //References for all the sprites that the chesspiece can be
    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;


    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //tatke the instantiated location and adjust the transform
        SetCoords();

        switch (this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; cname = "queen"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; cname = "knight"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; cname = "bishop"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; cname = "king"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; cname = "rook"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; cname = "pawn"; break;

            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; cname = "queen"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; cname = "knight"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; cname = "bishop"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; cname = "king"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; cname = "rook"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; cname = "pawn"; break;



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

        this.transform.position = new Vector3(x, y, -2.0f);
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

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                KnightMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                RookMovePlate(1, 0);
                RookMovePlate(0, 1);
                RookMovePlate(-1, 0);
                RookMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while ((sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null) || (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chickenman>().player != player))
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

    }

    public void RookMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;
        while ((sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null) || (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chickenman>().player != player))
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

    }

    public void KnightMovePlate()
    {
        KnightPointMovePlate(xBoard + 1, yBoard + 2);
        KnightPointMovePlate(xBoard - 1, yBoard + 2);
        KnightPointMovePlate(xBoard + 2, yBoard + 1);
        KnightPointMovePlate(xBoard + 2, yBoard - 1);
        KnightPointMovePlate(xBoard + 1, yBoard - 2);
        KnightPointMovePlate(xBoard - 1, yBoard - 2);
        KnightPointMovePlate(xBoard - 2, yBoard + 1);
        KnightPointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chickenman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void KnightPointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            GameObject mp = sc.GetPositionMat(x, y);
            if (cp == null)
            {
                if (mp == null)
                {
                    MovePlateSpawn(x, y);
                }
                else if (mp.GetComponent<Mat>().matname != "mountain")
                {
                    MovePlateSpawn(x, y);
                }

            }
            else if (cp.GetComponent<Chickenman>().player != player)
            {
                if (mp == null)
                {
                    MovePlateAttackSpawn(x, y);
                }
                else if (mp.GetComponent<Mat>().matname != "mountain")
                {
                    MovePlateAttackSpawn(x, y);
                }
            }

        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if ((sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null) || (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chickenman>().player != player))
            {
                MovePlateSpawn(x, y);
            }

            if (this.name == "white_pawn" && y == 2)
                if((sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null) || (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chickenman>().player != player))
                MovePlateSpawn(x,y + 1);

            if (this.name == "black_pawn" && y == 5)
                if((sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null) || (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chickenman>().player != player))
                MovePlateSpawn(x, y - 1);

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chickenman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chickenman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -8.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -5.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public bool enenmyCheck(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.GetPosition(x, y) == null) return false;
        else return sc.GetPosition(x, y).GetComponent<Chickenman>().player != player;
    }

    public bool MatyCheck(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.GetPositionMat(x, y) == null) return false;
        else return sc.GetPositionMat(x, y).GetComponent<Mat>().matname == "river";
    }





}

