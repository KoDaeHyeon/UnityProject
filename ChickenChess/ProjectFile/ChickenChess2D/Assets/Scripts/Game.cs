using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public GameObject chickenpiece;
    public GameObject mat;
    public GameObject fog;

    public GameObject[,] positions = new GameObject[8, 8];
    public GameObject[] playerBlack = new GameObject[16];
    public GameObject[] playerWhite = new GameObject[16];
    public GameObject[] matDeploy = new GameObject[7];
    public GameObject[,] matpositions = new GameObject[8,8];

    private GameObject[] fogDeploy = new GameObject[64];
    private GameObject[,] fogpositions = new GameObject[8, 8];
    private GameObject refog;

    System.Random rand = new System.Random();
    private List<int> matx = new List<int>();
    private List<int> maty = new List<int>();

    private string currentPlayer = "white";

    public GameObject forDestroy;

    private bool gameOver = false;

    MoveDraughts md= new MoveDraughts();

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("white_rook",0,0),Create("white_knight",1,0),Create("white_bishop",2,0),Create("white_queen",3,0),
            Create("white_king",4,0),Create("white_bishop",5,0),Create("white_knight",6,0),Create("white_rook",7,0),Create("white_pawn",0,1),
            Create("white_pawn",1,1),Create("white_pawn",2,1),Create("white_pawn",3,1),Create("white_pawn",4,1),Create("white_pawn",5,1),
            Create("white_pawn",6,1),Create("white_pawn",7,1)
        };
        playerBlack = new GameObject[]
        {
            Create("black_rook",0,7),Create("black_knight",1,7),Create("black_bishop",2,7),Create("black_queen",3,7),
            Create("black_king",4,7),Create("black_bishop",5,7),Create("black_knight",6,7),Create("black_rook",7,7),Create("black_pawn",0,6),
            Create("black_pawn",1,6),Create("black_pawn",2,6),Create("black_pawn",3,6),Create("black_pawn",4,6),Create("black_pawn",5,6),
            Create("black_pawn",6,6),Create("black_pawn",7,6)
        };

        for(int i=0;i<matDeploy.Length;i++) matrandom(); //지형 개수만큼 난수 생성

        matDeploy = new GameObject[]
        {
            CreateMat("mountain",matx[0],maty[0]),CreateMat("mountain",matx[1],maty[1]),
            CreateMat("river",matx[2],maty[2]),CreateMat("river",matx[3],maty[3]),
            CreateMat("grass",matx[4],maty[4]),CreateMat("grass",matx[5],maty[5]),
            CreateMat("tower",matx[6],maty[6])
        };

        fogDeploy = new GameObject[]
        {
            CreateFog("fog",0,0),CreateFog("fog",1,0),CreateFog("fog",2,0),CreateFog("fog",3,0),
            CreateFog("fog",4,0),CreateFog("fog",5,0),CreateFog("fog",6,0),CreateFog("fog",7,0),
            CreateFog("fog",0,1),CreateFog("fog",1,1),CreateFog("fog",2,1),CreateFog("fog",3,1),
            CreateFog("fog",4,1),CreateFog("fog",5,1),CreateFog("fog",6,1),CreateFog("fog",7,1),
            CreateFog("fog",0,2),CreateFog("fog",1,2),CreateFog("fog",2,2),CreateFog("fog",3,2),
            CreateFog("fog",4,2),CreateFog("fog",5,2),CreateFog("fog",6,2),CreateFog("fog",7,2),
            CreateFog("fog",0,3),CreateFog("fog",1,3),CreateFog("fog",2,3),CreateFog("fog",3,3),
            CreateFog("fog",4,3),CreateFog("fog",5,3),CreateFog("fog",6,3),CreateFog("fog",7,3),
            CreateFog("fog",0,4),CreateFog("fog",1,4),CreateFog("fog",2,4),CreateFog("fog",3,4),
            CreateFog("fog",4,4),CreateFog("fog",5,4),CreateFog("fog",6,4),CreateFog("fog",7,4),
            CreateFog("fog",0,5),CreateFog("fog",1,5),CreateFog("fog",2,5),CreateFog("fog",3,5),
            CreateFog("fog",4,5),CreateFog("fog",5,5),CreateFog("fog",6,5),CreateFog("fog",7,5),
            CreateFog("fog",0,6),CreateFog("fog",1,6),CreateFog("fog",2,6),CreateFog("fog",3,6),
            CreateFog("fog",4,6),CreateFog("fog",5,6),CreateFog("fog",6,6),CreateFog("fog",7,6),
            CreateFog("fog",0,7),CreateFog("fog",1,7),CreateFog("fog",2,7),CreateFog("fog",3,7),
            CreateFog("fog",4,7),CreateFog("fog",5,7),CreateFog("fog",6,7),CreateFog("fog",7,7)
        };


        // Set all piece positions on the position board
        for (int i = 0; i< playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }

        for (int i = 0; i < matDeploy.Length; i++)
        {
            SetPositionMat(matDeploy[i]);
        }

        for (int i = 0; i < fogDeploy.Length; i++)
        {
            SetPositionFog(fogDeploy[i]);
        }


        white_fog();
    

    }

    private void setFogDeploy()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++) 
            {
                if(GetPositionFog(x,y) == null) 
                {
                    refog = CreateFog("fog", x, y);
                    SetPositionFog(refog);

                }
            }
        }
    }

    public void clearFog()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                DestroyFog(x, y);

            }
        }
    }

    public void matrandom()
    {
        int randmatx = rand.Next(0, 8);
        int randmaty = rand.Next(2, 6);
        if (matx.Count == 0) //처음 난수 생성시
        {
            matx.Add(randmatx);
            maty.Add(randmaty);
        }
        else // 두번부터 난수 생성시
        {
            bool over = true; //좌표 중복 검사
            while (over == true)
            {
                if (matx.Contains(randmatx) && maty.Contains(randmaty))
                {
                    randmatx = rand.Next(0, 7);
                    randmaty = rand.Next(2, 5);
                    over = true;
                }
                else
                {
                    matx.Add(randmatx);
                    maty.Add(randmaty);
                    over = false;
                    break;
                }
            }
        }
    }


    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chickenpiece, new Vector3(0, 0, -2), Quaternion.identity);
        Chickenman cm = obj.GetComponent<Chickenman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        return obj;
    }

    public GameObject CreateMat(string name, int x, int y)
    {
        GameObject obj = Instantiate(mat, new Vector3(0, 0, -2), Quaternion.identity);
        Mat cm = obj.GetComponent<Mat> ();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        return obj;
    }

    public GameObject CreateFog(string name, int x, int y)
    {
        GameObject obj = Instantiate(fog, new Vector3(0, 0, -5), Quaternion.identity);
        Fog cm = obj.GetComponent<Fog>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chickenman cm = obj.GetComponent<Chickenman>();

        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionMat(GameObject obj)
    {
        Mat cm = obj.GetComponent<Mat>();

        matpositions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionFog(GameObject obj)
    {
        Fog cm = obj.GetComponent<Fog>();

        fogpositions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }
    public void SetPositionFogEmpty(int x, int y)
    {
        fogpositions[x, y] = null;
    }
    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public GameObject GetPositionMat(int x, int y)
    {
        return matpositions[x, y];
    }

    public GameObject GetPositionFog(int x, int y)
    {
        return fogpositions[x, y];
    }

    public bool PositionOnBoard(int x, int y) //x, y 공개 풀 위치
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (!gameOver)
        {
            if (currentPlayer == "white")
            {
                currentPlayer = "black";
                md.ChooseComputerMove();
            }
            else
            {
                currentPlayer = "white";
            }
        }
        
    }

    public void Update()
    {
        forDestroy = GameObject.Find("New Game Object");
        if ( forDestroy != null){
            Destroy(forDestroy);
        }
        if (!gameOver)
        {

            setFogDeploy();
            white_fog();
            //clearFog();
            GameObject.FindWithTag("WinnerText").GetComponent<Text>().text = currentPlayer;
        }
        else
        {
            clearFog();
        }
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }

        
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        GameObject.FindWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindWithTag("WinnerText").GetComponent<Text>().text=playerWinner+" is Winner";

        GameObject.FindWithTag("StartText").GetComponent<Text>().enabled = true;
    }
    
   

    public void white_fog()
    {
        for (int i = 0; i < playerWhite.Length; i++)
        {
            if (playerWhite[i] != null)
            {
                int fogx = playerWhite[i].GetComponent<Chickenman>().GetXBoard();
                int fogy = playerWhite[i].GetComponent<Chickenman>().GetYBoard();
                switch (playerWhite[i].name)
                {
                    case "white_queen":
                        whitequeenFogDestroy(fogx, fogy);
                        break;
                    case "white_knight":
                        whiteknightFogDestroy(fogx, fogy);
                        break;
                    case "white_bishop":
                        whitebishopFogDestroy(fogx, fogy);
                        break;
                    case "white_king":
                        whitekingFogDestroy(fogx, fogy);
                        break;
                    case "white_rook":
                        whiterookFogDestroy(fogx, fogy);
                        break;
                    case "white_pawn":
                        whitepawnFogDestroy(fogx, fogy);
                        break;
                }
            }
        }
    }
    private void DestroyFog(int x, int y)
    {
        if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
        {
            if (!(GetPositionFog(x, y) == null))
            {
                Destroy(GetPositionFog(x, y));
                SetPositionFogEmpty(x, y);
            }
        }
    }

    private void towerfogDetroy(int x, int y)
    {
        DestroyFog(x-2, y+1);
        DestroyFog(x-2, y);
        DestroyFog(x-2, y-1);
        DestroyFog(x-1, y+2);
        DestroyFog(x-1, y+1);
        DestroyFog(x-1, y);
        DestroyFog(x-1, y-1);
        DestroyFog(x-1, y-2);
        DestroyFog(x, y+2);
        DestroyFog(x, y+1);
        DestroyFog(x, y-1);
        DestroyFog(x, y-2);
        DestroyFog(x+1, y+2);
        DestroyFog(x+1, y+1);
        DestroyFog(x+1, y);
        DestroyFog(x+1, y-1);
        DestroyFog(x+1, y-2);
        DestroyFog(x+2, y+1);
        DestroyFog(x+2, y);
        DestroyFog(x+2, y-1);
    }

    private void whitepawnFogDestroy(int x, int y)
    {
        if(x==matx[6]&& y == maty[6])
        {
            towerfogDetroy(x, y);
        }
        if ((x == matx[4] && y == maty[4]) || (x == matx[5] && y == maty[5]))
        {
            DestroyFog(x, y);
        }
        else
        {
            DestroyFog(x, y);
            DestroyFog(x + 1, y + 1);
            DestroyFog(x - 1, y + 1);
            DestroyFog(x, y + 1);
        }

    }

    private void whiterookFogDestroy(int x, int y)
    {
        if (x == matx[6] && y == maty[6])
        {
            towerfogDetroy(x, y);
        }
        if ((x == matx[4] && y == maty[4]) || (x == matx[5] && y == maty[5]))
        {
            DestroyFog(x, y);
        }
        else
        {
            DestroyFog(x, y);
            DestroyFog(x, y + 1);
            DestroyFog(x, y + 2);
            DestroyFog(x, y - 1);
            DestroyFog(x, y - 2);
            DestroyFog(x + 1, y);
            DestroyFog(x + 2, y);
            DestroyFog(x - 1, y);
            DestroyFog(x - 2, y);
        }
    }

    private void whitekingFogDestroy(int x, int y)
    {
        if (x == matx[6] && y == maty[6])
        {
            towerfogDetroy(x, y);
        }
        if ((x == matx[4] && y == maty[4]) || (x == matx[5] && y == maty[5]))
        {
            DestroyFog(x, y);
        }
        else
        {
            DestroyFog(x, y);
            DestroyFog(x + 1, y);
            DestroyFog(x - 1, y);
            DestroyFog(x, y + 1);
            DestroyFog(x, y - 1);
        }
    }

    private void whitebishopFogDestroy(int x, int y)
    {
        if (x == matx[6] && y == maty[6])
        {
            towerfogDetroy(x, y);
        }
        if ((x == matx[4] && y == maty[4]) || (x == matx[5] && y == maty[5]))
        {
            DestroyFog(x, y);
        }
        else
        {
            DestroyFog(x, y);
            DestroyFog(x + 1, y);
            DestroyFog(x + 1, y + 1);
            DestroyFog(x + 1, y - 1);
            DestroyFog(x - 1, y);
            DestroyFog(x - 1, y + 1);
            DestroyFog(x - 1, y - 1);
            DestroyFog(x, y + 1);
            DestroyFog(x, y - 1);
        }
    }

    private void whiteknightFogDestroy(int x, int y)
    {
        if (x == matx[6] && y == maty[6])
        {
            towerfogDetroy(x, y);
        }
        if ((x == matx[4] && y == maty[4]) || (x == matx[5] && y == maty[5]))
        {
            DestroyFog(x, y);
        }
        else
        {
            DestroyFog(x, y);
            DestroyFog(x, y + 1);
            DestroyFog(x, y + 2);
            DestroyFog(x - 1, y + 1);
            DestroyFog(x - 1, y + 2);
            DestroyFog(x + 1, y + 1);
            DestroyFog(x + 1, y + 2);
        }
    }

    private void whitequeenFogDestroy(int x, int y)
    {
        if (x == matx[6] && y == maty[6])
        {
            towerfogDetroy(x, y);
        }
        if ((x == matx[4] && y == maty[4]) || (x == matx[5] && y == maty[5]))
        {
            DestroyFog(x, y);
        }
        else
        {
            DestroyFog(x, y);
            DestroyFog(x, y + 1);
            DestroyFog(x, y - 1);
            DestroyFog(x - 1, y);
            DestroyFog(x - 1, y + 1);
            DestroyFog(x - 1, y - 1);
            DestroyFog(x - 2, y + 2);
            DestroyFog(x - 2, y - 2);
            DestroyFog(x + 1, y + 1);
            DestroyFog(x + 1, y - 1);
            DestroyFog(x + 1, y);
            DestroyFog(x + 2, y + 2);
            DestroyFog(x + 2, y - 2);
        }
    }




 


}

