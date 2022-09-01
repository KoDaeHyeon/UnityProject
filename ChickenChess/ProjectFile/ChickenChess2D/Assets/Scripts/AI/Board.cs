using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject chickenpiece;
    public GameObject mat;

    public GameObject[,] positions = new GameObject[8, 8];
    private GameObject[,] matpositions = new GameObject[8, 8];

    List<Move> moves = new List<Move>();

    private string player;

    int pawn = 100;
    int rook = 500;
    int bishop = 500;
    int knight = 800;
    int queen = 900;
    int king = 999;


    private void OnDestroy()
    {
        Debug.Log("=======================Destroy!!======================");
    }

    public void brainLoad(Board board) //AI��꿡 �� ������ ���� ����
    {
        //Debug.Log("���󺸵����");
        chickenpiece = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().chickenpiece;
        mat = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().mat;
        GameObject pos = new GameObject();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //Debug.Log(i + "," + j);
                if (board.PositionOnBoard(i, j) && board.GetPosition(i, j) != null)
                {
                    if (board.GetPosition(i,j).GetComponent<Chickenman>() !=null)
                    {
                        pos = Create(board.positions[i, j].GetComponent<Chickenman>().name, board.positions[i, j].GetComponent<Chickenman>().GetXBoard(),
                            board.positions[i, j].GetComponent<Chickenman>().GetYBoard());
                        SetPosition(pos);
                    }

                    if(board.GetPosition(i,j).GetComponent<Mat>() != null)
                    {

                        pos = CreateMat(board.positions[i, j].GetComponent<Mat>().name, board.matpositions[i, j].GetComponent<Mat>().GetXBoard(),
                            board.matpositions[i, j].GetComponent<Mat>().GetYBoard());
                        SetPositionMat(pos);
                    }
   
                }

            }
        }
        Destroy(pos);
        chickenpiece= null;
        mat=null;
        Destroy(chickenpiece);
        Destroy(mat);

        //Debug.Log("���󺸵������");
    }

    public void brainStart() //AI��꿡 �� ������ ���� ����
    {
        //Debug.Log("���󺸵����");
        Game sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        chickenpiece = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().chickenpiece;
        mat = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().mat;
        GameObject pos = new GameObject();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //Debug.Log(i + "," + j);
                if (sc.PositionOnBoard(i, j) && sc.GetPosition(i, j) != null)
                {
                    if (sc.GetPosition(i, j).GetComponent<Chickenman>() != null)
                    {
                        pos = Create(sc.positions[i, j].GetComponent<Chickenman>().name, sc.positions[i, j].GetComponent<Chickenman>().GetXBoard(),
                            sc.positions[i, j].GetComponent<Chickenman>().GetYBoard());
                        SetPosition(pos);
                    }

                    if (sc.GetPosition(i, j).GetComponent<Mat>() != null)
                    {

                        pos = CreateMat(sc.positions[i, j].GetComponent<Mat>().name, sc.matpositions[i, j].GetComponent<Mat>().GetXBoard(),
                            sc.matpositions[i, j].GetComponent<Mat>().GetYBoard());
                        SetPositionMat(pos);
                    }

                }

            }
        }
        Destroy(pos);
        sc = null;
        chickenpiece = null;
        mat = null;
        Destroy(sc);
        Destroy(chickenpiece);
        Destroy(mat);

        //Debug.Log("���󺸵������");
    }

    public void DestroyAll()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(positions[i,j] !=null)
                Destroy(positions[i, j]);
                if (matpositions[i, j] != null)
                Destroy(positions[i, j]);
            }
        }
        

    }
    public GameObject CreateMat(string name, int x, int y)
    {
        GameObject obj = Instantiate(mat);
        Mat cm = obj.GetComponent<Mat>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        return obj;
    }

    public void SetPositionMat(GameObject obj)
    {
        Mat cm = obj.GetComponent<Mat>();

        matpositions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chickenpiece);
        Chickenman cm = obj.GetComponent<Chickenman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        return obj;
    }


    public bool PositionOnBoard(int x, int y) //x, y ���� Ǯ ��ġ
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public GameObject GetPositionMat(int x, int y)
    {
        return matpositions[x, y];
    }

    public void SetPosition(GameObject obj)
    {
        Chickenman cm = obj.GetComponent<Chickenman>();

        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }


    public void MoveMaker(int matrixX, int matrixY, int x, int y, bool attack) // AI�� ������ ���� ����
    {
        GameObject reference = GetPosition(x, y);
        //Debug.Log("���󺸵� movemaker : " + matrixX + "," +matrixY + " | " + x +"," +y);

            if (reference != null)
            {
                if (attack)
                {
                    GameObject cp = GetPosition(matrixX, matrixY);

                    //Debug.Log("Attack");

                    Destroy(cp);
                }

                 SetPositionEmpty(reference.GetComponent<Chickenman>().GetXBoard(),
                 reference.GetComponent<Chickenman>().GetYBoard());

                reference.GetComponent<Chickenman>().SetXBoard(matrixX);
                reference.GetComponent<Chickenman>().SetYBoard(matrixY);

                SetPosition(reference);

            }



    }


    public List<Move> scan(string p) //������� �� �̵����� ��ĵ
    {
        //Debug.Log("Scan ����");
        List<Move> moves = new List<Move>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject cm = GetPosition(i, j);
                if (cm != null)
                {
                    player = cm.GetComponent<Chickenman>().player;
                    if (player == p)
                    {
                        //Debug.Log("���� ���� ��ĵ �÷��̾� : " + i + "," + j+ " :" + player);
                        moves.AddRange(AImove(i, j));
                    }
                }
            }
        }
        return moves;
    }

    public List<Move> AImove(int i, int j)
    {
        List<Move> moves = new List<Move>();
        GameObject cm = GetPosition(i, j);
        if (cm != null)
        {
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
                    moves.AddRange(AIPawnMovePlate(i, j + 1, i, j));
                    break;
            }
        }

        return moves;
    }
    public List<Move> AILineMovePlate(int xIncrement, int yIncrement, int xBoard, int yBoard)
    {
        List<Move> moves = new List<Move>();
       int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;
        string player = GetPosition(xBoard, yBoard).GetComponent<Chickenman>().player;

        while ( PositionOnBoard(x, y) && GetPosition(x, y) == null)
        {
            Move ms = new Move();
            ms.setxy(x, y, xBoard, yBoard, true);
            MoveSocre(ms);
            moves.Add(ms);
            x += xIncrement;
            y += yIncrement;
        }
        if ( PositionOnBoard(x, y) && GetPosition(x, y).GetComponent<Chickenman>().player != player)
        {
            Move ms = new Move();
            ms.setxy(x, y, xBoard, yBoard,true);
            MoveSocre(ms);
            moves.Add(ms);
        }
        return moves;
    }

    public List<Move> AIRookMovePlate(int xIncrement, int yIncrement, int xBoard, int yBoard)
    {

        List<Move> moves = new List<Move>();
       GameObject mp;

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        string player = GetPosition(xBoard, yBoard).GetComponent<Chickenman>().player;

        while (PositionOnBoard(x, y) && GetPosition(x, y) == null)
        {
            mp = GetPositionMat(x, y);

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

        if (PositionOnBoard(x, y) && GetPosition(x, y) != null)
        {
            mp = GetPositionMat(x, y);
            if (mp == null)
            {
                if (PositionOnBoard(x, y) && GetPosition(x, y).GetComponent<Chickenman>().player != player)
                {
                    Move ms = new Move();
                    ms.setxy(x, y, xBoard, yBoard, true);
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
        if (PositionOnBoard(x, y))
        {
            GameObject cp = GetPosition(x, y);
            string player = GetPosition(xBoard, yBoard).GetComponent<Chickenman>().player;
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
        string player = GetPosition(xBoard, yBoard).GetComponent<Chickenman>().player;
        if (PositionOnBoard(x, y))
        {
            GameObject cp = GetPosition(x, y);
            GameObject mp = GetPositionMat(x, y);
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
        List<Move> moves = new List<Move>();
        string player = GetPosition(xBoard, yBoard).GetComponent<Chickenman>().player;
        if (PositionOnBoard(x, y))
        {
            string cm = GetPosition(xBoard, yBoard).GetComponent<Chickenman>().name;

            if (GetPosition(x, y) == null)
            {
                Move ms = new Move();
                ms.setxy(x, y, xBoard, yBoard);
                MoveSocre(ms);
                moves.Add(ms);
            }

            if (GetPosition(x, y) == null && cm == "white_pawn" && yBoard == 1)
            {
                if (GetPosition(x, y + 1) == null)
                {
                    Move ms = new Move();
                    ms.setxy(x, y + 1, xBoard, yBoard);
                    MoveSocre(ms);
                    moves.Add(ms);
                }

            }

            if (GetPosition(x, y) == null && cm == "black_pawn" && yBoard == 6)
            {
                if (GetPosition(x, y - 1) == null)
                {
                    Move ms = new Move();
                    ms.setxy(x, y - 1, xBoard, yBoard);
                    MoveSocre(ms);
                    moves.Add(ms);
                }
            }

            if (PositionOnBoard(x + 1, y) && GetPosition(x + 1, y) != null && GetPosition(x + 1, y).GetComponent<Chickenman>().player != player)
            {
                Move ms = new Move();
                ms.setxy(x + 1, y, xBoard, yBoard, true);
                MoveSocre(ms);
                moves.Add(ms);
            }
            if (PositionOnBoard(x - 1, y) && GetPosition(x - 1, y) != null && GetPosition(x - 1, y).GetComponent<Chickenman>().player != player)
            {
                Move ms = new Move();
                ms.setxy(x - 1, y, xBoard, yBoard, true);
                MoveSocre(ms);
                moves.Add(ms);
            }
        }
        return moves;
    }

    public bool LegalCheck()//AI�� ���󺸵� üũ����Ʈ �Ǵ�
    {
        List<Move> pseudoWhiteMoves = scan("white");
        //Debug.Log("üũ�Ǵ�  :  " + pseudoWhiteMoves.Count);
        foreach (Move moveToVerify in pseudoWhiteMoves)
        {
            GameObject cp = GetPosition(moveToVerify.x, moveToVerify.y);
            if (cp != null)
            {
                if (cp.name == "black_king")
                {
                    //Debug.Log("Board Legal");
                    return true;
                }
            }

        }

        return false;
    }

    public double CorrectComputerMove(Board board, int maxDepth, int currentDepth) //AI ���� �θƽ� ���
    {
        double score = 0;
        double bestscore = 0;
        //if (maxDepth <= currentDepth)
        //    return bestscore;
        List<Move> pseudoBlackMoves = scan("black");
        List<Move> pesduoWhiteMoves = scan("white");
        foreach (Move moveblack in pseudoBlackMoves)
        {
            Board b = new Board();
            b.brainLoad(board);
            b.MoveMaker(moveblack.x, moveblack.y, moveblack.removeX, moveblack.removeY, moveblack.attack);//AI ���� ���忡���� ������ ����
         //   score = CorrectComputerMove(b, maxDepth, currentDepth + 1);
            score += MoveSocre(moveblack);
            foreach (Move movewhite in pesduoWhiteMoves)
            {

                if (moveblack.removeX == movewhite.x && moveblack.removeY == movewhite.y)
                {
                    score += AttackScore(moveblack);
                    break;
                }

            }
            if (bestscore < score)
                bestscore = score;
            //Debug.Log("move : " + moveblack.removeX + "," + moveblack.removeY + " | " + moveblack.x + "," + moveblack.y + "  attack : " + moveblack.attack);

            b.DestroyAll();
            Destroy(b);

            }
            Debug.Log("score" + bestscore);
 
        return bestscore;
    }


    public double MoveSocre(Move m)
    {
        double score = 0;
        Chickenman me = GetPosition(m.removeX, m.removeY).GetComponent<Chickenman>();
        if (PositionOnBoard(m.x, m.y))
        {
            GameObject cp = GetPosition(m.x, m.y);
            if (cp != null)
            {
                switch (cp.GetComponent<Chickenman>().cname)
                {
                    case "pawn":
                        score += pawn;
                        if (m.attack && me.cname != "king") score -= chickenScore(me) / 2;
                        break;
                    case "rook":
                        score += rook;
                        if (m.attack && me.cname != "king") score -= chickenScore(me) / 2;
                        break;
                    case "bishop":
                        score += bishop;
                        if (m.attack && me.cname != "king") score -= chickenScore(me) / 2;
                        break;
                    case "knight":
                        score += knight;
                        if (m.attack && me.cname != "king") score -= chickenScore(me) / 2;
                        break;
                    case "queen":
                        score += queen;
                        if (m.attack && me.cname != "king") score -= chickenScore(me) / 2;
                        break;
                    case "king":
                        score += king;
                        if (m.attack && me.cname != "king") score -= chickenScore(me) / 2;
                        break;
                }
            }
        }
        return score;
    }

    public double AttackScore(Move m)
    {
        double score = 0;
        Chickenman me = GetPosition(m.removeX, m.removeY).GetComponent<Chickenman>();
        if (PositionOnBoard(m.x, m.y))
        {
            GameObject cp = GetPosition(m.x, m.y);
            if (cp != null)
            {
                switch (cp.GetComponent<Chickenman>().cname)
                {
                    case "pawn":
                        score -= pawn/2;
                        break;
                    case "rook":
                        score -= rook/2;
                        break;
                    case "bishop":
                        score -= bishop/2;
                        break;
                    case "knight":
                        score -= knight/2;
                        break;
                    case "queen":
                        score -= queen/2;
                        break;
                    case "king":
                        score -= king/2;
                        break;
                }
            }
        }
        return score;
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
