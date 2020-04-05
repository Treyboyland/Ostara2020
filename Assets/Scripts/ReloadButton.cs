using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ReloadButton : MonoBehaviour
{
    [Tooltip("Must match path of scene relative to 'Assets' folder")]
    [SerializeField]
    string sceneName;

    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ReloadGame);
    }

    void ReloadGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
