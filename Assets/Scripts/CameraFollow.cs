using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _camFollow;
    private Transform _ball, _win;

    private void Awake()
    {
        _ball = FindAnyObjectByType<Ball>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_win == null)
        {
            _win = GameObject.Find("Win(Clone)").GetComponent<Transform>();
        }

        if (transform.position.y > _ball.transform.position.y && transform.position.y > _win.position.y +4f)
        {
            _camFollow = new Vector3(transform.position.x, _ball.position.y, transform.position.z);
        }
        transform.position = new Vector3(transform.position.x, _camFollow.y, -5f);
    }
}
