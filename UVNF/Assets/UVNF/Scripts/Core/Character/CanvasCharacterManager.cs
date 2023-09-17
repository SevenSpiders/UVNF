using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UVNF.Core.Story.Character;
using UVNF.Entities;

namespace UVNF.Core
{
    public class CanvasCharacterManager : MonoBehaviour
    {
        public List<CanvasCharacter> CharactersOnScreen;
        public RectTransform MainCharacterStack;
        public CanvasCharacter characterPrefab;

        private CanvasCharacter[] LeftSideCharacters { get { return CharactersOnScreen.Where(x => x.CurrentPosition == ScenePositions.Left).ToArray(); } }
        private CanvasCharacter[] MiddleSideCharacters { get { return CharactersOnScreen.Where(x => x.CurrentPosition == ScenePositions.Middle).ToArray(); } }
        private CanvasCharacter[] RightSideCharacters { get { return CharactersOnScreen.Where(x => x.CurrentPosition == ScenePositions.Right).ToArray(); } }

        public void AddCharacter(CharacterData characterData, Sprite characterSprite, bool flip, ScenePositions enter, ScenePositions position, float enterTime)
        {
            MainCharacterStack.gameObject.SetActive(true);


            CanvasCharacter _character = Instantiate(characterPrefab, MainCharacterStack);
            _character.Setup(characterData, MainCharacterStack.sizeDelta, flip);
            _character.SetSprite(characterSprite);
            _character.CurrentPosition = position; // ScenePosition

            Vector2 startPosition = new Vector2();
            switch (enter)
            {
                case ScenePositions.Left:
                    startPosition = new Vector2(-MainCharacterStack.sizeDelta.x - _character.sizeDelta.x / 2, 0);
                    break;
                case ScenePositions.Top:
                    startPosition = new Vector2(0, MainCharacterStack.sizeDelta.y + _character.sizeDelta.y / 2);
                    break;
                case ScenePositions.Right:
                    startPosition = new Vector2(MainCharacterStack.sizeDelta.x + _character.sizeDelta.x / 2, 0);
                    break;
            }

            _character.position = startPosition;
            CharactersOnScreen.Add(_character);

            Vector2 endPosition = new Vector2();
            switch (position) {

                case ScenePositions.Left:
                    endPosition = new Vector2(-(MainCharacterStack.sizeDelta.x / 2), 0);

                    CanvasCharacter[] leftCharacters = LeftSideCharacters.Reverse().ToArray();
                    if (leftCharacters.Length > 1)
                    {
                        float leftPosition = Mathf.Abs(MainCharacterStack.sizeDelta.x);
                        float offset = leftPosition / (leftCharacters.Length + 1);
                        for (int i = 0; i < leftCharacters.Length; i++)
                        {
                            Vector2 newPosition = new Vector2(-MainCharacterStack.sizeDelta.x + offset * (i + 1), 0);
                            leftCharacters[i].MoveCharacter(newPosition, 1f);
                        }
                    }
                    else
                    {
                        _character.MoveCharacter(endPosition, enterTime);
                    }
                    break;
                case ScenePositions.Top:
                    endPosition = new Vector2(0, 0);
                    _character.MoveCharacter(endPosition, enterTime);
                    break;
                case ScenePositions.Middle:
                    endPosition = new Vector2(0f, 0f);
                    _character.MoveCharacter(endPosition, enterTime);
                    break;
                case ScenePositions.Right:
                    endPosition = new Vector2(MainCharacterStack.sizeDelta.x / 2, 0);

                    CanvasCharacter[] rightCharacters = RightSideCharacters;
                    if (rightCharacters.Length > 1)
                    {
                        float rightPosition = Mathf.Abs(MainCharacterStack.sizeDelta.x);
                        float offset = rightPosition / (rightCharacters.Length + 1);
                        for (int i = 0; i < rightCharacters.Length; i++)
                        {
                            Vector2 newPosition = new Vector2(offset * (i + 1), 0);
                            rightCharacters[i].MoveCharacter(newPosition, 1f);
                        }
                    }
                    else
                    {
                        _character.MoveCharacter(endPosition, enterTime);
                    }
                    break;
            }

        }

        public void RemoveCharacter(string characterName, ScenePositions exitPosition, float exitTime)
        {
            CanvasCharacter character = CharactersOnScreen.Find(x => x.name == characterName);

            Vector3 endPosition = new Vector3();

            switch (exitPosition)
            {
                case ScenePositions.Left:
                    endPosition = new Vector3(-(MainCharacterStack.rect.width + (character.rect.width / 2f)), 0, 0);
                    break;
                case ScenePositions.Top:
                    endPosition = new Vector3(0, character.rect.height, 0);
                    break;
                case ScenePositions.Right:
                    endPosition = new Vector3(MainCharacterStack.rect.width + (character.rect.width / 2f), 0, 0);
                    break;
            }

            CharactersOnScreen.Remove(character);
            character.MoveCharacter(endPosition, exitTime);
        }

        public void MoveCharacterTo(string characterName, string characterToMoveTo, float moveTime)
        {
            CanvasCharacter mainCharacter = CharactersOnScreen.Find(x => x.name == characterName);
            CanvasCharacter moveToCharacter = CharactersOnScreen.Find(x => x.name == characterToMoveTo);

            mainCharacter.MoveCharacter(moveToCharacter.position, moveTime);
        }

        public void ChangeCharacterSprite(string characterName, Sprite characterSprite)
        {
            CanvasCharacter character = CharactersOnScreen.Find(x => x.name == characterName);
            character.ChangeSprite(characterSprite);
        }

        public void Hide()
        {
            MainCharacterStack.gameObject.SetActive(false);
        }
    }
}
