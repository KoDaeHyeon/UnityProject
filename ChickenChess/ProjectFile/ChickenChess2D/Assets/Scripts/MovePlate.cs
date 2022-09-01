using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //보드 포지션
    int matrixX;
    int matrixY;

    //false : 이동, true : 공격
    public bool attack = false;



    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        int x = reference.GetComponent<Chickenman>().GetXBoard();
        int y = reference.GetComponent<Chickenman>().GetYBoard();
        int oldx = x;
        int oldy = y;
        int attackx;
        int attacky;
        string attackwhitetext="none";
        if (reference.GetComponent<Chickenman>().cname == "knight" )
        {

            if (attack)
            {
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

                if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
                if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");
                attackwhitetext = controller.GetComponent<Game>().GetPosition(matrixX, matrixY).GetComponent<Chickenman>().name;
                Destroy(cp);
            }

            attackx = matrixX;
            attacky = matrixY;

            controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chickenman>().GetXBoard(),
                 reference.GetComponent<Chickenman>().GetYBoard());

            reference.GetComponent<Chickenman>().SetXBoard(matrixX);
            reference.GetComponent<Chickenman>().SetYBoard(matrixY);
            reference.GetComponent<Chickenman>().SetCoords();

            controller.GetComponent<Game>().SetPosition(reference);

            controller.GetComponent<Game>().NextTurn();

            reference.GetComponent<Chickenman>().DestroyMovePlates();
        }
        else if(reference.GetComponent<Chickenman>().cname == "pawn")
        {
            if (attack)
            {
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

                if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
                if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");
                attackwhitetext = controller.GetComponent<Game>().GetPosition(matrixX, matrixY).GetComponent<Chickenman>().name;
                Destroy(cp);

            }
            else
            {
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
                if(cp!=null)
                {
                    if (reference.GetComponent<Chickenman>().name == "black_pawn")
                        matrixY = matrixY + 1;
                    if (reference.GetComponent<Chickenman>().name == "white_pawn")
                        matrixY = matrixY - 1;
                }
            }
            attackx = matrixX;
            attacky = matrixY;

            controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chickenman>().GetXBoard(),
                 reference.GetComponent<Chickenman>().GetYBoard());

            reference.GetComponent<Chickenman>().SetXBoard(matrixX);
            reference.GetComponent<Chickenman>().SetYBoard(matrixY);
            reference.GetComponent<Chickenman>().SetCoords();

            controller.GetComponent<Game>().SetPosition(reference);

            controller.GetComponent<Game>().NextTurn();

            reference.GetComponent<Chickenman>().DestroyMovePlates();
        }
        else
        {
            while (matrixY != y || matrixX != x)
            {

                if (reference.GetComponent<Chickenman>().cname == "rook")
                {
                    if (x == matrixX && y > matrixY && reference.GetComponent<Chickenman>().MatyCheck(x, y - 1))
                    {
                        pieceDeploy(x, y);
                        break;
                    } else if (x == matrixX && y < matrixY && reference.GetComponent<Chickenman>().MatyCheck(x, y + 1))
                    {
                        pieceDeploy(x, y);
                        break;
                    }
                    else if (x > matrixX && y == matrixY && reference.GetComponent<Chickenman>().MatyCheck(x - 1, y))
                    {
                        pieceDeploy(x, y);
                        break;
                    }
                    else if (x < matrixX && y == matrixY && reference.GetComponent<Chickenman>().MatyCheck(x + 1, y))
                    {
                        pieceDeploy(x, y);
                        break;
                    }

                }
                if (y == matrixY) y = matrixY;
                else if (y > matrixY) y--;
                else y++;
                if (x == matrixX) x = matrixX;
                else if (x > matrixX) x--;
                else x++;
                if (reference.GetComponent<Chickenman>().enenmyCheck(x, y))
                {
                    GameObject cp = controller.GetComponent<Game>().GetPosition(x, y);
                    if (cp.name == "white_king") { controller.GetComponent<Game>().Winner("black"); }
                    if (cp.name == "black_king") { controller.GetComponent<Game>().Winner("white"); }

                    if(controller.GetComponent<Game>().GetPosition(x, y) != null)
                    attackwhitetext = controller.GetComponent<Game>().GetPosition(x, y).GetComponent<Chickenman>().name;

                    Destroy(cp);

                    controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chickenman>().GetXBoard(),
                     reference.GetComponent<Chickenman>().GetYBoard());

                    reference.GetComponent<Chickenman>().SetXBoard(x);
                    reference.GetComponent<Chickenman>().SetYBoard(y);
                    reference.GetComponent<Chickenman>().SetCoords();

                    controller.GetComponent<Game>().SetPosition(reference);
                    controller.GetComponent<Game>().NextTurn();


                    reference.GetComponent<Chickenman>().DestroyMovePlates();

                    break;
                }
                if (y == matrixY && x == matrixX)
                {
                    controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chickenman>().GetXBoard(),
                         reference.GetComponent<Chickenman>().GetYBoard());

                    reference.GetComponent<Chickenman>().SetXBoard(x);
                    reference.GetComponent<Chickenman>().SetYBoard(y);
                    reference.GetComponent<Chickenman>().SetCoords();

                    controller.GetComponent<Game>().SetPosition(reference);
                    controller.GetComponent<Game>().NextTurn();

                    reference.GetComponent<Chickenman>().DestroyMovePlates();
                }
            }
            attackx = x;
            attacky = y;
        }

        string whitetext = oldx + "|" + oldy + " ->" + attackx + "|" + attacky;
        GameObject.Find("TestWhite").GetComponent<Text>().text = whitetext;


        GameObject.Find("WhiteAttack").GetComponent<Text>().text = attackwhitetext;



    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }

    public void pieceDeploy(int x, int y)
    {
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chickenman>().GetXBoard(),
                         reference.GetComponent<Chickenman>().GetYBoard());

        reference.GetComponent<Chickenman>().SetXBoard(x);
        reference.GetComponent<Chickenman>().SetYBoard(y);
        reference.GetComponent<Chickenman>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);
        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<Chickenman>().DestroyMovePlates();
    }
}
