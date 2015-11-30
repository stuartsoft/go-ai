﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {
    public List<List<Piece>> pieceMatrix;
    public int CurrentTurn { get; private set; }//0 for black, 1 for white
    public bool needsRefreshModel;

    public int boardSize {
        get { return pieceMatrix.Count; } }

    public Board(int d)
    {
        needsRefreshModel = false;
        pieceMatrix = new List<List<Piece>>();
        CurrentTurn = 0;
        for (int i = 0; i < d; i++)
        {
            List<Piece> temp = new List<Piece>();
            for (int j = 0; j < d; j++)
            {
                Piece tempPiece = new Piece(new Vector2(i, j));
                temp.Add(tempPiece);
            }
            pieceMatrix.Add(temp);
        }
    }

    public List<Piece> AdjPieces(int r, int c, Color targetColor)
    {

        List<Piece> adj = new List<Piece>();
        if (r - 1 > 0 && pieceMatrix[r - 1][c].color == targetColor)
            adj.Add(pieceMatrix[r - 1][c]);
        if (c - 1 > 0 && pieceMatrix[r][c - 1].color == targetColor)
            adj.Add(pieceMatrix[r][c - 1]);
        if (r + 1 > 0 && pieceMatrix[r + 1][c].color == targetColor)
            adj.Add(pieceMatrix[r + 1][c]);
        if (c + 1 > 0 && pieceMatrix[r][c + 1].color == targetColor)
            adj.Add(pieceMatrix[r][c + 1]);

        return adj;
    }

    public List<Piece> ConnectedPiecesDFS(int r, int c)
    {
        List<Piece> Conn = new List<Piece>();
        //TODO: dfs search starting from node [r][c], looking for all connected notes of the same color
        return Conn;
    }

    public List<Piece> ConnectedGroupLiberties(List<Piece> Conn) {
        List<Piece> Liberties = new List<Piece>();
        for (int i = 0; i < Conn.Count; i++)
        {
            List<Piece> temp = AdjPieces((int)Conn[i].position.x, (int)Conn[i].position.y, Constants.CLEARCOLOR);
            for (int j = 0; j < temp.Count; j++)
            {
                Liberties.Add(temp[j]);
            }
        }

        //TODO: eliminate duplicate liberties before returning

        return Liberties;
    }

    public List<Vector2> PossibleMoves()
    {
        List<Vector2> PosMoves = new List<Vector2>();
        //every spot that isn't currently occupied. Later we can optimize this

        for (int i = 0;i< pieceMatrix.Count; i++)
        {
            for (int j = 0; j < pieceMatrix.Count; j++)
            {
                if (pieceMatrix[i][j].color == Constants.CLEARCOLOR)
                {
                    PosMoves.Add(new Vector2(i, j));
                }
            }
        }
        return PosMoves;
    }

    public bool PlayPiece(int r, int c, Color color)
    {
        if (r > 0 && c > 0 && r < pieceMatrix.Count && c < pieceMatrix.Count)
        {
            if (pieceMatrix[r][c].color == Constants.CLEARCOLOR)
            {
                pieceMatrix[r][c] = new Piece(new Vector2(r, c), color);
                CurrentTurn = (CurrentTurn == 0) ? 1 : 0;

                //TODO: evaluate and eliminate captured pieces
                needsRefreshModel = true;
                return true;
            }
        }

        return false;
        //Action could not be completed, either there is already a piece in the position specified,
        //or the position specified does not exist
    }

    public Vector2 CountPieces()//returns a vector of the black pieces and white pieces 
    {
        int b = 0;
        int w = 0;

        for (int i = 0; i < pieceMatrix.Count; i++)
        {
            for (int j = 0; j < pieceMatrix[i].Count; j++)
            {
                if (pieceMatrix[i][j].color == Constants.BLACKCOLOR)
                    b++;
                else if (pieceMatrix[i][j].color == Constants.WHITECOLOR)
                    w++;
            }
        }

        return new Vector2(b, w);
    }

    public int Score()
    {
        Vector2 piecesCount = CountPieces();
        return (int)piecesCount.x - (int)piecesCount.y;
    }

    public Board cloneBoard()
    {
        Board newBoard = new Board(boardSize);
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Piece temp = new Piece(pieceMatrix[i][j].position, pieceMatrix[i][j].color);
                newBoard.pieceMatrix[i][j] = temp;
            }
        }
        return newBoard;
    }

}
