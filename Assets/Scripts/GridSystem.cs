using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoSingleton<GridSystem>
{

    public int[,] GridInt;
    public GameObject[,] GridGameObject;
    public float scale;
    [SerializeField] private GameObject _cubeTemplatePosition, _cubeParent;
    [SerializeField] private float _positionDistance;
    [SerializeField] private int _OPCubeCount;

    public void NewGridCreated()
    {
        MatrisResizeFunc(ItemData.Instance.field.cubeLineCount, ItemData.Instance.field.cubeColumnCount);
        MatrisPlacementWithHGAlgorithmFunc(ItemData.Instance.field.cubeLineCount, ItemData.Instance.field.cubeColumnCount, ItemData.Instance.field.cubeObjectTypeCount);
        GameObjectMatrisPlacement(_cubeTemplatePosition, _cubeParent, ItemData.Instance.field.cubeLineCount, ItemData.Instance.field.cubeColumnCount, _OPCubeCount, GridGameObject, GridInt);
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
            }
        }
    }
    private void GameObjectMatrisPlacement(GameObject cubeTemplatePosition, GameObject cubeParent, int lineCount, int columnCount, int OPCubeCount, GameObject[,] GridGameObject, int[,] GridInt)
    {
        float scale = CubePositionAlgorithmFunc(_positionDistance, lineCount);
        for (int countForLine = 0; countForLine < lineCount; countForLine++)
        {
            for (int countForColumn = 0; countForColumn < columnCount; countForColumn++)
            {
                GameObject obj = CallCubeGameObject(OPCubeCount);
                CubeAddedListFunc(obj, countForLine, countForColumn, GridGameObject);
                CubePositionPlacement(obj, cubeTemplatePosition, cubeParent, scale, _positionDistance, countForLine, countForColumn);
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
    private float CubePositionAlgorithmFunc(float positionDistance, int lineCount)
    {
        return scale = ((5 * positionDistance) + 1) / (6 * lineCount);
    }
    private void CubePositionPlacement(GameObject obj, GameObject cubeTemplatePosition, GameObject cubeParent, float scale, float positionDistance, int countForLine, int countForColumn)
    {
        obj.transform.position = new Vector3(cubeTemplatePosition.transform.position.x + ((scale / 2) - (positionDistance / 2) + ((scale / 5) * 6 * countForLine)), cubeTemplatePosition.transform.position.y, cubeTemplatePosition.transform.position.z + ((scale / 5) * 6 * countForColumn));
        obj.transform.localScale = new Vector3(scale, obj.transform.localScale.y, scale);
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
