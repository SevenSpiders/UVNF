using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UVNF.Core
{
    public class CanvasCharacter: MonoBehaviour
    {
        [HideInInspector] public CharacterData data;
        [HideInInspector] public ScenePositions CurrentPosition;
        public Vector3 position {
            get => rectTransform.anchoredPosition;
            set => rectTransform.anchoredPosition = value;
        }
        public Vector2 sizeDelta {
            get => rectTransform.sizeDelta;
            set => rectTransform.sizeDelta = value;
        }

        public Rect rect => rectTransform.rect;

        RectTransform rectTransform;
        Image image;

        public bool CurrentlyMoving => (movingCoroutine != null);
        private Coroutine movingCoroutine;



        void Awake() {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
        }


        public void Setup(CharacterData data, Vector2 size, bool isFacingRight) {
            this.data = data;
            this.name = data.characterName;
            rectTransform.sizeDelta = size;
            rectTransform.localScale = isFacingRight ? Vector3.one : new Vector3(-1, 1, 1);
        }






        public void ChangeSprite(Sprite newSprite) => SetSprite(newSprite);
        public void SetSprite(Sprite sprite) {
            image.sprite = sprite;
        }


        public void MoveCharacter(Vector2 endPosition, float moveTime)
        {
            if (CurrentlyMoving)
                StopCoroutine(movingCoroutine);

            movingCoroutine = StartCoroutine(MoveCharacterCoroutine(rectTransform.anchoredPosition, endPosition, moveTime));
        }

        public IEnumerator MoveCharacterCoroutine(Vector2 startPosition, Vector2 endPosition, float moveTime)
        {
            float distance = Vector3.Distance(startPosition, endPosition);
            float currentLerpTime = 0f;

            while (rectTransform.anchoredPosition != endPosition)
            {
                currentLerpTime += Time.deltaTime;
                if (currentLerpTime > moveTime)
                    currentLerpTime = moveTime;

                float t = currentLerpTime / moveTime;
                t = t * t * t * (t * (6f * t - 15f) + 10f);
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
                yield return null;
            }
        }

    }
}