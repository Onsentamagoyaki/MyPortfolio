using UnityEngine;

public class OnDrawPlayerBoxCast : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 direction = Vector3.down;
    public float distance = 0.05f; 
    public LayerMask layerMask;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

       
        Vector3 startPos = transform.position;
        Quaternion orientation = transform.rotation;

        
        Vector3 endPos = startPos + direction.normalized * distance;

      
        DrawBox(startPos, orientation, boxSize);

       
        DrawBox(endPos, orientation, boxSize);

  
        Gizmos.DrawLine(startPos, endPos);
    }

    void DrawBox(Vector3 center, Quaternion orientation, Vector3 size)
    {
        Vector3[] points = new Vector3[8];

        points[0] = center + orientation * new Vector3(-size.x, -size.y, -size.z) * 0.5f;
        points[1] = center + orientation * new Vector3(size.x, -size.y, -size.z) * 0.5f;
        points[2] = center + orientation * new Vector3(size.x, -size.y, size.z) * 0.5f;
        points[3] = center + orientation * new Vector3(-size.x, -size.y, size.z) * 0.5f;

        points[4] = center + orientation * new Vector3(-size.x, size.y, -size.z) * 0.5f;
        points[5] = center + orientation * new Vector3(size.x, size.y, -size.z) * 0.5f;
        points[6] = center + orientation * new Vector3(size.x, size.y, size.z) * 0.5f;
        points[7] = center + orientation * new Vector3(-size.x, size.y, size.z) * 0.5f;

        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawLine(points[i], points[(i + 1) % 4]);
            Gizmos.DrawLine(points[i + 4], points[(i + 1) % 4 + 4]);
            Gizmos.DrawLine(points[i], points[i + 4]);
        }
        Gizmos.DrawLine(points[0], points[1]);
        Gizmos.DrawLine(points[2], points[3]);
        Gizmos.DrawLine(points[4], points[5]);
        Gizmos.DrawLine(points[6], points[7]);
    }
}
