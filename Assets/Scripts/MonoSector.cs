using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class MonoSector : MonoBehaviour {
	public float radius;
	public float degree;
	public int segments;

	private MonoAI _owner;

	public void Init(MonoAI owner)
	{
		_owner = owner;
		return;
	}

	// Use this for initialization
	void Start () {
		MeshFilter meshFiliter = transform.GetComponent<MeshFilter>();
		meshFiliter.mesh = CreateMesh(radius, degree, segments);
		return;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMesh(radius, degree, segments);
		return;
	}

	private Mesh CreateMesh(float radius, float angleDegree, int segments)
	{
		if (segments == 0)
		{
			segments = 1;
		}
		Mesh mesh = new UnityEngine.Mesh();
		Vector3[] vertics = new Vector3[segments + 2];
		vertics[0] = new Vector3(0f, 0f, 0f);
		Vector2[] uvs = new Vector2[vertics.Length];
		uvs[0] = new Vector2(0.5f, 0.5f);
		float angle = Mathf.Deg2Rad * angleDegree;
		float startAngle = Mathf.Deg2Rad * (180 - angleDegree) / 2f;
		float angleCurrent = startAngle + angle;
		float angleDelta = angle / segments;
		for(int i = 1; i < vertics.Length; ++i)
		{
			float x = Mathf.Cos(angleCurrent);
			float y = Mathf.Sin(angleCurrent);
			vertics[i] = new Vector3(x * radius, y * radius, 0f);
			uvs[i] = new Vector2(0.5f + x * 0.5f, 0.5f + y * 0.5f);
			angleCurrent -= angleDelta;
		}

		int[] triangles = new int[segments * 3];
		for(int i = 0, vi = 1; i < triangles.Length; i += 3, ++vi)
		{
			triangles[i] = 0;
			triangles[i + 1] = vi;
			triangles[i + 2] = vi + 1;
		}
		mesh.vertices = vertics;
		mesh.uv = uvs;
		mesh.triangles = triangles;

		return mesh;
	}

	private void UpdateMesh(float radius, float angleDegree, int segments)
	{
		float angle = Mathf.Deg2Rad * angleDegree;
		float startAngle = Mathf.Deg2Rad * (180 - angleDegree) / 2f;
		float angleCurrent = startAngle + angle;
		float angleDelta = angle / segments;
		Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		for (int i = 1; i < vertices.Length; ++i)
		{
			RaycastHit hitInfo;
			Vector3 direction = transform.TransformPoint(vertices[i]) - transform.position;
			float length = radius;
			LayerMask blockMask = 1 << 8 | 1 << 9 | 1 << 10;
			if (Physics.Raycast(transform.position, direction.normalized, out hitInfo, length, blockMask))
			{
				Vector3 hitPos = transform.InverseTransformPoint(hitInfo.point);
				vertices[i] = hitPos;
				//float ratio = (hitPos - transform.position).magnitude / radius;
				MonoPlayer player = hitInfo.transform.GetComponent<MonoPlayer>();
				if (player != null)
				{
					//update the state to attack
					MonoAIManager.Instance.OnDetectEnemy();
				}
				MonoAI ai = hitInfo.transform.GetComponent<MonoAI>();
				if (ai != null && ai.status == AIStatus.Freeze)
				{
					//update the state to attack
					MonoAIManager.Instance.OnDetectEnemy();
				}
			}
			else
			{
				Vector3 hitPos = transform.InverseTransformPoint(transform.position + direction.normalized * length);
				vertices[i] = hitPos;
				//float ratio = (hitPos - transform.position).magnitude / radius;
			}
			angleCurrent -= angleDelta;
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals();

		return;
	}
}
