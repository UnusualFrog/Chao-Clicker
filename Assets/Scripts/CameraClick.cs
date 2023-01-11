using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraClick : MonoBehaviour
{
    [SerializeField] 
    private Text _totalScoreLabel;
    int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       check2DObjectClicked();
    }

    void check2DObjectClicked()
    {
    if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Mouse is pressed down");
            Camera cam = Camera.main;

            //Raycast depends on camera projection mode
            Vector2 origin = Vector2.zero;
            Vector2 dir = Vector2.zero;

            if (cam.orthographic)
            {
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                origin = ray.origin;
                dir = ray.direction;
            }

            RaycastHit2D hit = Physics2D.Raycast(origin, dir);

            //Check if we hit anything
            if (hit)
            {
                Debug.Log("We hit " + hit.collider.name);
                if (hit.collider.name == "chao front facing")
                {
                    Jump(hit);
                }
                else if (hit.collider.name == "blackmarketicon")
                {
                    SceneManager.LoadScene("BlackMarketScene");
                }
                
            }
        }
    }

    
    private void Jump(RaycastHit2D target) {
        target.transform.position += new Vector3(0,100,0);
        StartCoroutine(DelayDown(target));
    }

    IEnumerator DelayDown(RaycastHit2D target) 
    {
        yield return new WaitForSeconds(0.15f);
        target.transform.position -= new Vector3(0,100,0);
        totalScore += 1;
        _totalScoreLabel.text = "" + totalScore;
    }
}
