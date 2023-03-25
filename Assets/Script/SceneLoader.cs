using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

public class SceneLoader : MonoBehaviour
{
    [SerializeField,Header("�Ó]�ɗ��p����p�l��")]
    Image _fadePanel = null;
    [SerializeField, Header("�Ó]���鑬�x"), Tooltip("�Ó]���鑬�x")]
    float _fadeSpeed = 1f;
    [Tooltip("�J�ډ\�ȏꍇTrue")]
    bool _isLoaded = false;
    [Tooltip("�֐����Ō��߂�J�ڐ�̖��O")]
    string _sceneName = "";
    [Tooltip("�J�ډ\�ȃV�[���̖��O�ꗗ")]
    List<string> _sceneNames;

    // Start is called before the first frame update
    void Start()
    {
        _sceneNames = EditorBuildSettings.scenes
                                  .Where(scene => scene.enabled)
                                  .Select(scene => Path.GetFileNameWithoutExtension(scene.path))
                                  .ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isLoaded)
        {
            if (_fadePanel)
            {
                _fadePanel.DOColor(Color.black, _fadeSpeed).OnComplete(() => SceneManager.LoadScene(_sceneName));
                _isLoaded = false;
                Debug.Log("�J�ڊ���");
            }
            else
            {
                SceneManager.LoadScene(_sceneName);
                _isLoaded = false;
                Debug.Log("�p�l���Ȃ��A�J�ڊ���");
            }
        }
    }
    public void LoadScene(string sceneName)
    {
        _sceneName = sceneName;
        foreach(var name in _sceneNames)
        {
            _isLoaded = name == _sceneName ? true : false;
        }
        if(!_isLoaded)
        {
            Debug.LogError("�J�ڕs�\�ȃV�[�������w�肳��Ă��܂��ABuildSettings���m�F���Ă�������");
        }
    }
}
