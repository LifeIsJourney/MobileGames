using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereControl : MonoBehaviour
{
    private GameObject targetObject = null;
    private Scene _scene;

    void Start()
    {
        targetObject = GameObject.Find("CubeTwo");
        Debug.Log(targetObject.transform.position);
        _scene = SceneManager.GetActiveScene();
    }
    
    void Update()
    {
        targetObject = GameObject.Find("CubeTwo");

        transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, 0.3f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "CubeTwo")
        {
            Debug.Log("Score Stopped and Reset Level");
            SceneManager.LoadScene(_scene.name);
        }
    }
}
