﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {


	[SerializeField] Transform target;
	[SerializeField] float speed = 200;
	Vector3[] path;
	int targetIndex;
    Movement movement;

    private void Awake() {
        movement = GetComponent<Movement>();
    }
	void Start() {
		StartCoroutine(Searching());
	}

    IEnumerator Searching(){
        PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
        yield return new WaitForSeconds(.2f);
        StartCoroutine(Searching());
    }

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        StopCoroutine("FollowPath");
        path = newPath;
		targetIndex = 0;
		if (pathSuccessful) {
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
        if(path == null || path.Length <= 0) StopCoroutine("FollowPath");
		Vector3 currentWaypoint = path[0];
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if(targetIndex >= path.Length){
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
				currentWaypoint = path[targetIndex];
			}

            movement.RotateToPosition(currentWaypoint);
            // movement.rb.velocity = (currentWaypoint - transform.position).normalized * speed * Time.deltaTime;
			yield return null;

		}
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