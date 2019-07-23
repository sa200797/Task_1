using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithAssetBundle : MonoBehaviour
{
    //https://drive.google.com/uc?export=download&id=17njmDvRo1BT9CsvzFDGzLiHdbcRsL_0w

    public string url = "";
    public bool mClearCache;

    private Object[] objects;
    private Texture[] textures;
    private Material goMaterial;

    private AssetBundle assetBundle;

    public int frameCounter = 1;

    public int frameRate = 30;

    private void Awake()
    {
        goMaterial = GetComponent<Renderer>().material;
        Caching.compressionEnabled = true;


    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadandPlay());


    }

    // Update is called once per frame
    void Update()
    {

        //Set the material's texture to the current value of the frameCounter variable  
        StartCoroutine("PlayLoop", 0.04f);
        goMaterial.mainTexture = textures[frameCounter];
        Debug.Log(goMaterial.name);
    }

    private IEnumerator DownloadandPlay()
    {
        yield return GetBundle();

        if (!assetBundle)
        {
            Debug.Log("Bundle failed to load");
            yield break;

        }
        //TO Do here
       // AssetBundleRequest request = assetBundle.LoadAssetAsync<Texture>("Resources/Sequence");

       // this.objects = Resources.LoadAll("Sequence", typeof(Texture));

        this.objects = assetBundle.LoadAllAssets();

        //Initialize the array of textures with the same size as the objects array  
        this.textures = new Texture[objects.Length];

        //Cast each Object to Texture and store the result inside the Textures array  
        for (int i = 0; i < objects.Length; i++)
        {
            this.textures[i] = (Texture)this.objects[i];
        }

    }
    private IEnumerator GetBundle()
    {

        WWW request = WWW.LoadFromCacheOrDownload(url, 0);


        while (!request.isDone)
        {
            Debug.Log(request.progress);
            yield return null;

        }
        if (request.error == null)
        {
            assetBundle = request.assetBundle;
            Debug.Log("Success");

        }
        else
        {
            Debug.Log(request.error);

        }


    }


    IEnumerator PlayLoop(float delay)
    {
        //Wait for the time defined at the delay parameter  
        yield return new WaitForSeconds(delay);

        //Advance one frame  
        frameCounter = (++frameCounter * frameRate) % textures.Length;

        //Stop this coroutine  
        StopCoroutine("PlayLoop");
    }
}
