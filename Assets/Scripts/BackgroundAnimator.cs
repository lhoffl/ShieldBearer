using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{

    public float scroll_speed = -0.01f;

    public Sprite background_1;
    public Sprite background_2;
    public Sprite background_3;
    public Sprite background_4;

    private SpriteRenderer _SprRd;

    // Start is called before the first frame update
    void Start()
    {
        _SprRd = GetComponent<SpriteRenderer>();
        switch (Random.Range(1,4))
        {
            case 1:
                _SprRd.sprite = background_1;
                break;
            case 2:
                _SprRd.sprite = background_2;
                break;
            case 3:
                _SprRd.sprite = background_3;
                break;
            case 4:
                _SprRd.sprite = background_4;
                break;
            default:
                _SprRd.sprite = background_1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(scroll_speed, 0, 0);

        if(transform.position.x < -4) transform.position = new Vector3(0,0,0);
    }

}
