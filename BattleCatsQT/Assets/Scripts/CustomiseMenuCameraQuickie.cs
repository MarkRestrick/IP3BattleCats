using UnityEngine;
using System.Collections;

public class CustomiseMenuCameraQuickie : MonoBehaviour
{
    public Transform startPos;
    public Transform custPos;

    public bool toStart;
    public bool toCust;

    public float moveSpeed;
    public float rotateSpeed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (toStart)
        {
            float moveStep = moveSpeed * Time.deltaTime;
            float step = rotateSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPos.position, moveStep);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startPos.rotation, step);
        }

        if (toCust)
        {
            float moveStep = moveSpeed * Time.deltaTime;
            float step = rotateSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, custPos.position, moveStep);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, custPos.rotation, step);
        }
    }

    public void Customise()
    {
        toStart = false;
        toCust = true;
    }

    public void StartPosition()
    {
        toStart = true;
        toCust = false;
    }
}
