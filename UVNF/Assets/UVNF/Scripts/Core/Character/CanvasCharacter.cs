using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UVNF.Core
{
    public class CanvasCharacter: MonoBehaviour
    {
        [HideInInspector] public CharacterData data;
        public Vector3 CurrentPosition {
            get => rectTransform.anchoredPosition;
            set => rectTransform.anchoredPosition = value;
        }

        RectTransform rectTransform;
        Image image;



        void Awake() {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
        }


        public void Setup(CharacterData data, Vector2 size, bool isFacingRight) {
            this.data = data;
            rectTransform.sizeDelta = size;
            rectTransform.localScale = isFacingRight ? Vector3.one : new Vector3(-1, 1, 1);
        }

        public void SetSprite(Sprite sprite) {
            image.sprite = sprite;
        }

    }
}