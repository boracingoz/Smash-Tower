using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    private float _curTime;

    private bool _smash, _invicible;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _smash = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _smash = false;          
        }

        if (_invicible)
        {
            _curTime -= Time.deltaTime * .35f;

        }
        else
        {
            if (_smash)
            {
                _curTime += Time.deltaTime * .8f;

            }
            else
            {
                _curTime -= Time.deltaTime * .5f;

            }
        }

        if (_curTime >= 1)
        {
            _curTime = 1;
            _invicible = true;
        }
        else if (_curTime <= 0)
        {
            _curTime = 0;
            _invicible = false;
        }
        print(_invicible);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _smash = true;
            _rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
        }

        if (_rb.velocity.y > 5 )
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 5, _rb.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision target)
    {
        if (!_smash)
        {
         _rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        else
        {
            if (_invicible)
            {
                if (target.gameObject.tag == "enemy" || target.gameObject.tag == "plane")
                {
                    Destroy(target.transform.parent.gameObject);
                }
            }
            else
            {
                if (target.gameObject.tag == "enemy")
                {
                    Destroy(target.transform.parent.gameObject);
                }

                if (target.gameObject.tag == "plane")
                {
                    Debug.Log("Over!");
                }
            }
        }
    }

    private void OnCollisionStay(Collision target)
    {
        if (!_smash || target.gameObject.tag == "Finish")
        {
            _rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}
