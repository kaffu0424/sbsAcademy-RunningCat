﻿using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
    public Material[] materials;

	void Awake()
    {
        transform.localScale = new Vector3(Random.Range(3, 5), Random.Range(3, 10), Random.Range(3, 5));
	}
	
	void Update ()
    {
        transform.Translate(Vector3.back * Global.ScrollSpeed * Time.deltaTime);

        if (transform.position.z < -1f)
        {
            transform.localScale = new Vector3(Random.Range(3, 5), Random.Range(5, 20), Random.Range(3, 5));
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 60f);
            int result = Random.Range(0, materials.Length);
            this.GetComponent<MeshRenderer>().material = materials[result];
        }
    }
}
