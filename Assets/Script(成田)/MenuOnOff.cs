using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOnOff : MonoBehaviour
{
    bool menu = false;
    [SerializeField]
    GameObject canvas = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))//esc
        {
            Debug.Log("osareta");
            if (!menu)
            {
                canvas.SetActive(true);
                menu = true;
            }
            else
            {
                canvas.SetActive(false);
                menu = false;
            }
        }
    }
}
