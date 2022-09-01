using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDraughts : MonoBehaviour
{

    public bool attack = false;
    public string player="black";

    public GameObject controller;

    GameObject reference = null;

    int pawn=100;
    int rook=500;
    int bishop=300;
    int knight=300;
    int queen=900;
    int king=999;

    public List<Move> AImove(int i, int j)
    {
        List<Move> moves = new List<Move>();
        GameObject cm = controller.GetComponent<Game>().GetPosition(i, j);
        switch (cm.GetComponent<Chickenman>().name)
        {
            case "black_queen":
            case "white_queen":
                moves.AddRange(AILineMovePlate(1, 0, i, j));
                moves.AddRange(AILineMovePlate(0, 1, i, j));
                moves.AddRange(AILineMovePlate(1, 1, i, j));
                moves.AddRange(AILineMovePlate(-1, 0, i, j));
                moves.AddRange(AILineMovePlate(0, -1, i, j));
                moves.AddRange(AILineMovePlate(-1, -1, i, j));
                moves.AddRange(AILineMovePlate(-1, 1, i, j));
                moves.AddRange(AILineMovePlate(1, -1, i, j));
                break;
            case "black_knight":
            case "white_knight":
                moves.AddRange(AIKnightMovePlate(i, j));
                break;
            case "black_bishop":
            case "white_bishop":
                moves.AddRange(AILineMovePlate(1, 1, i, j));
                moves.AddRange(AILineMovePlate(1, -1, i, j));
                moves.AddRange(AILineMovePlate(-1, 1, i, j));
                moves.AddRange(AILineMovePlate(-1, -1, i, j));
                break;
            case "black_king":
            case "white_king":
                moves.AddRange(AISurroundMovePlate(i, j));
                break;
            case "black_rook":
            case "white_rook":
                moves.AddRange(AIRookMovePlate(1, 0, i, j));
                moves.AddRange(AIRookMovePlate(0, 1, i, j));
                moves.AddRange(AIRookMovePlate(-1, 0, i, j));
                moves.AddRange(AIRookMovePlate(0, -1, i, j));
                break;
                
            case "black_pawn":
                moves.AddRange(AIPawnMovePlate(i, j - 1, i, j));
                break;
            case "white_pawn":
                moves.AddRange(AIPawnMovePlate(i, j + 1, i ,j));
                break;
        }

        return moves;
    }

    public List<Move> AILineMovePlate(int xIncrement, int yIncrement, int xBoard, int yBoard)
    {
        Game sc = controller.GetComponent<Game>();

        List<Move> moves=new List<Move>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            Move ms = new Move();
            ms.setxy(x, y, xBoard, yBoard);
            MoveSocre(ms);
            moves.Add(ms);
            x += xIncrement;
            y += yIncrement;
        }
        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chickenman>().player != player)
        {
            Move ms = new Move();
            ms.setxy(x, y, xBoard, yBoard, true);
            MoveSocre(ms);
            moves.Add(ms);
        }

        return moves;

    }

    public List<Move> AIRookMovePlate(int xIncrement, int yIncrement, int xBoard, int yBoard)
    {
        Game sc = controller.GetComponent<Game>();
        GameObject mp;

        List<Move> moves = new List<Move>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            mp = sc.GetPositionMat(x, y);

            if (mp == null)
            {
                Move ms = new Move();
                ms.setxy(x, y, xBoard, yBoard);
                MoveSocre(ms);
                moves.Add(ms);
                x += xIncrement;
                y += yIncrement;
            }
            else
            {
                if (mp.GetComponent<Mat>().matname != "river")
                {
                    Move ms = new Move();
                    ms.setxy(x, y, xBoard, yBoard);
                    MoveSocre(ms);
                    moves.Add(ms);
                    x += xIncrement;
                    y += yIncrement;
                }
                else
                {
                    break;
                }
            }
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) != null)
        {
            mp = sc.GetPositionMat(x, y);
            if (mp == null)
            {
                if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chickenman>().player != player)
                {
                    Move ms = new Move();
                    ms.setxy(x, y, xBoard, yBoard,true);
                    MoveSocre(ms);
                    moves.Add(ms);
                }
            }
            else
            {
                if (mp.GetComponent<Mat>().matname != "river")
                {
                    Move ms = new Move();
                    ms.setxy(x, y, xBoard, yBoard, true);
                    MoveSocre(ms);
                    moves.Add(ms);
                }
            }
        }
        return moves;
    }

    public List<Move> AIKnightMovePlate(int xBoard, int yBoard)
    {
        List<Move> moves = new List<Move>();
        moves.AddRange(AIKnightPointMovePlate(xBoard + 1, yBoard + 2, xBoard, yBoard));
        moves.AddRange(AIKnightPointMovePlate(xBoard - 1, yBoard + 2, xBoard, yBoard));
        moves.AddRange(AIKnightPointMovePlate(xBoard + 2, yBoard + 1, xBoard, yBoard));
        moves.AddRange(AIKnightPointMovePlate(xBoard + 2, yBoard - 1, xBoard, yBoard));
        moves.AddRange(AIKnightPointMovePlate(xBoard + 1, yBoard - 2, xBoard, yBoard));
        moves.AddRange(AIKnightPointMovePlate(xBoard - 1, yBoard - 2, xBoard, yBoard));
        moves.AddRange(AIKnightPointMovePlate(xBoard - 2, yBoard + 1, xBoard, yBoard));
        moves.AddRange(AIKnightPointMovePlate(xBoard - 2, yBoard - 1, xBoard, yBoard));
        return moves;
    }

    public List<Move> AISurroundMovePlate(int xBoard, int yBoard)
    {
        List<Move> moves = new List<Move>();
        moves.AddRange(AIPointMovePlate(xBoard, yBoard + 1, xBoard, yBoard));
        moves.AddRange(AIPointMovePlate(xBoard, yBoard - 1, xBoard, yBoard));
        moves.AddRange(AIPointMovePlate(xBoard - 1, yBoard - 1, xBoard, yBoard));
        moves.AddRange(AIPointMovePlate(xBoard - 1, yBoard - 0, xBoard, yBoard));
        moves.AddRange(AIPointMovePlate(xBoard - 1, yBoard + 1, xBoard, yBoard));
        moves.AddRange(AIPointMovePlate(xBoard + 1, yBoard - 1, xBoard, yBoard));
        moves.AddRange(AIPointMovePlate(xBoard + 1, yBoard - 0, xBoard, yBoard));
        moves.AddRange(AIPointMovePlate(xBoard + 1, yBoard + 1, xBoard, yBoard));
        return moves;
    }

    public List<Move> AIPointMovePlate(int x, int y, int xBoard, int yBoard)
    {
        List<Move> moves = new List<Move>();

        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                Move ms = new Move();
                ms.setxy(x, y, xBoard, yBoard);
                MoveSocre(ms);
                moves.Add(ms);
            }
            else if (cp.GetComponent<Chickenman>().player != player)
            {
                Move ms = new Move();
                ms.setxy(x, y, xBoard, yBoard, true);
                MoveSocre(ms);
                moves.Add(ms);
            }
        }
        return moves;
    }

    public List<Move> AIKnightPointMovePlate(int x, int y, int xBoard, int yBoard)
    {
        List<Move> moves = new List<Move>();
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            GameObject mp = sc.GetPositionMat(x, y);
            if (cp == null)
            {
                if (mp == null)
                {
                    Move ms = new Move();
                    ms.setxy(x, y, xBoard, yBoard);
                    MoveSocre(ms);
                    moves.Add(ms);
                    
                }
                else if (mp.GetComponent<Mat>().matname != "mountain")
                {
                    Move ms = new Move();
                    ms.setxy(x, y, xBoard, yBoard);
                    MoveSocre(ms);
                    moves.Add(ms);
                }

            }
            else if (cp.GetComponent<Chickenman>().player != player)
            {
                if (mp == null)
                {
                    Move ms = new Move();
                    ms.setxy(x, y, xBoard, yBoard, true);
                    MoveSocre(ms);
                    moves.Add(ms);
                }
                else if (mp.GetComponent<Mat>().matname != "mountain")
                {
                    Move ms = new Move();
                    ms.setxy(x, y, xBoard, yBoard, true);
                    MoveSocre(ms);
                    moves.Add(ms);
                }
            }

        }
        return moves;
    }

    public List<Move> AIPawnMovePlate(int x, int y, int xBoard, int yBoard)
    {
        Game sc = controller.GetComponent<Game>();
        List<Move> moves = new List<Move>();
        if (sc.PositionOnBoard(x, y))
        {
            string cm = sc.GetPosition(xBoard, yBoard).GetComponent<Chickenman>().name;

            if (sc.GetPosition(x, y) == null)
            {
                Move ms = new Move();
                ms.setxy(x, y, xBoard, yBoard);
                MoveSocre(ms);
                moves.Add(ms);
            }

            if (sc.GetPosition(x, y) == null && cm == "white_pawn" && yBoard == 1)
            {
                if (sc.GetPosition(x, y+1) == null)
                {
                    Move ms = new Move();
                    ms.setxy(x, y+1, xBoard, yBoard);
                    MoveSocre(ms);
                    moves.Add(ms);
                }

            }

            if (sc.GetPosition(x, y) == null && cm == "black_pawn" && yBoard == 6)
            {
                if (sc.GetPosition(x, y-1) == null)
                {
                    Move ms = new Move();
                    ms.setxy(x, y-1, xBoard, yBoard);
                    MoveSocre(ms);
                    moves.Add(ms);
                }
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chickenman>().player != player)
            {
                Move ms = new Move();
                ms.setxy(x+1, y, xBoard, yBoard, true);
                MoveSocre(ms);
                moves.Add(ms);
            }
            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chickenman>().player != player)
            {
                Move ms = new Move();
                ms.setxy(x-1, y, xBoard, yBoard, true);
                MoveSocre(ms);
                moves.Add(ms);
            }
        }

        return moves;
    }

    public void MoveMaker(int matrixX, int matrixY, int x, int y, bool attack)
    {
            Debug.Log("행동 실행" + x + "," + y + " | " + matrixX + "," + matrixY + " , " + attack);
            controller = GameObject.FindGameObjectWithTag("GameController");
            reference = controller.GetComponent<Game>().GetPosition(x, y);
        if (reference != null)
        {
            if (attack)
            {
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
                Debug.Log("공격대상" + cp.name);
                //Debug.Log("Attack");
                if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
                if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");
                Destroy(cp);
            }

            controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chickenman>().GetXBoard(),
                 reference.GetComponent<Chickenman>().GetYBoard());

            reference.GetComponent<Chickenman>().SetXBoard(matrixX);
            reference.GetComponent<Chickenman>().SetYBoard(matrixY);
            reference.GetComponent<Chickenman>().SetCoords();
            controller.GetComponent<Game>().SetPosition(reference);
            controller.GetComponent<Game>().NextTurn();
        }

    }

    public List<Move> scan(string p) //보드상의 말 이동범위 스캔
    {
        //Debug.Log("Scan 진입");
        List<Move> moves = new List<Move>();
        controller = GameObject.FindGameObjectWithTag("GameController");
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject cm = controller.GetComponent<Game>().GetPosition(i, j);
                if (cm != null)
                {
                    player =cm.GetComponent<Chickenman>().player;
                    if (player == p) 
                    {
                        //Debug.Log("스캔 플레이어 : " + i + "," + j+ " :" + player);
                        moves.AddRange(AImove(i, j)); 
                    }
                }
            }
        }
        return moves;
    }
    public void ChooseComputerMove() //컴퓨터 움직임
    {
        Move ms = new Move();



            ms = CorrectComputerMove();


        Debug.Log("결정 행동" + ms.removeX + "," + ms.removeY + " | " + ms.x + "," + ms.y + "  || " + ms.score);

        string blacktext = ms.removeX.ToString() + "|" + ms.removeY.ToString() +" ->" + ms.x.ToString() +"|" +ms.y.ToString();
        GameObject.FindWithTag("Test").GetComponent<Text>().text = blacktext;

        if (controller.GetComponent<Game>().GetPosition(ms.x, ms.y) != null)
        {
            string Attackblacktext = controller.GetComponent<Game>().GetPosition(ms.x, ms.y).GetComponent<Chickenman>().name;
            GameObject.Find("BlackAttack").GetComponent<Text>().text = Attackblacktext;
        }
        else
        {
            string Attackblacktext = "none";
            GameObject.Find("BlackAttack").GetComponent<Text>().text = Attackblacktext;
        }

        MoveMaker(ms.x, ms.y, ms.removeX, ms.removeY, ms.attack);
    }

    public bool LegalCheck()//AI의 체크메이트 판단
    {
        List<Move> pseudoWhiteMoves = scan("white");
        //Debug.Log("체크판단  :  " + pseudoWhiteMoves.Count);
        foreach (Move moveToVerify in pseudoWhiteMoves)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(moveToVerify.x, moveToVerify.y);
            if (cp != null)
            {
                if (cp.name == "black_king")
                {
                    
                    return true;
                }
            }

        }
        return false;
    }

    Move CorrectComputerMove() //AI 점수 계산
    {
        List<Move> pseudoBlackMoves = scan("black");
        List<Move> pesduoWhiteMoves = scan("white");
        List<Move> MoveList = new List<Move>();
        Move correctMove = new Move();
            foreach (Move moveblack in pseudoBlackMoves)
            {
                Board board = new Board();
                board.brainStart(); //AI 가상 보드 시작
                board.MoveMaker(moveblack.x, moveblack.y, moveblack.removeX, moveblack.removeY, moveblack.attack);//AI 가상 보드에서의 움직임 적용
                Debug.Log("move : " + moveblack.removeX + "," + moveblack.removeY + " | " + moveblack.x + "," + moveblack.y + "  attack : " + moveblack.attack);
                if (!board.LegalCheck() ) // 가상보드에 체크가 없으면 저장(체크 방어)
                {
                    //Debug.Log("Legal pass move : " + moveblack.removeX + ","+ moveblack.removeY + " | " + moveblack.x + "," + moveblack.y + "  attack : " + moveblack.attack);
                    moveblack.score += board.CorrectComputerMove(board, 2, 0);
                    MoveList.Add(moveblack);
                }

                board.DestroyAll();

                Destroy(board);
            
            }

        /*Debug.Log("Count" + legalMoves.Count);
        for(int i=0; i<legalMoves.Count; i++)
        {
            Debug.Log("legaMove" + legalMoves[i].removeX + "," + legalMoves[i].removeY + " | " + legalMoves[i].x + "," + legalMoves[i].y + "  attack : " + legalMoves[i].attack);
        }*/
        if (MoveList.Count == 0)
        {
            correctMove = pseudoBlackMoves[Random.Range(0, pseudoBlackMoves.Count)];
            Debug.Log("Random!!!!");
        }
        else
        {
            foreach (Move moveblack in MoveList)
            {
                if (moveblack.score > correctMove.score)
                {
                    correctMove = moveblack;
                }
            }
            if (correctMove.score == -9999 || correctMove.score == 0)
            {
                correctMove = MoveList[Random.Range(0, MoveList.Count)];
            }
        }



        return correctMove;
    }

    public void MoveSocre(Move m)
    {
        if(m.attack)    m.score = 0;
        Game sc = controller.GetComponent<Game>();
        Chickenman me = sc.GetPosition(m.removeX, m.removeY).GetComponent<Chickenman>();
        if (sc.PositionOnBoard(m.x, m.y))
        {
            GameObject cp = sc.GetPosition(m.x, m.y);
            if(cp != null)
            {
                switch(cp.GetComponent<Chickenman>().cname)
                {
                    case "pawn":
                        m.score = m.score + pawn*2;
                        if (m.attack && me.cname != "king") m.score -= chickenScore(me)/2;
                        break;
                    case "rook":
                        m.score = m.score + rook * 2;
                        if (m.attack && me.cname != "king") m.score -= chickenScore(me)/2;
                        break;
                    case "bishop":
                        m.score = m.score + bishop * 2;
                        if (m.attack && me.cname != "king") m.score -= chickenScore(me)/2;
                        break;
                    case "knight":
                        m.score = m.score + knight * 2;
                        if (m.attack && me.cname != "king") m.score -= chickenScore(me)/2;
                        break;
                    case "queen":
                        m.score = m.score + queen * 2;
                        if (m.attack && me.cname != "king") m.score -= chickenScore(me)/2;
                        break;
                    case "king":
                        m.score = m.score + king * 2;
                        if (m.attack && me.cname != "king") m.score -= chickenScore(me)/2;
                        break;
                }
            }
        }
    }

    public void AttackScore(Move m)
    {
        if (m.attack) m.score = 0;
        Game sc = controller.GetComponent<Game>();
        Chickenman me = sc.GetPosition(m.removeX, m.removeY).GetComponent<Chickenman>();
        if (sc.PositionOnBoard(m.x, m.y))
        {
            GameObject cp = sc.GetPosition(m.x, m.y);
            if (cp != null)
            {
                switch (cp.GetComponent<Chickenman>().cname)
                {
                    case "pawn":
                        m.score -= pawn / 2;
                        break;
                    case "rook":
                        m.score -= rook / 2;
                        break;
                    case "bishop":
                        m.score -= bishop / 2;
                        break;
                    case "knight":
                        m.score -= knight / 2;
                        break;
                    case "queen":
                        m.score -= queen / 2;
                        break;
                    case "king":
                        m.score -= king / 2;
                        break;
                }
            }

        }
    }

    public int chickenScore(Chickenman ck)
    {
        int score = 0;
        switch (ck.cname)
        {
            case "pawn":
                score = pawn;
                break;
            case "rook":
                score = rook;
                break;
            case "bishop":
                score = bishop;
                break;
            case "knight":
                score = knight;
                break;
            case "queen":
                score = queen;
                break;
            case "king":
                score = king;
                break;
        }

        return score;
    }
}
