using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(HingeJoint))]

public class PlayerController : MonoBehaviour
{
    public string touchableTag = "Touchable";

    LineRenderer lineRenderer;
    HingeJoint joint;

    Ray ray = new Ray();
    RaycastHit hit = new RaycastHit();

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        joint = GetComponent<HingeJoint>();
        //joint.anchor = Vector3.zero;
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
                    var contactPoint = hit.point;
                    joint.anchor = transform.InverseTransformPoint(contactPoint);
                }
            }
        }
        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, hit.point);
    }
}
