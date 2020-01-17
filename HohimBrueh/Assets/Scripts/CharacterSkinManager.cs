using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<TongueSkin> tongues;

    public Texture getLineSkinFor(TongueType type) {
        TongueSkin skin = tongues.Find(x => x.type == type);
        if(skin != null) {
            return skin.texture;
        }
        return Resources.Load<Texture>("");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
