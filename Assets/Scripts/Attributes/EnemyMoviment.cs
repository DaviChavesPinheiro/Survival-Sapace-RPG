using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviment : Movement
{
    Transform target;
	Vector3[] path;
	int targetIndex;

    override protected void Awake() {
        base.Awake();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Search(){
        PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
    }

    public void CancelSearch(){
        StopCoroutine("FollowPath");
    }

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        StopCoroutine("FollowPath");
        path = newPath;
		targetIndex = 0;
		if (pathSuccessful && gameObject.activeSelf) {
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
        if(path != null && path.Length > 0){
			Vector3 currentWaypoint = path[0];
			while (true) {
				if (Vector2.Distance(transform.position, currentWaypoint) <= .25f) {
					targetIndex ++;
					if(targetIndex >= path.Length){
						targetIndex = 0;
						path = new Vector3[0];
						yield break;
					}
					currentWaypoint = path[targetIndex];
				}

				RotateToPosition(currentWaypoint);
				rb.velocity = (currentWaypoint - transform.position).normalized * maxSpeed * 30 * Time.deltaTime;
				yield return null;
			}
		}
	}

    override public void Accelerate(){
        rb.velocity = transform.up.normalized * maxSpeed * 30 * Time.deltaTime;
		isAccelerating = true;
    }

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.green;
				Gizmos.DrawSphere(path[i], .2f);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
