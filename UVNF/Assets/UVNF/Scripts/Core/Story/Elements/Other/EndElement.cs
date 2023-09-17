using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UVNF.Core.UI;
using UVNF.Extensions;
using UVNF.Entities.Containers;


namespace UVNF.Core.Story.Other
{
    public class EndElement : StoryElement
    {
        public override string ElementName => "EndElement";

        public override Color32 DisplayColor => _displayColor;
        private Color32 _displayColor = new Color32().Other();
        public override StoryElementTypes Type => StoryElementTypes.Other;

        public StoryGraph nextChapter;

#if UNITY_EDITOR
        public override void DisplayLayout(Rect layoutRect, GUIStyle label)
        {
            nextChapter = EditorGUILayout.ObjectField(nextChapter, typeof(StoryGraph), false) as StoryGraph;
        }
#endif

        public override IEnumerator Execute(UVNFManager managerCallback, UVNFCanvas canvas)
        {
            // managerCallback.CharacterManager.ChangeCharacterSprite(CharacterName, NewSprite);
            managerCallback.StartSubStory(nextChapter);
            return null;
        }
    }

}