using UnityEngine;
using System.Collections.Generic;

public class SpriteSwapper : MonoBehaviour {

	//0 = high
	//1 = meduim
	//2 = low
    [SerializeField] List<SpriteRenderer> sprites;

    int current = 0;
    int next = 0;
    bool swapping = false;

    public void swap(int _next)
    {
        if (!swapping)
        {
            swapping = true;
            next = _next;
        }
        else
        {
            Color tmp = sprites[current].color;
            tmp.a = 0;
            sprites[current].color = tmp;

            tmp = sprites[next].color;
            tmp.a = 1;
            sprites[next].color = tmp;
            current = next;

            swapping = true;
            next = _next;
        }
    }

	void Update ()
    {
        if (swapping)
        {
            if (sprites[current].color.a > 0)
            {
                Color tmp = sprites[current].color;
                tmp.a -= 0.05f;
                sprites[current].color = tmp;

                tmp = sprites[next].color;
                tmp.a += 0.05f;
                sprites[next].color = tmp;
            }
            else
            {
                swapping = false;
                current = next;
            }
        }
	}
}
