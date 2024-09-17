using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    private float _curTime;

    private bool _smash, _invincible;

    public enum BallState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }

    [HideInInspector]
    public BallState ballState = BallState.Prepare;

    public AudioClip bounceOffClip, deadClip, winClip, destroyClip, iDestroyClip;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (ballState == BallState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _smash = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _smash = false;
            }

            if (_invincible)
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
                _invincible = true;
            }
            else if (_curTime <= 0)
            {
                _curTime = 0;
                _invincible = false;
            }
        }

        if (ballState == BallState.Prepare)
        {
            ballState = BallState.Playing;
        }

        if (ballState == BallState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }
        }
    }

    private void FixedUpdate()
    {
        if (ballState == BallState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _smash = true;
                _rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }

        if (_rb.velocity.y > 5 )
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 5, _rb.velocity.z);
        }
    }

    public void IncreaseBrokenStacks()
    {
        if (ScoreManager.instance == null)
        {
            Debug.LogError("ScoreManager instance is null!");
            return;
        }

        if (SoundManager.instance == null)
        {
            Debug.LogError("SoundManager instance is null!");
            return;
        }

        if (!_invincible)
        {
            ScoreManager.instance.AddScore(1);
            SoundManager.instance.PlaySoundFX(destroyClip, 0.5f);
        }
        else
        {
            ScoreManager.instance.AddScore(2);
            SoundManager.instance.PlaySoundFX(iDestroyClip, 0.5f);
        }
    }

    private void OnCollisionEnter(Collision target)
    {
        if (SoundManager.instance == null)
        {
            Debug.LogError("SoundManager instance is null!");
            return;
        }

        if (!_smash)
        {
            _rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager.instance.PlaySoundFX(bounceOffClip, 0.5f);
        }
        else
        {
            if (_invincible)
            {
                if (target.gameObject.CompareTag("enemy") || target.gameObject.CompareTag("plane"))
                {
                    ShatterStack(target);
                }
            }
            else
            {
                if (target.gameObject.CompareTag("enemy"))
                {
                    ShatterStack(target);
                }

                if (target.gameObject.CompareTag("plane"))
                {
                    HandleGameOver();
                }
            }
        }

        if (target.gameObject.CompareTag("Finish") && ballState == BallState.Playing)
        {
            HandleFinish();
        }
    }

    private void ShatterStack(Collision target)
    {
        StackController stackController = target.transform.parent.GetComponent<StackController>();
        if (stackController != null)
        {
            stackController.ShatterAllParts();
        }
        else
        {
            Debug.LogError("StackController not found on parent of collided object!");
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over!");
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetScore();
        }
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlaySoundFX(deadClip, 0.5f);
        }
    }

    private void HandleFinish()
    {
        ballState = BallState.Finish;
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlaySoundFX(winClip, 0.7f);
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
