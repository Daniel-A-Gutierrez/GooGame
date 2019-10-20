using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The arc should also stop when it quote-on-quote hits an object...
//rewrite this to work with a Vector3
//should display outside GameMode

[RequireComponent(typeof(MeshFilter))]

public class LaunchArcMesh : MonoBehaviour {

    Mesh mesh;
    public float mesh_width;

    public Vector3 player_vector; //player's position, height, 
    public Vector3 velocity_vector; 

    public float velocity; //projectile velocity...

    public int resolution = 500; //resolution of the arrow

 


    float g; //force of gravity

    float angle;
    float radianAngle; //angle that projectile is shot at

    


    private void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void OnValidate() {
        if (mesh != null && Application.isPlaying) {
            MakeArcMesh(CalculateArcArray());
        }
    }

    // Start is called before the first frame update
    void Start() {
        MakeArcMesh(CalculateArcArray());
    }

    

    void MakeArcMesh(Vector3[] arcVerts) {
        mesh.Clear();
        Vector3[] vertices = new Vector3[(resolution + 1) * 2];
        int[] triangles = new int[resolution * 6 * 2];

        for (int i = 0; i <= resolution; i++) {
            //set vertices
            //x - width, y - height, z - distance along arc
            vertices[i * 2] = new Vector3(mesh_width * 0.5f, arcVerts[i].y, arcVerts[i].x);
            vertices[i * 2 + 1] = new Vector3(mesh_width * -0.5f, arcVerts[i].y, arcVerts[i].x);

            //set triangles
            if (i != resolution) {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    //calculate height and distance of each vertex
    Vector3 CalculateArcPoint(float t, float maxDistance) {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }

    void SetVelocity(Vector3 v) {
        velocity = velocity_vector.magnitude;
    }  

    //create an array of vector3 positions for arc
    Vector3[] CalculateArcArray() { 
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;

        float maxDistance = (velocity * velocity / (2 * g)) *
                (1 + Mathf.Sqrt(1 + ((2 * g * player_vector.y) / (velocity * velocity * Mathf.Sin(radianAngle) * Mathf.Sin(radianAngle))))) *
                Mathf.Sin(2 * radianAngle);


        for (int i = 0; i <= resolution; i++) {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance); //we could subtract a distance from max distance...
        }

        return arcArray;
    }

        
    }
