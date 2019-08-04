using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionVariant : MonoBehaviour
{

    public float min_size = 1.2f;

    public float max_size = 1.7f;

    // Start is called before the first frame update
    void Start()
    {
        float size = Random.Range(min_size, max_size);

        transform.localScale = new Vector3(size, size, 1);

        //float rotation = Random.Range(-180, 180);

        //transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
