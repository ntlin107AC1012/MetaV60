using Oculus.Interaction.Input;
using System.Collections.Generic;
using UnityEngine;
namespace Oculus.Interaction.Samples.PalmMenu
{
    public class Righthandmenu : MonoBehaviour,IGameObjectFilter
    {
        [SerializeField, Interface(typeof(IHand))]
        private Object _leftHand;

        [SerializeField]
        private GameObject[] _leftHandedGameObjects;

        [SerializeField]
        private GameObject[] _rightHandedGameObjects;

        private IHand LeftHand { get; set; }

        private readonly HashSet<GameObject> _leftHandedGameObjectSet =
            new HashSet<GameObject>();
        private readonly HashSet<GameObject> _rightHandedGameObjectSet =
            new HashSet<GameObject>();

        protected virtual void Start()
        {
            foreach (var go in _leftHandedGameObjects)
            {
                _leftHandedGameObjectSet.Add(go);
            }

            foreach (var go in _rightHandedGameObjects)
            {
                _rightHandedGameObjectSet.Add(go);
            }

            LeftHand = _leftHand as IHand;
        }

        public bool Filter(GameObject go)
        {
            if (LeftHand.IsDominantHand)
            {
                return _leftHandedGameObjectSet.Contains(go);
            }
            else
            {
                return _rightHandedGameObjectSet.Contains(go);
            }
        }
    }
}
