using UnityEngine;

public class JellyMesh : MonoBehaviour
{
   public float intensity = 1f;
   public float mass = 1f;
   public float stiffness = 1f;
   public float damping = 0.75f;
   private Mesh originalMesh, meshClone;
   private MeshRenderer renderer;
   private JellyVertex[] jellyVertices;
   private Vector3[] vertexArray;

   private void Awake()
   {
      originalMesh = GetComponent<MeshFilter>().sharedMesh;
      meshClone = Instantiate(originalMesh);
      GetComponent<MeshFilter>().sharedMesh = meshClone;
      renderer = GetComponent<MeshRenderer>();

      jellyVertices = new JellyVertex[meshClone.vertices.Length];
      for (int i = 0; i < meshClone.vertices.Length; i++)
      {
         jellyVertices[i] = new JellyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
      }
   }

   private void FixedUpdate()
   {
      vertexArray = originalMesh.vertices;
      for (int i = 0; i < jellyVertices.Length; i++)
      {
         Vector3 target = transform.TransformPoint(vertexArray[jellyVertices[i].id]);
         float intensity = (1 - (renderer.bounds.max.y - target.y) / renderer.bounds.size.y) * this.intensity;
         jellyVertices[i].Shake(target,mass,stiffness,damping);
         target = transform.InverseTransformPoint(jellyVertices[i].position);
         vertexArray[jellyVertices[i].id] = Vector3.Lerp(vertexArray[jellyVertices[i].id], target, intensity);
      }

      meshClone.vertices = vertexArray;
   }
}
