using UnityEngine;

public interface IGrid {

    GameObject[] getNeighbors(GameObject tileObject);
    GameObject[] getColumn(GameObject tileObject);
    GameObject[] getColumn(int columnNum);
    GameObject[] getRightDiagonal(GameObject tileObject);
    GameObject[] getLeftDiagonal(GameObject tileObject);
    GameObject[] getRowSkip(GameObject tileObject);
    GameObject[] getRowFull(GameObject tileObject);
    GameObject[] getIfNotNull(GameObject[] neighbors, int topRowCheck, int bottomRowCheck);
    GameObject getTile(int x, int y, int z);
}
