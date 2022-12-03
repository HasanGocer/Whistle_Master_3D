using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    //managerde bulunacak

    [System.Serializable]
    public class Field
    {
        public int cuberCount, cubeLineCount, cubeColumnCount, cubeObjectTypeCount;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    public void IDAwake()
    {
        ItemPlacement();
        StartFunc();
    }

    public void StartFunc()
    {
        GridSystem.Instance.NewGridCreated();
        CuberSystem.Instance.StartCuberPlacement();
    }

    private void ItemPlacement()
    {
        field.cuberCount = standart.cuberCount + (factor.cuberCount * constant.cuberCount);
        //fieldPrice.cuberCount = fieldPrice.cuberCount * factor.cuberCount;

        field.cubeLineCount = standart.cubeLineCount + (factor.cubeLineCount * constant.cubeLineCount);
        //fieldPrice.cubeLineCount = fieldPrice.cubeLineCount * factor.cubeLineCount;

        field.cubeColumnCount = standart.cubeColumnCount + (factor.cubeColumnCount * constant.cubeColumnCount);
        //fieldPrice.cubeColumnCount = fieldPrice.cubeColumnCount * factor.cubeColumnCount;

        field.cubeObjectTypeCount = standart.cubeObjectTypeCount + (factor.cubeObjectTypeCount * constant.cubeObjectTypeCount);
        //fieldPrice.cubeObjectTypeCount = fieldPrice.cubeObjectTypeCount * factor.cubeObjectTypeCount;
    }

    public void SetCuberCount()
    {
        //fieldPrice.cuberCount = fieldPrice.cuberCount / factor.cuberCount;
        factor.cuberCount++;
        //fieldPrice.cuberCount = fieldPrice.cuberCount * factor.cuberCount;
        field.cuberCount = standart.cuberCount + (factor.cuberCount * constant.cuberCount);
    }

    public void SetCubeLineCount()
    {
        //fieldPrice.cubeLineCount = fieldPrice.cubeLineCount / factor.cubeLineCount;
        factor.cubeLineCount++;
        //fieldPrice.cubeLineCount = fieldPrice.cubeLineCount * factor.cubeLineCount;
        field.cubeLineCount = standart.cubeLineCount + (factor.cubeLineCount * constant.cubeLineCount);
    }

    public void SetCubeColumnCount()
    {
        //fieldPrice.cubeColumnCount = fieldPrice.cubeColumnCount / factor.cubeColumnCount;
        factor.cubeColumnCount++;
        //fieldPrice.cubeColumnCount = fieldPrice.cubeColumnCount * factor.cubeColumnCount;
        field.cubeColumnCount = standart.cubeColumnCount + (factor.cubeColumnCount * constant.cubeColumnCount);
    }

    public void SetCubeObjectTypeCount()
    {
        //fieldPrice.cubeObjectTypeCount = fieldPrice.cubeObjectTypeCount / factor.cubeObjectTypeCount;
        factor.cubeObjectTypeCount++;
        //fieldPrice.cubeObjectTypeCount = fieldPrice.cubeObjectTypeCount * factor.cubeObjectTypeCount;
        field.cubeObjectTypeCount = standart.cubeObjectTypeCount + (factor.cubeObjectTypeCount * constant.cubeObjectTypeCount);
    }
}
