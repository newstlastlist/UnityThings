using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalCutter  
{
    private Vector3[] planePoints = new Vector3[3];
    private Vector3 planeNormal;
    private List<Vector3> GetGameObjVerts(GameObject o)
    {
        List<Vector3> verts = new List<Vector3>();
        Mesh objMesh = o.GetComponent<MeshFilter>().mesh;
        foreach(Vector3 vert in objMesh.vertices)
        {
            verts.Add(o.transform.TransformPoint(vert));
        }
        return verts;   
    }
    private int[] GetGameObjTriangles(GameObject o)
    {
        Mesh objMesh = o.GetComponent<MeshFilter>().mesh;
        return objMesh.triangles;   
    }

    
    public UniversalCutter SetPlaneByGameObj(GameObject plane)
    {
        List<Vector3> planeVerts = GetGameObjVerts(plane); 
        planePoints[0] = planeVerts[0];
        planePoints[1] = planeVerts[1];
        for(var i = 2; i < planeVerts.Count; i++)
        {
            Vector3 side1 = planeVerts[0] - planeVerts[i];
            Vector3 side2 = planeVerts[1] - planeVerts[i];
            
            if(Vector3.Dot(side1.normalized, side2.normalized) < 0.9f && Vector3.Dot(side1.normalized, side2.normalized) > -0.9f)
            {
                planePoints[2] = planeVerts[i];
                break;
            }

        } 
        planeNormal = Vector3.Cross((planePoints[0] - planePoints[1]),(planePoints[2] - planePoints[0])).normalized; 

        return this;
    }

    public UniversalCutter SetPlaneByThreePoints(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        planePoints[0] = p1;
        planePoints[1] = p2;
        
        Vector3 side1 = planePoints[0] - p3;
        Vector3 side2 = planePoints[1] - p3;
        
        if(Vector3.Dot(side1.normalized, side2.normalized) < 0.95f && Vector3.Dot(side1.normalized, side2.normalized) > -0.95f)
        {
            planePoints[2] = p3;  
        }
        else
        {
            throw new UnityException("POINTS LIE IN ONE LINE!");
        }

        planeNormal = Vector3.Cross((planePoints[0] - planePoints[1]),(planePoints[2] - planePoints[0])).normalized;

        return this;
    }

    public UniversalCutter SetPlaneByPointAndNormal(Vector3 point, Vector3 normal)
    {
        planePoints[0] = point;   
        planeNormal = normal.normalized;
        return this;
    }

    private Vector3? IntersectionPointFinder(Vector3 v1, Vector3 v2)
    {
        //Vector3 planeNormal = Vector3.Cross((planePoints[0] - planePoints[1]),(planePoints[2] - planePoints[0])).normalized;

        Vector3 o;

        Vector3 v = planePoints[0] - v1;
        float d = Vector3.Dot(planeNormal, v);
        
        Vector3 w = v2 - v1;
        float e = Vector3.Dot(planeNormal, w);

        float x = d/e;
        
        if(e != 0)
        {
            o = v1 + w * x; //intersection point of line, on which lie v1 v2
            if(Vector3.Dot(v1 - o, v2 - o) <= 0) //check if current secton (v1,v2) intersects our plane
            {
                return o;    
            }
        }
        return null;
        
    }

    

    private List<List<Vector3>> calculateSegments(GameObject o)
    {
        List<Vector3> oVerts = GetGameObjVerts(o);
        int[] oTriangles = GetGameObjTriangles(o);

        //Fill list of segments  
        List<List<Vector3>> sectionsList = new List<List<Vector3>>(); //it will be List of segments like [[A,B],[D,K],[B,C]]
        for(int i = 0; i < oTriangles.Length; i += 3) //Get each vert in each current triangle
        {
            
            int i1 = (i + 0);
            int i2 = (i + 1);
            int i3 = (i + 2);
            Vector3 v1 = oVerts[oTriangles[i1]];
            Vector3 v2 = oVerts[oTriangles[i2]];
            Vector3 v3 = oVerts[oTriangles[i3]];

            List<Vector3> section = new List<Vector3>(); //segment of triangle like [A,B]

            //finding intersections in current triangle. it must be 2 points A and B
            if(IntersectionPointFinder(v1,v2) != null)
                section.Add((Vector3)IntersectionPointFinder(v1,v2));  

            if(IntersectionPointFinder(v2,v3) != null)
                section.Add((Vector3)IntersectionPointFinder(v2,v3));  

            if(IntersectionPointFinder(v3,v1) != null)
                section.Add((Vector3)IntersectionPointFinder(v3,v1));   

            if(section.Count > 0)
                sectionsList.Add(section); 
            //Debug.Log(section.Count);     
        }
        return sectionsList;
    }
    
    public List<List<Vector3>> SortedIntersectionPoints(GameObject o)
    {
        var sectionsList = calculateSegments(o);
        var lines = new List<List<Vector3>>();
        while(sectionsList.Count > 0)
        {
            var found = true;
            var line = sectionsList[0];
            sectionsList.RemoveAt(0);
            while(found)
            {
                found = false;
                for(int i = sectionsList.Count - 1; i >= 0; i--)
                {   
                    if(Compare(line[0], sectionsList[i][0]))
                    {
                        line.Insert(0, sectionsList[i][1]);
                        sectionsList.RemoveAt(i);
                        found = true;
                        break;
                    }
                    if(Compare(line[0], sectionsList[i][1]))
                    {
                        line.Insert(0, sectionsList[i][0]);
                        sectionsList.RemoveAt(i);
                        found = true;
                        break;
                    }
                    if(Compare(line[line.Count - 1], sectionsList[i][0]))
                    {
                        line.Add(sectionsList[i][1]);
                        sectionsList.RemoveAt(i);
                        found = true;
                        break;
                    }
                    if(Compare(line[line.Count - 1], sectionsList[i][1]))
                    {
                        line.Add(sectionsList[i][0]);
                        sectionsList.RemoveAt(i);
                        found = true;
                        break;
                    }
                }
            }
            lines.Add(line);
        }
        
        return lines;
    }

    

    private bool Compare(Vector3 a, Vector3 b)
    {
        
        return a == b;
    }

    
    
}
