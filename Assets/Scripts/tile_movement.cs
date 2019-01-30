using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tile_movement : MonoBehaviour {

    public GameObject pickup_prefab;
    public GameObject wall_prefab;

    public Vector3 current_rotation;
    public float rotation_z = 0f;
    public float rotation_x = 0f;
    public float angle_limit = 10f;

    private List<GameObject> walls = new List<GameObject>();
    private List<GameObject> pickups = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for (int i = -4; i < 5; i++)
        {
            for (int j = -4; j < 5; j++)
            {
                if(i == 0 && j == 0)
                {
                    continue;
                }

                if (((i + j) % 2) == 0)
                {
                    walls.Add(Instantiate(wall_prefab, new Vector3(i, 0, j), generate_rand_direction()));
                }
            }
        }

        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].transform.parent = transform;
        }

        //Generate 6 random positions
        for (int i = 0; i < 6; i++)
        {
            float x = Random.Range(-5, 5);
            float y = Random.Range(-5, 5);
            while (x == 5 && y == 5)
            {
                x = Random.Range(-5, 5);
                y = Random.Range(-5, 5);
            }

            pickups.Add(Instantiate(pickup_prefab, new Vector3(x + 0.5f, 0, y + 0.5f), Quaternion.identity));
        }

        for (int i = 0; i < pickups.Count; i++)
        {
            pickups[i].transform.parent = transform;
        }
    }

    Quaternion generate_rand_direction()
    {
        int r = Random.Range(0, 4);
        if (r == 0)
        {
            return Quaternion.Euler(0, 0, 0);
        }
        else if (r == 1)
        {
            return Quaternion.Euler(0, 90, 0);
        }
        else if (r == 2)
        {
            return Quaternion.Euler(0, 180, 0);
        }
        else
        {
            return Quaternion.Euler(0, 270, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        current_rotation = GetComponent<Transform>().eulerAngles;

		if ((Input.GetAxis("Horizontal") > 0.2) && rotation_z >= -angle_limit)
        {
            transform.Rotate(0,0,-1);
            rotation_z += -1;
        }

        if ((Input.GetAxis("Horizontal") < -0.2) && rotation_z <= angle_limit)
        {
            transform.Rotate(0,0,1);
            rotation_z += 1;
        }

        if ((Input.GetAxis("Vertical") > 0.2) && rotation_x <= angle_limit)
        {
            transform.Rotate(1, 0, 0);
            rotation_x += 1;
        }

        if ((Input.GetAxis("Vertical") < -0.2) && rotation_x >= -angle_limit)
        {
            transform.Rotate(-1, 0, 0);
            rotation_x += -1;
        }

        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -25, 25);
        transform.localRotation = Quaternion.Euler(currentRotation);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("maze0");
        }

        if (Input.GetKey("escape") || Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }
}
