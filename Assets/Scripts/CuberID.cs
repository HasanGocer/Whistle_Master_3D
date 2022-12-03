using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CuberID : MonoBehaviour
{
    [SerializeField] private int cuberCubeCount;
    [SerializeField] private TextMeshPro cuberCubeCountText;
    public List<GameObject> CubeGameObject = new List<GameObject>();
    public GameObject cubeParent;

    public void CuberCubeCountTextPlus()
    {
        cuberCubeCount++;
        cuberCubeCountText.text = cuberCubeCount.ToString();
    }
}
