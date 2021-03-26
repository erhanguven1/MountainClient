using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager instance;
    private void Awake()
    {
        instance = this;
    }

    public Tree treePrefab;
    [HideInInspector]
    public List<Point> treePositions = new List<Point>();
    public Transform treesParent;

    public void GenerateTrees()
    {
        foreach (var item in treePositions)
        {
            PlaceTree(item.vertexPosition * 15, Quaternion.identity, item);
        }
    }

    public void PlaceTree(Vector3 position, Quaternion rotation, Point p = null)
    {
        var tree = Instantiate(treePrefab, position, rotation, treesParent);
        tree.Place(p);
    }

    public void AddTreePosition(Vector3 position)
    {
        //treePositions.Add(position);
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var item in treePositions)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(item.vertexPosition * 15, 1.5f);
        }
    }
}
