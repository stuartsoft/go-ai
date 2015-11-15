﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Board {
    public List<List<Piece>> pieceMatrix;

    public Board(int d)
    {
        pieceMatrix = new List<List<Piece>>();
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

}
