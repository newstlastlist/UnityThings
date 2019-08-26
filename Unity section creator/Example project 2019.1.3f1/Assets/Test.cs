using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Test : MonoBehaviour
{

    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {

        //Set Plane By Game Object (create 3d plane)
        
        List<List<Vector3>> segments = new UniversalCutter()
            .SetPlaneByGameObj(plane)
            .SortedIntersectionPoints(this.gameObject);

        // //Set plane by 3 points
        
        // Vector3 p1 = new Vector3(-14.8f, 19.0f, 30.0f);
        // Vector3 p2 = new Vector3(-10.2f, 20.5f, 26.8f);
        // Vector3 p3 = new Vector3(-13.7f, 13.4f, 28.9f);
        // List<List<Vector3>> segments = new UniversalCutter()
        //     .SetPlaneByThreePoints(p1,p2,p3)
        //     .SortedIntersectionPoints(this.gameObject);

        // // Set Plane by point and normal
        // Vector3 normal = new Vector3(0.6f, 0.0f, 0.8f);
        // Vector3 point = new Vector3(-14.8f, 19.0f, 30.0f);
        // List<List<Vector3>> segments = new UniversalCutter()
        //     .SetPlaneByPointAndNormal(point, normal)
        //     .SortedIntersectionPoints(this.gameObject);

        List<Vector3> positions = new List<Vector3>();
        foreach(var segment in segments)
        {
           foreach(var position in segment)
           {
               positions.Add(position);
           }
        }

        GetComponent<LineRenderer>().positionCount = positions.Count;
        GetComponent<LineRenderer>().SetPositions(positions.ToArray());
       
        
        

    }

}
