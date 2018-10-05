using UnityEngine;

public interface IGrid {

    GameObject[] getNeighbors(GameObject tileO);
    GameObject[] getColumn(GameObject tileO);
    GameObject[] getColumn(int columnNum);
    GameObject[] getRightDiagonal(GameObject tileO);
    GameObject[] getLeftDiagonal(GameObject tileO);
    GameObject[] getRowSkip(GameObject tileO);
    GameObject[] getRowFull(GameObject tileO);
    GameObject[] getIfNotNull(GameObject[] neighbors,int first,int second);
    GameObject getTile(int x,int y, int z);
}
