using UnityEngine;

public class JellyVertex : MonoBehaviour
{
   public int id;
   public Vector3 position;
   public Vector3 velocity;
   public Vector3 force;

   public JellyVertex(int vertexId,Vector3 pos)
   {
      id = vertexId;
      position = pos;
   }

   public void Shake(Vector3 target, float m, float s, float d)
   {
      force = (target - position) * s;
      velocity = (velocity + force / m) * d;
      position += velocity;

      if ((velocity + force + force / m).magnitude < 0.001f)
      {
         position = target;
      }
   }
}
