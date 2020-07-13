using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    Transform target;
    RectTransform rectTransform;
    float borderSize = 15f;
    float visibleDistance = Mathf.Infinity;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetTarget(Transform target) {
        this.target = target;
        if(target.GetComponent<Health>())
            target.GetComponent<Health>().onDie += DestroyPointer;
    }

    public void SetVisibleDistance(float value){
        visibleDistance = value;
    }
    
    public void Update() {
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(target.position);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen) {
            if(Vector2.Distance(target.position, transform.parent.position) > visibleDistance) DestroyPointer();
            RotatePointerTowardsTargetPosition();

            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            rectTransform.position = pointerWorldPosition;
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0f);
        } else
        {
            DestroyPointer();
        }
    }

    private void DestroyPointer()
    {
        if(target.GetComponent<Health>())
            target.GetComponent<Health>().onDie -= DestroyPointer;
        transform.parent.GetComponent<PointersController>().DestroyPointer(target);
        Destroy(gameObject);
    }

    private void RotatePointerTowardsTargetPosition() {
        Vector3 toPosition = target.position;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        rectTransform.localEulerAngles = new Vector3(0, 0, angle + 135f);
    }

    public float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
