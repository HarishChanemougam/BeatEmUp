using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelBehaviour : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField, Scene] int _sceneIndex;

    private void Start()
    {
        _button.onClick.AddListener(LoadLevel);
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadAsynchronously(_sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }

}