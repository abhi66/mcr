using UnityEngine;
using System.Collections;

public class Damage_bkp : MonoBehaviour {



		public float maxMoveDelta = 1f;
		public float maxCollisionStraingth = 30;
		public float yForceDamp = 0.3f;
		public float demolutionRange = 0.6f;
		public float impactDirManipulator = .75f;
		public MeshFilter[] optionalMeshList;

		MeshFilter [] meshfilters;
		float sqrDemRange;

		void Start () {
			if (optionalMeshList.Length > 0)
				meshfilters = optionalMeshList;
			else
				meshfilters = GetComponentsInChildren<MeshFilter> ();

			sqrDemRange = demolutionRange * demolutionRange;
		}

		void OnCollisionEnter(Collision hit){
			Vector3 colRelVel = hit.relativeVelocity;
			colRelVel *= yForceDamp;
			Vector3 colPointToMe = transform.position - hit.contacts [0].point;
			float colStrength = colRelVel.magnitude * Vector3.Dot (hit.contacts [0].normal, colPointToMe.normalized);
			OnMeshForce (hit.contacts [0].point, Mathf.Clamp01 (colStrength / maxCollisionStraingth));
			Debug.Log ("collision happen");
		}
		void OnMeshForce(Vector4 originPosAndForce){
			OnMeshForce ((Vector3)originPosAndForce, originPosAndForce.w);
		}
		void OnMeshForce(Vector3 originMAF, float force){
			force = Mathf.Clamp01 (force);
			for (int j = 0; j < meshfilters.Length; ++j) {
				Vector3[] verts = meshfilters [j].mesh.vertices;
				for(int i = 0; i < verts.Length; ++i){
					Vector3 scaledVerts = Vector3.Scale (verts [i], transform.localScale);
					Vector3 vertWorldPos = meshfilters [j].transform.position + meshfilters [j].transform.rotation * scaledVerts;
					Vector3 originToMeDir = vertWorldPos - originMAF;
					Vector3 flatVertToCenterDir = transform.position - vertWorldPos;
					flatVertToCenterDir.y = 0;

					if(originToMeDir.sqrMagnitude < sqrDemRange){
						float dist = Mathf.Clamp01 (originToMeDir.sqrMagnitude / sqrDemRange);
//						float movDelta = force * (1 - dist) * maxMoveDelta;
						Vector3 moveDir = Vector3.Slerp(originToMeDir, flatVertToCenterDir, impactDirManipulator).normalized * maxMoveDelta;
						verts [i] += Quaternion.Inverse (transform.rotation) * moveDir;

					}
				}
				meshfilters [j].mesh.vertices = verts;
				meshfilters [j].mesh.RecalculateBounds ();
				Debug.Log ("changing mesh");
			}
			Debug.Log ("out msh");
		}

	}

