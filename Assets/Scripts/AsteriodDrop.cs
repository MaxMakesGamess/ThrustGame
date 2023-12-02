using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodDrop : MonoBehaviour
{
    UnityEngine.Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "OutOfBounds"){
           transform.position = startPos;
        }
    }
}
