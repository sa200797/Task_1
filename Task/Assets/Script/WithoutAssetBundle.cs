using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithoutAssetBundle : MonoBehaviour
{
    
    private Object[] objects;
    
    private Texture[] textures;
    
    private Material goMaterial;
 
    public int frameCounter = 1;

    public int frameRate = 30;


    void Awake()
    {
        
        goMaterial = GetComponent<Renderer>().material;
    }

    void Start()
    {
       
        this.objects = Resources.LoadAll("Sequence", typeof(Texture));

       
        this.textures = new Texture[objects.Length];

          
        for (int i = 0; i < objects.Length; i++)
        {
            this.textures[i] = (Texture)this.objects[i];
        }
    }

    void Update()
    {
        
        StartCoroutine("PlayLoop", 0.04f);
        
        goMaterial.mainTexture = textures[frameCounter];

    }

   
    IEnumerator PlayLoop(float delay)
    {
        
        yield return new WaitForSeconds(delay);

        
        frameCounter = (++frameCounter * frameRate) % textures.Length;

        
        StopCoroutine("PlayLoop");
    }

   
    IEnumerator Play(float delay)
    {
         
        yield return new WaitForSeconds(delay);

       
        if (frameCounter < textures.Length - 1)
        {
            
            ++frameCounter;
        }

        
        StopCoroutine("PlayLoop");
    }

}