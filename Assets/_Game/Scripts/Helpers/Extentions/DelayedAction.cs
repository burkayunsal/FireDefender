using System.Collections;
using UnityEngine;
using System;

namespace SBF.Toolkit
{
    public class DelayedAction
    {
        private Action action;
        private float delay;

        public DelayedAction(Action action,float delay)
        {
            this.action = action;
            this.delay = delay;
        }

        public void Execute(MonoBehaviour parent)
        {
            parent.StartCoroutine(GetCoroutine());
        }

        private IEnumerator GetCoroutine()
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}

