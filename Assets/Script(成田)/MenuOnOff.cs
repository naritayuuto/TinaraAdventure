using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOnOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetButtonDown("Fire3"))//esc
        //{
        //    Debug.Log("関数を呼んだ");
        //    CanvasOnOff();
        //}
    }

    public void CanvasOnOff(GameObject canvas)
    {
        if (!canvas.activeSelf)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
