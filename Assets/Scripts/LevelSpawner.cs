using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] model;
    public GameObject pipePrefab;
    [HideInInspector]
    public GameObject[] modelPrefab = new GameObject[4];
    public GameObject winPrefab;

    private GameObject _temp1, _temp2;

    private const float SCALE_INCREMENT_Y = 1.08f;
    private const float INITIAL_SCALE_Y = 10.2f;
    private const float INITIAL_SCALE_XZ = 1.2f;


    public int level = 1, addOn = 7;
    private float i = 0;
    // Start is called before the first frame update
    void Awake()
    {
        level = PlayerPrefs.GetInt("level", 1);

        Vector3 pipePos = new Vector3(0, -1.675853f, 0.7320552f);
        GameObject pipe = Instantiate(pipePrefab, pipePos, Quaternion.identity);
        float scaleY = INITIAL_SCALE_Y + (level - 1) * SCALE_INCREMENT_Y;
        pipe.transform.localScale = new Vector3(INITIAL_SCALE_XZ, scaleY, INITIAL_SCALE_XZ);

        if (level > 9)
        {
            addOn = 0;
        }

        ModelSelection();
        float random = Random.value;
        for (i = 0; i > -level - addOn; i-= 0.5f)
        {
            if (level <= 20)
            {
                _temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]);
            }
            if (level > 20 && level <= 50)
            {
                _temp1 = Instantiate(modelPrefab[Random.Range(1, 3)]);
            }
            if (level > 50 && level <= 100)
            {
                _temp1 = Instantiate(modelPrefab[Random.Range(2, 4)]);
            }
            if (level > 100)
            {
                _temp1 = Instantiate(modelPrefab[Random.Range(3, 4)]);
            }

            _temp1.transform.position = new Vector3(0, i-0.01f,0);
            _temp1.transform.eulerAngles = new Vector3(0, i*8,0);

            if (Mathf.Abs(i) >= level * .3f && Mathf.Abs(i) <= level * .6f)
            {
                _temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                _temp1.transform.eulerAngles += Vector3.up * 180;
            }else if (Mathf.Abs(i) >= level * .8f)
            {
                _temp1.transform.eulerAngles += new Vector3(0, i * 8, 0);

                if (random > .75f)
                {
                    _temp1.transform.eulerAngles += Vector3.up * 180;
                }
            }

            _temp1.transform.parent = FindObjectOfType<Rotator>().transform;

        }

        _temp2 = Instantiate(winPrefab);
        _temp2.transform.position = new Vector3(0,i - 01f,0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void ModelSelection()
    {
        int randomModel = Random.Range(0, 5);
        switch (randomModel)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = model[i];
                }
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = model[i+4];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = model[i+8];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = model[i+12];
                }
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefab[i] = model[i+16];
                }
                break;
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }
}
