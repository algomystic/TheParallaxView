using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildHologram : MonoBehaviour
{

    int repeat_x = 10;
    int repeat_y = 10;
    int repeat_z = 7;

    public GameObject holoPlaneX;
    public GameObject holoPlaneY;
    public GameObject holoPlaneZ;
    public GameObject holoCube;

    // Start is called before the first frame update
    void Start()
    {


        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer rend;


        
        for (int x = 1; x < repeat_x-1; x++)
        {
            // create x plane
            Vector3 pos = new Vector3(x - 9f / 2f, 0, -9f / 2f);
            GameObject plane = Instantiate(holoPlaneX, pos, Quaternion.AngleAxis(-90, Vector3.forward), this.transform);
            plane.transform.localPosition = pos;
            plane.transform.localScale = new Vector3(7, 7, 9);

            Color col = new Color(0.8584906f, 0.8584906f, 0.8584906f, 0.1411765f);

            props.SetColor("_Color", col);

            rend = plane.GetComponentInChildren<MeshRenderer>();
            rend.SetPropertyBlock(props);
        }

        
        for (int y = 1; y < repeat_y-1; y++)
        {
            // create y plane
            Vector3 pos = new Vector3(0, y - 9f / 2f, -9f / 2f);
            GameObject plane = Instantiate(holoPlaneY, pos, Quaternion.identity, this.transform);
            plane.transform.localPosition = pos;
            plane.transform.localScale = new Vector3(7, 7, 9);

            Color col = new Color(0.8584906f, 0.8584906f, 0.8584906f, 0.1411765f);

            props.SetColor("_Color", col);

            rend = plane.GetComponentInChildren<MeshRenderer>();
            rend.SetPropertyBlock(props);
        }
        

        /*
        
        for (int z = 1; z < repeat_z; z++)
        {
            // create z plane
            Vector3 pos = new Vector3(0, 0, -z);
            GameObject plane = Instantiate(holoPlaneZ, pos, Quaternion.AngleAxis(-90, Vector3.left), this.transform);
            plane.transform.localPosition = pos;
            plane.transform.localScale = new Vector3(7, 7, 7);

            Color col = 0.2f* Color.magenta * (10f - z) / 9f;
            col.a = 0.05f;

            //Material mat = plane.GetComponentInChildren<Renderer>().material;
            //mat.color = col;
            //mat.SetColor("_EmissionColor", 0.08f * col);

            props.SetColor("_Color", col);

            rend = plane.GetComponentInChildren<MeshRenderer>();
            rend.SetPropertyBlock(props);

        }
        
       */
        
       
        for (int x = 1; x < repeat_x - 2; x++)
        {
            for (int y = 1; y < repeat_y - 2; y++)
            {
                for (int z = 0; z < repeat_z - 1; z++)
                {
                    float dist_x = Mathf.Abs((float)x - 4f) * 0.3333f; // 0..1
                    float dist_y = Mathf.Abs((float)y - 4f) * 0.3333f; // 0..1
                    float dist_z = z / 5f; // 0..1
                    float dist = 0.3333333f * (dist_x + dist_y + dist_z); // 0..1

                    //float cull = dist_x + dist_y - dist_z;
                    //if (cull > 0.7) continue;

                    Vector3 pos = new Vector3(x - 8f / 2f, y - 8f / 2f, -z - 0.5f);
                    GameObject cube = Instantiate(holoCube, pos, Quaternion.identity, this.transform);
                    cube.transform.localPosition = pos;
                    cube.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

                    Color col = new Color(1f - dist_z, 0.9f*( 1f - Mathf.Max(dist * 2f, dist_z)), 0f, 0.3f*(1f-dist_z));

                    //Material mat = cube.GetComponentInChildren<Renderer>().material;
                    //mat.color = col;
                    //mat.SetColor("_EmissionColor", (0.5f - 0.5f*dist_z) * col);

                    props.SetColor("_Color", col);

                    rend = cube.GetComponentInChildren<MeshRenderer>();
                    rend.SetPropertyBlock(props);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
