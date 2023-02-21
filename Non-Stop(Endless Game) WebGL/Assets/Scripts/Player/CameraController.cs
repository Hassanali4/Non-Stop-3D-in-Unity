using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;
    private Vector3 offset;
    //public float smoothSpeed = 0.125f;
    // Start is called before the first frame update
    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Start()
    {
        offset = transform.position - _target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.position + offset;
        Vector3 newPosition = new Vector3(transform.position.x,transform.position.y,offset.z + _target.position.z);
        transform.position = Vector3.Lerp(transform.position,newPosition, 10 * Time.deltaTime);
        //transform.LookAt(player);
    }
}


