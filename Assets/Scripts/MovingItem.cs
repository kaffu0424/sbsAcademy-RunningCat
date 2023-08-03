using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{
    public bool posInit;
    void Update()
    {
        transform.Translate(Vector3.back * Global.ScrollSpeed * Time.deltaTime);

        if(transform.position.z < -1f)
        {
            if ( posInit )
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 60f);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
