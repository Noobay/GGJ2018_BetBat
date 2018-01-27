using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Use this for initialization
    public void Update()
    {
         if(Input.touches.Length > 0 || Input.GetButton("Fire1"))
        {
            Debug.Log("touched");
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
