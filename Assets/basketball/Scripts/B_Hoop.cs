using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Hoop : MonoBehaviour
{
    BoxCollider2D netCollider;
    // Start is called before the first frame update
    void Start()
    {
        netCollider = this.gameObject.transform.GetChild(0).transform.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
