using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace JoystickLab
{
    struct LineProp
    {
        public GameObject point;
        public GameObject distanceText;

        public LineProp(GameObject point, GameObject distanceText)
        {
            this.point = point;
            this.distanceText = distanceText;
        }


        public void SetEnable(bool enabled)
        {
            if (distanceText != null)
            {
                distanceText.SetActive(enabled);
            }            
            point.SetActive(enabled);
            
        }
    }
   
    public class LineRendererDrawing : SingleToneManager<LineRendererDrawing>
    {
        public Material lineMaterial;

        public Material dottedLineMaterial;

        //public TextMesh distanceText;
        public GameObject distanceTextButton;
        public UnitConverter distanceUIBox;
        
        private List<Transform> clickPoints;
        private List<Vector3> clickPositions;
        public float lineWidth;

        GameObject textObj = null;
        LineRenderer lineRenderer;

        private Stack<LineProp> pointStack;

        private bool isDisCrete;
        
        public bool IsDisCrete
        {
            private get { return isDisCrete; }
            set
            {
                isDisCrete = value;
                textObj.SetActive(false);
            }
        }
        // Use this for initialization
        void Start()
        {
            //lineRenderer = GetComponent<LineRenderer>();
            clickPoints = new List<Transform>();
            lineRenderer = new LineRenderer();
            pointStack = new Stack<LineProp>();
            
            textObj = Instantiate(distanceTextButton.gameObject, Vector3.zero, Quaternion.identity);
            textObj.name = "DottedDistanceLabel";
            textObj.SetActive(false);
            

        }
        
        public float DrawLine(GameObject point, bool done)
        {
            float distance = 0;
            int lastIndex = clickPoints.Count - 1;

            Transform startpoint = null;
            Transform endPoint = null;

            if (!done && clickPoints.Count > 0)
            {
                //Debug.Log("Dotted");
                lineRenderer.material = dottedLineMaterial;
                lineRenderer.textureMode = LineTextureMode.Tile;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, clickPoints[lastIndex].position);
                lineRenderer.SetPosition(1, point.transform.position);
                startpoint = clickPoints[lastIndex];
                endPoint = point.transform;
            }

            if (done) // done == true when user click
            {
                //Debug.Log("Straight");
                clickPoints.Add(point.transform);
                
                pointStack.Push(new LineProp(point, textObj));
                point.transform.name = clickPoints.Count.ToString();
                //print("The Pushed Point is = "+ point.name+ " and the text is = "+textObj.name);
                
                lastIndex = clickPoints.Count - 1;
                if (lineRenderer != null)
                {
                    lineRenderer.material = lineMaterial;
                }
                lineRenderer = point.AddComponent<LineRenderer>();
                lineRenderer.material = lineMaterial;
                lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
                if (clickPoints.Count >= 2)
                {
                    startpoint = clickPoints[lastIndex];
                    endPoint = clickPoints[lastIndex - 1];
                    
                    if (IsDisCrete)
                    {
                        ClearPoints();
                    }
                }                
            }
            if (startpoint != null && endPoint != null)
            {
                Vector3 resultant = endPoint.position - startpoint.position;
                distance = Vector3.Magnitude(resultant);
                float angel = Mathf.Atan2(resultant.z, resultant.x) * Mathf.Rad2Deg;
                DrawLength(endPoint.position, startpoint.position, distance, angel, done);
            }

            return distance;
        }

        void DrawLength(Vector3 pointOne, Vector3 pointTwo, float distance, float yAngle, bool done)
        {
            string unit;
            string finalOutputText;
            
            Vector3 point = (pointOne + pointTwo) / 2;
            point.y += 0.02f;
            if (done)
            {
                textObj = Instantiate(distanceTextButton.gameObject, point, Quaternion.identity);
                textObj.name = clickPoints.Count.ToString() + "th Label";
                if (pointStack.Count > 0)
                {
                    LineProp l = pointStack.Pop();
                    l.distanceText = textObj;
                    pointStack.Push(l);
                }
            }      
            else if (!done)
            {
                if (distance * 100 > 5f && pointStack.Count>0)
                {
                    textObj.SetActive(true);
                }
                else
                {
                    textObj.SetActive(false);
                }
                textObj.transform.position = point;
            }

            finalOutputText = processDistance(distance, out unit);
            
            textObj.GetComponentInChildren<TextMeshProUGUI>().text = finalOutputText + " <size=2>" + unit;

            Vector3 prevAngle = textObj.transform.eulerAngles;
            prevAngle.y = -yAngle;
            prevAngle.x = 90;
            
            textObj.transform.eulerAngles = prevAngle;
        }

        public void ClearPoints()
        {
            clickPoints.Clear();
        }

        public void Undo()
        {
            if (pointStack.Count > 0)
            {
                pointStack.Pop().SetEnable(false);
                textObj.SetActive(false);
            }
            ClearPoints();
        }

        private string processDistance(float distance, out string unit)
        {
            float d = distance * 100;
            unit = "cm";
            if (d > 100)
            {
                d = distance;
                unit = "m";
            }
            
            return d.ToString("F1");

        }

        public void ShowDistanceBox(string output)
        {
            int leftarrowIndex = output.IndexOf('<');
            int rightarrawIndex = output.IndexOf('>');
            string ans = output.Substring(0,leftarrowIndex-1);
            print("leng"+output.Length);
            string unit = output.Substring(rightarrawIndex+1, output.Length - rightarrawIndex-1);
            distanceUIBox.output = ans+ " <size=80>" +unit;
            
            distanceUIBox.gameObject.SetActive(true);
            
        }

        public void HideDistanceBox()
        {
            distanceUIBox.gameObject.SetActive(false);
        }
    }
}