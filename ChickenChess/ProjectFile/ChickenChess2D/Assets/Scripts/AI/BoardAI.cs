using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAI
{
    const int pawnValue = 100;
    const int knightValue = 300;
    const int bishopValue = 300;
    const int rookValue = 500;
    const int queenValue = 900;



    /*
    public static float Minmax(
        Board board,
        int player,
        int maxDepth,
        int currentDepth,
        ref Move bestMove)
    {
        if (board.IsGameOver() || currentDepth == maxDepth)
            return board.Evaluate(player);

        bestMove = null;
        float bestScore = Mathf.Infinity;
        if (board.GetCurrentPlayer() == player)
            bestScore = Mathf.NegativeInfinity;
        foreach(Move m in board.GetMoves())
        {
            Board b = board.MakeMove(m);
            float currentScore;
            Move currentMove = null;

            currentScore = Minmax(b, player, maxDepth, currentDepth + 1, ref currentMove);
            if(board.GetCurrentPlayer() == player)
            {
                if(currentScore > bestScore)
                {
                    bestScore = currentScore;
                    bestMove = currentMove;
                }
            }
        }

        return bestScore;
    }

    public static float Negamax(
        Board board,
        int maxDepth,
        int currentDepth,
        ref Move bestMove)
    {
        if (board.IsGameOver() || currentDepth == maxDepth)
            return board.Evaluate();

        bestMove = null;
        float bestScore = Mathf.NegativeInfinity;

        foreach(Move m in board.GetMoves())
        {
            Board b = board.MakeMove(m);
            float recursedSocre;
            Move currentMove = null;

            recursedSocre = Negamax(b, maxDepth, currentDepth + 1, ref currentMove);
            float currentScore = -recursedSocre;
            if(currentScore > bestScore)
            {
                bestScore = currentScore;
                bestMove = m;
            }
        }
        return bestScore;
    }*/
    
}
