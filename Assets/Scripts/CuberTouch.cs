using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuberTouch : MonoBehaviour
{
    private int CuberCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            if (ControlCubeCount(other.gameObject))
            {
                TouchCube(other.gameObject);
            }
            else
            {
                //oyun bitir
            }
        }
    }

    public bool ControlCubeCount(GameObject obj)
    {
        if (obj.GetComponent<CubeID>().cubeCount == CuberCount)
            return true;
        else return false;
    }

    public void CuberCountplacement(int count)
    {
        CuberCount = count;
    }

    private void TouchCube(GameObject other)
    {
        CuberSystem cuberSystem = CuberSystem.Instance;
        CuberID cuberID = GetComponent<CuberID>();

        cuberSystem.AddedCubeTouch(cuberID, other.gameObject);
        StartCoroutine(cuberSystem.CallCuberTouchCube(other.gameObject, cuberSystem.shakerWaitTime, cuberSystem.shakerCuberStrength));
        //StartCoroutine(cuberSystem.CallCuberBlast(other, cuberSystem.particalWaitTime, cuberSystem.OPBlastParticalCount));
        //GridSystem.Instance.CubeAddObjectPool(other);
    }
}
