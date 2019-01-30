using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_wall : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        StartCoroutine(timing());
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator timing() {
        while (true) {
            Quaternion rotation;
            yield return new WaitForSeconds(Random.Range(1, 15));
            if (Random.Range(1, 101) > 50)
            {
                rotation = Quaternion.Euler(0, -90, 0) * transform.localRotation;
            } else
            {
                rotation = Quaternion.Euler(0, 90, 0) * transform.localRotation;
            }
            
            while (transform.localRotation != rotation)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, 1f * Time.deltaTime);
                yield return 0;
            }
        }
    }
}
