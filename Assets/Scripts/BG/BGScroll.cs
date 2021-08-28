using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float scrollSpeed;
    GameObject[] BGs = new GameObject[2];
    void Start()
    {
        BGs[0] = transform.GetChild(0).gameObject;
        BGs[1] = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        BGs[0].transform.Translate(Time.deltaTime * Vector2.down * scrollSpeed);
        BGs[1].transform.Translate(Time.deltaTime * Vector2.down * scrollSpeed);

        if (BGs[0].transform.localPosition.y <= -11f)
        {
            BGs[0].transform.localPosition = new Vector3(0, 11.844f);

            GameObject temp = BGs[0];
            BGs[0] = BGs[1];
            BGs[1] = temp;
        }
    }
}
