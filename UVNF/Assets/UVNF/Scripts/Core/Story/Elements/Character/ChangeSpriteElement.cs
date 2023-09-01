using System.Collections;
using UnityEditor;
using UnityEngine;
using UVNF.Core.UI;
using UVNF.Extensions;

namespace UVNF.Core.Story.Character
{
    public class ChangeSpriteElement : StoryElement
    {
        public override string ElementName => "Change Sprite";

        public override Color32 DisplayColor => _displayColor;
        private Color32 _displayColor = new Color32().Character();

        public override StoryElementTypes Type => StoryElementTypes.Character;

        public string CharacterName;
        public Sprite NewSprite;
        public CharacterData characterData;
        public CharacterPose pose;


#if UNITY_EDITOR
        public override void DisplayLayout(Rect layoutRect, GUIStyle label)
        {
            CharacterName = EditorGUILayout.TextField("Character Name", CharacterName);

            characterData = EditorGUILayout.ObjectField(characterData, typeof(CharacterData), false) as CharacterData;
            pose = (CharacterPose)EditorGUILayout.EnumPopup("Pose", pose);
            GUILayout.Label("New Character Sprite", EditorStyles.boldLabel);
            NewSprite = EditorGUILayout.ObjectField(NewSprite, typeof(Sprite), false) as Sprite;
            
        }
#endif

        public override IEnumerator Execute(UVNFManager managerCallback, UVNFCanvas canvas)
        {
            
            Sprite _sprite = NewSprite;
            if (characterData != null) {
                CharacterName = characterData.characterName;
                _sprite = characterData.GetSprite(pose);
                if (_sprite == null)  {
                    Debug.LogError($"Character sprite not found {pose}");
                    _sprite = NewSprite;
                }
            }

            managerCallback.CharacterManager.ChangeCharacterSprite(CharacterName, _sprite);
            return null;
            
        }
    }
}