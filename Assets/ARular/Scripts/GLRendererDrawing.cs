using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GLRendererDrawing : MonoBehaviour
{   
    // Draw red a rombus on the screen
    // and also draw a small cyan Quad in the left corner
    public Material mat;
    private int points = 0;

    private List<Vector3> pointList = new List<Vector3>();

    private bool compute;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Bla");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            pointList.Add(mousePosition);
            points++;
            if (points >= 2)
            {
                compute = true;
                
            }

            //GameObject clone = Instantiate(pointObject, new Vector3(Random.Range(0,5), Random.Range(0,0),Random.Range(5,10)), Quaternion.identity);
            //GameObject point = Instantiate(pointObject, mousePosition, Quaternion.identity);
        }
    }

    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }

        if (compute)
        {
            DrawLine(mat,pointList[0], pointList[1], 0.3f);
            pointList.Clear();
            //compute = false;
        }

        
    }

    void DrawLine(Material mat, Vector3 a, Vector3 b, float width)
    {
        
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        GL.Color(Color.red);
        
        GL.Vertex3(a.x,a.y+width/2,a.z);
        GL.Vertex3(b.x, b.y + width/2, b.z);
        GL.Vertex3(b.x, b.y - width/2, b.z);
        GL.Vertex3(a.x,a.y-width/2 , a.z);
        
        GL.End();
        GL.PopMatrix();
    }
}
