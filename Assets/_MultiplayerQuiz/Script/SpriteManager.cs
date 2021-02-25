using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;
    private Sprite[] profileSprites= new Sprite[0];

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        LoadProfileSprite();
    }

    public Sprite GetRandomProfileSprite()
    {
        Sprite result = null;
        if (profileSprites.Length > 0)
        {
            var randomIndex = Random.Range(0, profileSprites.Length-1);
            result = profileSprites[randomIndex];
        }
        return result;
    }

    /// <summary>
    /// Oyuncu Profile Resimleri Yükleniyor .
    /// </summary>
    private void LoadProfileSprite()
    {
        if (profileSprites?.Length == 0)
        {
            profileSprites = Resources.LoadAll<Sprite>("ProfileSprite");
        }
    }


}
