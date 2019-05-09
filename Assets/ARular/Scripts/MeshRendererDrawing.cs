using UnityEngine;
using System.Collections.Generic;

public class MeshRendererDrawing : MonoBehaviour
{
    //public GameObject mainObj;
    // Material used for the connecting lines
    public Material lineMat;

    public float radius = 0.05f;

    List<GameObject> points;
    // Fill in this with the default Unity Cylinder mesh
    // We will account for the cylinder pivot/origin being in the middle.
  
    public Mesh cylinderMesh;

    
    GameObject ringGameObject;
    public TextMesh distanceText;

    private void Start()
    {
        points = new List<GameObject>();
        //DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    public float DrawLine(GameObject point)
    {
        points.Add(point);

        ringGameObject = new GameObject();
        ringGameObject.transform.parent = this.gameObject.transform;

        // We make a offset gameobject to counteract the default cylindermesh pivot/origin being in the middle
        GameObject ringOffsetCylinderMeshObject = new GameObject();
        ringOffsetCylinderMeshObject.transform.parent = this.ringGameObject.transform;

        // Offset the cylinder so that the pivot/origin is at the bottom in relation to the outer ring gameobject.
        ringOffsetCylinderMeshObject.transform.localPosition = new Vector3(0f, 1f, 0f);
        // Set the radius
        ringOffsetCylinderMeshObject.transform.localScale = new Vector3(radius, 1f, radius);


        float distance = 0;
        int lastIndex = points.Count - 1;

        if (points.Count>1)
        {
            // Create the the Mesh and renderer to show the connecting ring
            MeshFilter ringMesh = ringOffsetCylinderMeshObject.AddComponent<MeshFilter>();
            ringMesh.mesh = this.cylinderMesh;

            MeshRenderer ringRenderer = ringOffsetCylinderMeshObject.AddComponent<MeshRenderer>();
            ringRenderer.material = lineMat;

            ringGameObject.transform.position = points[lastIndex-1].transform.position;
            float cylinderDistance = 0.5f * Vector3.Distance(points[lastIndex - 1].transform.position, points[lastIndex].transform.position);
            ringGameObject.transform.localScale = new Vector3(ringGameObject.transform.localScale.x, cylinderDistance, ringGameObject.transform.localScale.z);

            // Make the cylinder look at the main point.
            // Since the cylinder is pointing up(y) and the forward is z, we need to offset by 90 degrees.
            ringGameObject.transform.LookAt(points[lastIndex].transform,Vector3.up);
            //ringGameObject.transform.LookAt(mainObj.transform, Vector3.up);
            Vector3 angel = ringGameObject.transform.eulerAngles;
            angel.x += 90;
            ringGameObject.transform.eulerAngles = angel;

            Transform lastPoint = points[lastIndex].transform;
            Transform secondLastPoint = points[lastIndex - 1].transform;

            distance = Vector3.Distance(points[lastIndex - 1].transform.position, points[lastIndex].transform.position);

            DrawLength(secondLastPoint.position, lastPoint.position, distance, secondLastPoint, 90);
        }

        return distance * 100;

    }

    public void ClearPoints()
    {
        points.Clear();
    }

    void DrawLength(Vector3 pointOne, Vector3 pointTwo, float distance, Transform lookAt, float zAngle)
    {
        Vector3 spawnLocation = (pointOne + pointTwo) / 2;
        GameObject textObj = Instantiate(distanceText.gameObject, new Vector3(spawnLocation.x,spawnLocation.y + 0.1f, spawnLocation.z) , Quaternion.identity);

        textObj.GetComponent<TextMesh>().text = distance.ToString("F2");
        textObj.transform.LookAt(lookAt);
        textObj.transform.eulerAngles = new Vector3(0, textObj.transform.eulerAngles.y + 90, 0);
        //textObj.transform.eulerAngles = new Vector3(lookAt.eulerAngles.x,lookAt.eulerAngles.y,zAngle);
    }

}
