using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Text _totalScoreLabel;
    int totalScore = 0;
    
    public GameObject chaoPrefab;
    List<GameObject> chaoList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject firstChao = Instantiate(chaoPrefab, new Vector3(1113, 711, 0), Quaternion.identity);
        chaoList.Add(firstChao);
        Debug.Log(firstChao.GetComponent<ChaoController>().test());
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
                Debug.Log("We hit:  " + hit.collider.name);
                hitResult(hit);
                
            }
        }
    }

    void hitResult(RaycastHit2D hit)
    {
        if (hit.collider.name == "playButton")
        {
            SceneManager.LoadScene("ClikcerAreaScene");
            StartCoroutine(LoadYourAsyncScene());
        }
        else if (hit.collider.name.Substring(0,4) == "chao")
        {
            Jump(hit);
        }
        else if (hit.collider.name == "blackmarketicon")
        {
            SceneManager.LoadScene("BlackMarketScene");  
        }

    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // You could also load the Scene by using sceneBuildIndex.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ClikcerAreaScene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
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
        var pointMultiplier = chaoList[0].GetComponent("ChaoController");
        Debug.Log(chaoList[0]);
        totalScore += 1;
        _totalScoreLabel.text = "" + totalScore;
    }
}
