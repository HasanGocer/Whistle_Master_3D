using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CuberSystem : MonoSingleton<CuberSystem>
{
    [SerializeField] private int _OPCuberCount;
    [SerializeField] private float _cuberHorizontalDistance;
    [SerializeField] private GameObject _cuberTemplatePosition;
    [SerializeField] private GameObject _cuberParent;
    public float particalWaitTime, shakerWaitTime;
    public int shakerCuberStrength;
    public int OPBlastParticalCount;
    public List<GameObject> CuberGameObject = new List<GameObject>();

    public void StartCuberPlacement()
    {
        for (int i = 0; i < ItemData.Instance.field.cuberCount; i++)
        {
            GameObject obj = CallCuberGameObject(_OPCuberCount);
            CuberAddedListFunc(obj, CuberGameObject);
            CuberPositionPlacement(obj, _cuberTemplatePosition, _cuberParent, _cuberHorizontalDistance, CuberGameObject);
            CuberMaterialPlacement(obj, CuberGameObject.Count - 1, MaterialSystem.Instance.CubeMaterial);
            CuberFixedFunc(obj, CuberGameObject.Count - 1);
        }
    }

    public IEnumerator CallCuberBlast(GameObject templatePos, float waitTime, int OPBlastParticalCount)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(OPBlastParticalCount);
        obj.transform.position = templatePos.transform.position;
        yield return new WaitForSeconds(waitTime);
        ObjectPool.Instance.AddObject(OPBlastParticalCount, obj);
    }

    public IEnumerator CallCuberTouchCube(GameObject cuber, float waitTime, int shakerCuberStrength)
    {
        cuber.transform.DOShakePosition(waitTime, shakerCuberStrength);
        cuber.transform.DOShakeRotation(waitTime, shakerCuberStrength);
        cuber.transform.DOShakeScale(waitTime, shakerCuberStrength);
        yield return new WaitForSeconds(waitTime);
    }

    private GameObject CallCuberGameObject(int OPCuberCount)
    {
        return ObjectPool.Instance.GetPooledObject(OPCuberCount);
    }
    private void CuberAddedListFunc(GameObject obj, List<GameObject> CuberGameObject)
    {
        CuberGameObject.Add(obj);
    }
    private void CuberPositionPlacement(GameObject obj, GameObject cuberTemplatePosition, GameObject cuberParent, float cuberHorizontalDistance, List<GameObject> CuberGameObject)
    {
        obj.transform.position = new Vector3(cuberTemplatePosition.transform.position.x + (cuberHorizontalDistance * (CuberGameObject.Count - 1)), cuberTemplatePosition.transform.position.y, cuberTemplatePosition.transform.position.z);
        obj.transform.SetParent(cuberParent.transform);
    }
    private void CuberMaterialPlacement(GameObject obj, int CubeCount, List<Material> Materials)
    {
        obj.GetComponent<MeshRenderer>().material = Materials[CubeCount];
    }
    private void CuberFixedFunc(GameObject obj, int cuberCount)
    {
        CuberID cuberID = obj.GetComponent<CuberID>();
        CuberTouch cuberTouch = obj.GetComponent<CuberTouch>();

        cuberID.CuberCubeCountTextPlus();
        cuberTouch.CuberCountplacement(cuberCount);
    }
}
