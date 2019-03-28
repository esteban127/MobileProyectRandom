using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class touchTestScript : MonoBehaviour
{
    [SerializeField]GameObject testSprite;
    
    // Update is called once per frame
    void Update()
    {

        
        for (int i = 0; i < Input.touches.Length; i++)
        {
            Debug.Log("no me toques ahi sempai");
            if (Input.touches[i].phase == TouchPhase.Moved)
            {
                GameObject instance = Instantiate(testSprite);               
                Vector3 screenPoint = new Vector3(Input.touches[i].position.x, Input.touches[i].position.y, 10.0f);
                instance.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            }
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                GameObject instance = Instantiate(testSprite);
                Vector3 screenPoint = new Vector3(Input.touches[i].position.x, Input.touches[i].position.y, 10.0f);
                instance.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            }
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            GameObject instance = Instantiate(testSprite);
            Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            instance.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }*/
    }
}
