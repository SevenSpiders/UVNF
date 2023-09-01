using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UVNF.Core.UI;
using UVNF.Extensions;

namespace UVNF.Core.Story.Character
{
    public class IceBlockGameElement : StoryElement
    {
        public override string ElementName => "Minigame";

        public override Color32 DisplayColor => _displayColor;
        private Color32 _displayColor = new Color32().Character();
        public override StoryElementTypes Type => StoryElementTypes.MiniGame;

#if UNITY_EDITOR
        public override void DisplayLayout(Rect layoutRect, GUIStyle label)
        {
            
        }
#endif

        public override IEnumerator Execute(UVNFManager managerCallback, UVNFCanvas canvas)
        {
            // managerCallback.CharacterManager.ChangeCharacterSprite(CharacterName, NewSprite);
            return null;
        }
    }

}