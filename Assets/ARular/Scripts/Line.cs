using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Material lineMaterial;    
    public TextMesh distanceText;
    
    protected List<Transform> clickPoints;
    protected int index = 0;
    protected float lineWidth = 0.03f;
}
