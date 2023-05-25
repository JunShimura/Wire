using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(HingeJoint))]

public class WireController : MonoBehaviour
{
    public string touchableTag = "Touchable";

    LineRenderer lineRenderer;
    HingeJoint joint;

    Ray ray = new Ray();
    RaycastHit hit = new RaycastHit();
    Vector3 contactPoint = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        joint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag(touchableTag))
                {
                    ray.origin = transform.position;
                    ray.direction = hit.point - transform.position;
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                    {
                        contactPoint = hit.point;
                        joint.connectedBody = hit.rigidbody;
                        joint.anchor = transform.InverseTransformPoint(contactPoint);
                    }
                }
            }
        }
        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, hit.point);
    }
}
