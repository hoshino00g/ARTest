using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class LoadAssetBundle : MonoBehaviour
{
    AssetBundle assetBundle;

    void Start()
    {
        // AssetBundleの配置場所
        var path = "/Users/hoshinohiroto/Desktop/ARTest/AssetBundles/StandaloneOSXUniversal/head_small";
        // ヘッダ情報だけメモリ上に読まれる
        assetBundle = AssetBundle.LoadFromFile(path);
        if (assetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        print(assetBundle);
        GameObject prefab = assetBundle.LoadAsset<GameObject>("head_small");//ここ名前を合わせなければいけない
        print(prefab);
        Instantiate(prefab);
    }
}