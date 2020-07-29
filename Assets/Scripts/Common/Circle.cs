using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public int Segments = 32;
    public Color Color = Color.blue;
    public float XRadius = 1;
    public float YRadius = 1;
    public float ZRadius = 5;
 
private void OnDrawGizmos() {
    DrawEllipse(transform.position, transform.forward, transform.up, XRadius * transform.localScale.x, YRadius * transform.localScale.y, Segments, Color, 0, ZRadius);
}
 
private static void DrawEllipse(Vector3 pos, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, Color color, float duration = 0, float radiusZ = 5) {
    float angle = 0f;
    Quaternion rot = Quaternion.LookRotation(forward, up);
    Vector3 lastPoint = Vector3.zero;
    Vector3 thisPoint = Vector3.zero;
    for (int i = 0; i < segments + 1; i++) {
    thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
    thisPoint.z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;
    if (i > 0) {
    Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
    }
    lastPoint = thisPoint;
    angle += 360f / segments;
    }

    Debug.DrawRay(pos, Vector3.forward * radiusY, Color.blue);
    Debug.DrawRay(pos, Vector3.back * radiusY, Color.blue);
    Debug.DrawRay(pos, Vector3.right * radiusX, Color.red);
    Debug.DrawRay(pos, Vector3.left * radiusX, Color.red);
    Debug.DrawRay(pos, Vector3.up * radiusZ, Color.green);

}

}
