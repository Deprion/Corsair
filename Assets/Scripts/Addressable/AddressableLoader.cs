using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoader : MonoBehaviour
{
    public static AddressableLoader inst { get; private set; }

    public static SimpleEvent Loaded = new SimpleEvent();

    private string key = "language";
    private AsyncOperationHandle<IList<TextAsset>> handle;

    private Dictionary<string, TextAsset> obj = new Dictionary<string, TextAsset>();

    public TextAsset GetLanguage(string lang)
    {
        return obj[lang];
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        inst = this;
    }

    private IEnumerator Start()
    {
        handle = Addressables.LoadAssetsAsync<TextAsset>(
            key,
            addressable => {} );

        yield return handle;

        foreach (var item in handle.Result)
        {
            obj.Add(item.name, item);
        }

        Loaded.Invoke();
    }

    private void OnDestroy()
    {
        Addressables.Release(handle);
    }
}
