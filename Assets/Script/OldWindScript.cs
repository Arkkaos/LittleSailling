using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWindScript : MonoBehaviour
{
    static float[,,] WindCoordonneeTab;
    static Vector3[,] WindValueTab;

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
        WindValueTab = new Vector3[mapSize,mapSize];
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                WindCoordonneeTab[i,j,0] = (float) -mapSize / 2 + i;
                WindCoordonneeTab[i,j,1] = (float) mapSize / 2 - j;
            }
        }
        GameObject InstanstiateArrow;
        WindValueTab[0,0] = createVector(Random.Range(0f, 360), Random.Range(15f, 20.0f));
        InstanstiateArrow = Instantiate(ArrowPrefab, new Vector3(WindCoordonneeTab[0, 0, 0], WindCoordonneeTab[0, 0, 1], 0), Quaternion.Euler(WindValueTab[0, 0]), transform);
        InstanstiateArrow.transform.localScale *= WindValueTab[0, 0].magnitude / 50;
        for (int a = 1; a < mapSize; a++)
        {
            for (int b = 0; b < a; b++)
            {
                if (b == 0)
                {
                    WindValueTab[b, a] = createVector(Vector3.Angle(WindValueTab[b, a - 1], Vector3.right) + Random.Range(-180 / 12, 180 / 12), WindValueTab[b, a - 1].magnitude + Random.Range(-windChangeAmplitude, windChangeAmplitude));
                    WindValueTab[a, b] = createVector(Vector3.Angle(WindValueTab[a- 1, b], Vector3.right) + Random.Range(-180 / 12, 180 / 12), WindValueTab[a - 1, b].magnitude + Random.Range(-windChangeAmplitude, windChangeAmplitude));

                }
                else
                {
                    WindValueTab[b, a] = createVector((Vector3.Angle(WindValueTab[b, a - 1], Vector3.right) + Vector3.Angle(WindValueTab[b - 1, a], Vector3.right)) / 2 + Random.Range(-180 / 12, 180 / 12), (WindValueTab[b, a - 1].magnitude + WindValueTab[b - 1, a].magnitude) + Random.Range(-windChangeAmplitude, windChangeAmplitude));
                    WindValueTab[a, b] = createVector((Vector3.Angle(WindValueTab[a, b - 1], Vector3.right) + Vector3.Angle(WindValueTab[a - 1, b], Vector3.right)) / 2 + Random.Range(-180 / 12, 180 / 12), (WindValueTab[a, b - 1].magnitude + WindValueTab[a - 1, b].magnitude) + Random.Range(-windChangeAmplitude, windChangeAmplitude));
                }
                InstanstiateArrow = Instantiate(ArrowPrefab, new Vector3(WindCoordonneeTab[a, b, 0], WindCoordonneeTab[a, b, 1], 0), Quaternion.Euler(WindValueTab[a, b]), transform);
                InstanstiateArrow.transform.localScale *= WindValueTab[a, b].magnitude / 50;
                InstanstiateArrow = Instantiate(ArrowPrefab, new Vector3(WindCoordonneeTab[b, a, 0], WindCoordonneeTab[b, a, 1], 0), Quaternion.Euler(WindValueTab[b, a]), transform);
                InstanstiateArrow.transform.localScale *= WindValueTab[b, a].magnitude / 50;
            }
            WindValueTab[a, a] = createVector((Vector3.Angle(WindValueTab[a, a - 1], Vector3.right) + Vector3.Angle(WindValueTab[a - 1, a], Vector3.right)) / 2 + Random.Range(-180 / 12, 180 / 12), (WindValueTab[a, a - 1].magnitude + WindValueTab[a - 1, a].magnitude) + Random.Range(-windChangeAmplitude, windChangeAmplitude));

            InstanstiateArrow = Instantiate(ArrowPrefab, new Vector3(WindCoordonneeTab[a, a, 0], WindCoordonneeTab[a, a, 1], 0), Quaternion.Euler(WindValueTab[a, a]), transform);
            InstanstiateArrow.transform.localScale *= WindValueTab[a, a].magnitude / 50;


        }
    }

    // Update is called once per frame
    void Update() {


    }
    Vector3 createVector(float angle, float magnitude)
    {

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
        Vector3 vec = rotation * new Vector3(magnitude, 0, 0);
        return vec;
    }
    public static Vector3 getWind(Vector3 position)
    {
        return WindValueTab[(int)Mathf.Round(position.x) + (mapSize / 2), -(int)Mathf.Round(position.y) + (mapSize / 2)];
    }
}
