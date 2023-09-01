using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterData")]
public class CharacterData : ScriptableObject {

    public string characterName;
    public List<CharacterSprite> sprites;



    public Sprite GetSprite(CharacterPose pose) {
        for (int i = 0; i< sprites.Count; i++) {
            if (sprites[i].pose == pose)
                return sprites[i].sprite;
        }
        return null;
    }

    public Sprite GetSprite(string poseName) {
        for (int i = 0; i< sprites.Count; i++) {
            if (sprites[i].poseName == poseName)
                return sprites[i].sprite;
        }
        return null;
    }

    [System.Serializable]
    public struct CharacterSprite {
        public string poseName;
        public CharacterPose pose;
        public Sprite sprite;
    }
}

public enum CharacterPose {
    Custom = -1,
    Idle = 0,
    Agree = 1,
    Disagree = 2,

}
