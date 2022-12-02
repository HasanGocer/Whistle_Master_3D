using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoSingleton<GridSystem>
{

    public int[,] GridInt;
    public GameObject[,] GridGameObject;
    [SerializeField] private GameObject _cubeTemplatePosition, _cubeParent;
    [SerializeField] private float _xDistance, _zDistance;
    [SerializeField] private int _OPCubeCount;


    public int lineCount, columnCount, randomCubeTypeCount;

    public void NewGridCreated()
    {
        MatrisResizeFunc(lineCount, columnCount);
        MatrisPlacementWithHGAlgorithmFunc(lineCount, columnCount, randomCubeTypeCount);
        GameObjectMatrisPlacement(_cubeTemplatePosition, _cubeParent, _xDistance, _zDistance, lineCount, columnCount, _OPCubeCount, GridGameObject, GridInt);
    }

    public void CubeAddObjectPool(GameObject obj)
    {
        ObjectPool.Instance.AddObject(_OPCubeCount, obj);
    }


    private void MatrisResizeFunc(int lineCount, int columnCount)
    {
        GridInt = new int[lineCount, columnCount];
        GridGameObject = new GameObject[lineCount, columnCount];
    }
    private void MatrisPlacementWithHGAlgorithmFunc(int lineCount, int columnCount, int randomCubeTypeCount)
    {
        for (int countForLine = 0; countForLine < lineCount; countForLine++)
        {
            for (int countForColumn = 0; countForColumn < columnCount; countForColumn++)
            {
                print(countForLine + " " + countForColumn);
                if (countForLine == 0)
                    GridInt[countForLine, countForColumn] = Random.Range(0, randomCubeTypeCount);
                else if (countForColumn == 0)
                {
                    if (countForLine >= 2)
                    {
                        if (GridInt[countForLine - 1, countForColumn] == GridInt[countForLine - 2, countForColumn])
                            GridInt[countForLine, countForColumn] = Random.Range(0, randomCubeTypeCount);
                        else
                            GridInt[countForLine, countForColumn] = GridInt[countForLine - 1, countForColumn];
                    }
                    else
                        GridInt[countForLine, countForColumn] = GridInt[countForLine - 1, countForColumn];
                }
                else
                {
                    print("Hata1");
                    if (countForLine >= 2 && countForColumn != columnCount - 1)
                        if (GridInt[countForLine - 1, countForColumn] == GridInt[countForLine - 2, countForColumn] || GridInt[countForLine - 1, countForColumn] == GridInt[countForLine - 1, countForColumn - 1] || GridInt[countForLine - 1, countForColumn] == GridInt[countForLine - 1, countForColumn + 1])
                            GridInt[countForLine, countForColumn] = Random.Range(0, randomCubeTypeCount);
                        else
                            GridInt[countForLine, countForColumn] = GridInt[countForLine - 1, countForColumn];
                    else if (countForColumn != columnCount - 1)

                        if (GridInt[countForLine - 1, countForColumn] == GridInt[countForLine - 1, countForColumn - 1] || GridInt[countForLine - 1, countForColumn] == GridInt[countForLine - 1, countForColumn + 1])
                            GridInt[countForLine, countForColumn] = Random.Range(0, randomCubeTypeCount);
                        else
                            GridInt[countForLine, countForColumn] = GridInt[countForLine - 1, countForColumn];
                    else
                    {
                        if (GridInt[countForLine - 1, countForColumn] == GridInt[countForLine - 1, countForColumn - 1])
                            GridInt[countForLine, countForColumn] = Random.Range(0, randomCubeTypeCount);
                        else
                            GridInt[countForLine, countForColumn] = GridInt[countForLine - 1, countForColumn];
                    }

                }
                print(GridInt[countForLine, countForColumn]);
            }
        }
    }
    private void GameObjectMatrisPlacement(GameObject cubeTemplatePosition, GameObject cubeParent, float xDistance, float zDistance, int lineCount, int columnCount, int OPCubeCount, GameObject[,] GridGameObject, int[,] GridInt)
    {
        for (int countForLine = 0; countForLine < lineCount; countForLine++)
        {
            for (int countForColumn = 0; countForColumn < columnCount; countForColumn++)
            {
                GameObject obj = CallCubeGameObject(OPCubeCount);
                CubeAddedListFunc(obj, countForLine, countForColumn, GridGameObject);
                CubePositionPlacement(obj, cubeTemplatePosition, cubeParent, xDistance, zDistance, countForLine, countForColumn);
                CubeMaterialPlacement(obj, GridInt[countForLine, countForColumn], MaterialSystem.Instance.CubeMaterial);
                CubeFixedFunc(obj, GridInt[countForLine, countForColumn]);
            }
        }
    }
    private GameObject CallCubeGameObject(int OPCubeCount)
    {
        return ObjectPool.Instance.GetPooledObject(OPCubeCount);
    }
    private void CubeAddedListFunc(GameObject obj, int countForLine, int countForColumn, GameObject[,] GridGameObject)
    {
        GridGameObject[countForLine, countForColumn] = obj;
    }
    private void CubePositionPlacement(GameObject obj, GameObject cubeTemplatePosition, GameObject cubeParent, float xDistance, float zDistance, int countForLine, int countForColumn)
    {
        obj.transform.position = new Vector3(cubeTemplatePosition.transform.position.x + (xDistance * countForLine), cubeTemplatePosition.transform.position.y, cubeTemplatePosition.transform.position.z + (zDistance * countForColumn));
        obj.transform.SetParent(cubeParent.transform);
    }
    private void CubeMaterialPlacement(GameObject obj, int CubeCount, List<Material> Materials)
    {
        obj.GetComponent<MeshRenderer>().material = Materials[CubeCount];
    }
    private void CubeFixedFunc(GameObject obj, int cubeCount)
    {
        obj.GetComponent<CubeID>().cubeCount = cubeCount;
    }
}
