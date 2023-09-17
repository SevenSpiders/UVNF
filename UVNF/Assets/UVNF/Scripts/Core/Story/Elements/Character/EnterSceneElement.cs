using System.Collections;
using UnityEditor;
using UnityEngine;
using UVNF.Core.UI;
using UVNF.Extensions;

namespace UVNF.Core.Story.Character
{
    public class EnterSceneElement : StoryElement
    {
        public override string ElementName => "Enter Scene";

        public override Color32 DisplayColor => _displayColor;
        private Color32 _displayColor = new Color32().Character();

        public override StoryElementTypes Type => StoryElementTypes.Character;

        public CharacterData characterData;
        public CharacterPose pose;

        // Override sprite?
        bool overrideSprite = false;
        [HideInInspector]
        public Sprite customSprite; // use to override characterData sprite with custom one

        private bool foldOut = false;

        public bool Flip = false;

        public ScenePositions EnterFromDirection = ScenePositions.Left;
        public ScenePositions FinalPosition = ScenePositions.Left;

        public float EnterTime = 2f;
        public bool Wait = false;   // Waits for animation to finish before proceeding

#if UNITY_EDITOR
        public override void DisplayLayout(Rect layoutRect, GUIStyle label)
        {
            characterData = EditorGUILayout.ObjectField(characterData, typeof(CharacterData), false) as CharacterData;
            pose = (CharacterPose)EditorGUILayout.EnumPopup("Pose", pose);


            // if custom pose => override with custom sprite
            overrideSprite = (pose == CharacterPose.Custom);
            if (overrideSprite) {
                customSprite = EditorGUILayout.ObjectField(customSprite, typeof(Sprite), false) as Sprite;
            }


            Flip = GUILayout.Toggle(Flip, "Flip");
            Sprite _sprite = (overrideSprite) ? customSprite : characterData.GetSprite(pose);

            if (_sprite != null)
            {
                foldOut = EditorGUILayout.Foldout(foldOut, "Preview", true);
                if (foldOut) {
                    int width = 300;    // 1000
                    int height = 200;   // 500
                    layoutRect.position = new Vector2(0, layoutRect.y + 100);
                    layoutRect.width = width;
                    layoutRect.height = height;

                    GUI.DrawTexture(layoutRect, _sprite.texture, ScaleMode.ScaleToFit);
                    GUILayout.Space(height + 20);
                }
            }

            EnterFromDirection = (ScenePositions)EditorGUILayout.EnumPopup("Enter From", EnterFromDirection);
            FinalPosition = (ScenePositions)EditorGUILayout.EnumPopup("Final Position", FinalPosition);

            GUILayout.BeginHorizontal();
            {
                EnterTime = EditorGUILayout.FloatField("Enter Time", EnterTime);
                 GUILayout.Space(10);
                Wait = GUILayout.Toggle(Wait, "Wait");
            }
            GUILayout.EndHorizontal();

            // EnterTime = EditorGUILayout.Slider("Enter Time", EnterTime, 1f, 10f);
            
        }
#endif

        public override IEnumerator Execute(UVNFManager managerCallback, UVNFCanvas canvas)
        {

            Sprite _sprite = (overrideSprite) ? customSprite : characterData.GetSprite(pose);

            managerCallback.CharacterManager.AddCharacter(characterData, _sprite, Flip, EnterFromDirection, FinalPosition, EnterTime);
            
            float currentTime = 0f;
            while (currentTime < EnterTime && Wait)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }
            
        }
    }
}