namespace VRTK.UnityEventHelper
{
    using UnityEngine;
    using UnityEngine.Events;
    using System;

    [AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_ControllerEvents_UnityEvents")]
    public sealed class ChangeSize : VRTK_UnityEvents<VRTK_ControllerEvents>
    {
        [Serializable]
        public sealed class ControllerInteractionEvent : UnityEvent<object, ControllerInteractionEventArgs> { }
        private ControllerInteractionEvent OnTouchpadPressed = new ControllerInteractionEvent();
        private ControllerInteractionEvent OnTouchpadReleased = new ControllerInteractionEvent();

        public GameObject[] puzzles;
        private Transform[] trans;

        protected override void AddListeners(VRTK_ControllerEvents component)
        {
            component.TouchpadPressed += TouchpadPressed;
            component.TouchpadReleased += TouchpadReleased;
        }

        protected override void RemoveListeners(VRTK_ControllerEvents component)
        {
            component.TouchpadPressed -= TouchpadPressed;
            component.TouchpadReleased -= TouchpadReleased;
        }
        
        private void TouchpadPressed(object o, ControllerInteractionEventArgs e)
        {
            puzzles = GameObject.FindGameObjectsWithTag("Puzzle");
            float size = 0.1f;

            for(int i = 0; i < puzzles.Length; i++){
                size = UnityEngine.Random.Range(0.05f, 0.2f);
                puzzles[i].GetComponent<Transform>().localScale = new Vector3(size, size, size);
                puzzles[i].GetComponent<Transform>().Translate(Vector3.up*0.2f, Space.World);
                puzzles[i].GetComponent<Transform>().rotation = UnityEngine.Random.rotation;
            }    

            puzzles = GameObject.FindGameObjectsWithTag("LargePuzzle");
            size = 0.1f;

            for(int i = 0; i < puzzles.Length; i++){
                size = UnityEngine.Random.Range(0.05f, 0.2f);
                puzzles[i].GetComponent<Transform>().localScale = new Vector3(size, size, size);
                puzzles[i].GetComponent<Transform>().Translate(Vector3.up*0.2f, Space.World);
                puzzles[i].GetComponent<Transform>().rotation = UnityEngine.Random.rotation;
            }  
        }

        private void TouchpadReleased(object o, ControllerInteractionEventArgs e)
        {

        }
    }
}