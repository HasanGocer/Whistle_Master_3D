using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    //managerde bulunacak

    [System.Serializable]
    public class Field
    {
        public int cuberCount;
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
        CuberSystem.Instance.StartCuberPlacement();
        GridSystem.Instance.NewGridCreated();
    }

    private void ItemPlacement()
    {
        field.cuberCount = standart.cuberCount + (factor.cuberCount * constant.cuberCount);
        fieldPrice.cuberCount = fieldPrice.cuberCount * factor.cuberCount;

    }

    public void SetCuberCount()
    {
        fieldPrice.cuberCount = fieldPrice.cuberCount / factor.cuberCount;
        factor.cuberCount++;
        fieldPrice.cuberCount = fieldPrice.cuberCount * factor.cuberCount;
        field.cuberCount = standart.cuberCount + (factor.cuberCount * constant.cuberCount);

    }

    /*public void RunnerCount()
    {
        field.runnerCount = standart.runnerCount + (factor.runnerCount * constant.runnerCount);

        if (field.runnerCount > max.runnerCount)
        {
            field.runnerCount = max.runnerCount;
        }
    }


    public void RunnerSpeed()
    {
        field.runnerSpeed = standart.runnerSpeed - (factor.runnerSpeed * constant.runnerSpeed);

        if (field.runnerSpeed < max.runnerSpeed)
        {
            field.runnerSpeed = max.runnerSpeed;
        }
    }*/
}
