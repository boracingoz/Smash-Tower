using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;

    private bool _smash;

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
        else if (Input.GetMouseButtonUp(0)) //hata çýkarsa buraya bak. 
        {
            _smash = false;
        }
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
            if (target.gameObject.tag == "enemy")
            {
                Destroy(target.transform.parent.gameObject);
            }

            if (target.gameObject.tag == "plane")
            {
                Debug.Log("Over");
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
