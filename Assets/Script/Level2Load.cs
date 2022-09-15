using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2Load : MonoBehaviour
{
    [SerializeField] Collider2D _collider;
    [SerializeField, Scene] int _sceneIndex;
    [SerializeField] Rigidbody2D _rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifier si collision est bien le joueur
        if (collision.attachedRigidbody == null) return;
        if(collision.attachedRigidbody.TryGetComponent<PlayerTag>(out var pt))
        {
            LoadLevel();
            Debug.Log("Level2Enter");   
        }
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
