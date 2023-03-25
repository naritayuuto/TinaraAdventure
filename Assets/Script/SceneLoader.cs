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
    [SerializeField,Header("暗転に利用するパネル")]
    Image _fadePanel = null;
    [SerializeField, Header("暗転する速度"), Tooltip("暗転する速度")]
    float _fadeSpeed = 1f;
    [Tooltip("遷移可能な場合True")]
    bool _isLoaded = false;
    [Tooltip("関数内で決める遷移先の名前")]
    string _sceneName = "";
    [Tooltip("遷移可能なシーンの名前一覧")]
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
                Debug.Log("遷移完了");
            }
            else
            {
                SceneManager.LoadScene(_sceneName);
                _isLoaded = false;
                Debug.Log("パネルなし、遷移完了");
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
            Debug.LogError("遷移不可能なシーン名が指定されています、BuildSettingsを確認してください");
        }
    }
}
