# Section Creator For Unity
This is universal script for creating between game object mesh and plane mesh.
For result of implementation of script you will get a List of Lists of Vectors 3 (List<List<Vector3>>) where each
List<Vector3> is individual section (for example if you cut across torus you will get two sections).

This script has 3 ways for set plane and get sections:
* ###GameObject plane - create a plane in scene. 

List<List<Vector3>> segments = new **UniversalCutter()**.**SetPlaneByGameObj**(plane).**SortedIntersectionPoints**(this.gameObject);
      
Where ***plane*** is GameObject.

* ###Set plane by 3 points. 

List<List<Vector3>> segments = new **UniversalCutter()**.**SetPlaneByThreePoints**(p1,p2,p3).**SortedIntersectionPoints**(this.gameObject);
      
Where ***p1, p2, p3*** is points (Vector3) which lie in the plane which will be cut our mesh.

* ###Set plane by point and normal.

List<List<Vector3>> segments = new **UniversalCutter()**.**SetPlaneByPointAndNormal**(point, normal).**SortedIntersectionPoints**(this.gameObject);
      
Where ***point*** is point (Vector3) which lies in the plane which will be cut our mesh.
***normal*** is normal from this point which is perpendicular to the cutter plane.
      
### For implementation of script you can see Test script in example project folder


![Section Creator For Unity](https://i.imgur.com/c4LYyKs.png)
