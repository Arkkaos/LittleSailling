using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    static float[,,] WindCoordonneeTab;
    static float[,,] WindValueTab;

    [SerializeField]
    GameObject ArrowPrefab;

    static int mapSize;

    [SerializeField]
    float windChangeAmplitude;


    // Start is called before the first frame update
    void Start()
    {
        mapSize = 200;
        WindCoordonneeTab = new float[mapSize, mapSize, 2];
        WindValueTab = new float[mapSize, mapSize, 2];
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                WindCoordonneeTab[i, j, 0] = (float)-mapSize / 2 + i;
                WindCoordonneeTab[i, j, 1] = (float)mapSize / 2 - j;
            }
        }
        GameObject InstanstiateArrow;
        WindValueTab[0, 0, 0] = Random.Range(0f, 360);
        WindValueTab[0, 0, 1] = Random.Range(15f, 20.0f);
        InstanstiateArrow = Instantiate(ArrowPrefab, new Vector3(WindCoordonneeTab[0, 0, 0], WindCoordonneeTab[0, 0, 1], 0), Quaternion.Euler(0, 0, WindValueTab[0, 0, 0]), transform);
        InstanstiateArrow.transform.localScale *= WindValueTab[0, 0, 1] / 10;
        for (int a = 1; a < mapSize; a++)
        {
            for (int b = 0; b < a; b++)
            {
                if (b == 0)
                {
                    WindValueTab[b, a, 0] = WindValueTab[b, a - 1, 0] + Random.Range(-180 / 12, 180 / 12);
                    WindValueTab[b, a, 1] = WindValueTab[b, a - 1, 1] + Random.Range(-windChangeAmplitude, windChangeAmplitude);
                    WindValueTab[a, b, 0] = WindValueTab[a - 1, b, 0] + Random.Range(-180 / 12, 180 / 12);
                    WindValueTab[a, b, 1] = WindValueTab[a - 1, b, 1] + Random.Range(-windChangeAmplitude, windChangeAmplitude);
                }
                else
                {
                    WindValueTab[b, a, 0] = (WindValueTab[b - 1, a, 0] + WindValueTab[b, a - 1, 0]) / 2 + Random.Range(-180 / 12, 180 / 12);
                    WindValueTab[b, a, 1] = (WindValueTab[b - 1, a, 1] + WindValueTab[b, a - 1, 1]) / 2 + Random.Range(-windChangeAmplitude, windChangeAmplitude);
                    WindValueTab[a, b, 0] = (WindValueTab[a - 1, b, 0] + WindValueTab[a, b - 1, 0]) / 2 + Random.Range(-180 / 12, 180 / 12);
                    WindValueTab[a, b, 1] = (WindValueTab[a - 1, b, 1] + WindValueTab[a, b - 1, 1]) / 2 + Random.Range(-windChangeAmplitude, windChangeAmplitude);
                }
                InstanstiateArrow = Instantiate(ArrowPrefab, new Vector3(WindCoordonneeTab[a, b, 0], WindCoordonneeTab[a, b, 1], 0), Quaternion.Euler(0, 0, WindValueTab[a, b, 0]), transform);
                InstanstiateArrow.transform.localScale *= WindValueTab[a, b, 1] / 10;
                InstanstiateArrow = Instantiate(ArrowPrefab, new Vector3(WindCoordonneeTab[b, a, 0], WindCoordonneeTab[b, a, 1], 0), Quaternion.Euler(0, 0, WindValueTab[b, a, 0]), transform);
                InstanstiateArrow.transform.localScale *= WindValueTab[b, a, 1] / 10;
            }
            WindValueTab[a, a, 0] = (WindValueTab[a - 1, a, 0] + WindValueTab[a, a - 1, 0]) / 2 + Random.Range(-180 / 12, 180 / 12);
            WindValueTab[a, a, 1] = (WindValueTab[a - 1, a, 1] + WindValueTab[a, a - 1, 1]) / 2 + Random.Range(-windChangeAmplitude, windChangeAmplitude);
            InstanstiateArrow = Instantiate(ArrowPrefab, new Vector3(WindCoordonneeTab[a, a, 0], WindCoordonneeTab[a, a, 1], 0), Quaternion.Euler(0, 0, WindValueTab[a, a, 0]), transform);
            InstanstiateArrow.transform.localScale *= WindValueTab[a, a, 1] / 10;


        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public static float getWindAngle(Vector3 position)
    {
        return WindValueTab[(int)Mathf.Round(position.x) + (mapSize / 2), -(int)Mathf.Round(position.y) + (mapSize / 2), 0];
    }
    public static float getWindForce(Vector3 position)
    {
        return WindValueTab[(int)Mathf.Round(position.x) + (mapSize / 2), (int)-Mathf.Round(position.y) + (mapSize / 2), 1];
    }
}