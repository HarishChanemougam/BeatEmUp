using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField] Collider2D _ignoreSelf;

    [ShowNonSerializedField] List<Health> _detectedHealths;
    public IEnumerable<Health> DetectedHealths => _detectedHealths;

    void Awake()
    {
        _detectedHealths = new List<Health>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Guards
        if (collision == _ignoreSelf) return;

        if (collision.TryGetComponent<Health>(out var h))
        {
            if (_detectedHealths.Contains(h)) return;
            _detectedHealths.Add(h);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Guards
        if (collision == _ignoreSelf) return;
        if (collision.TryGetComponent<Health>(out var h))
        {
            if (_detectedHealths.Contains(h) == false) return;
            _detectedHealths.Remove(h);
        }
    }

    #region EDITOR
#if UNITY_EDITOR
    private void Reset()
    {
        _ignoreSelf = GetComponent<Collider2D>();
    }
#endif
    #endregion

}
