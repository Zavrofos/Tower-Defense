using System.Collections;
using UnityEngine;

namespace Assets.DevScripts
{
    public interface IPresenter 
    {
        void Subscribe();
        void Unsubscribe();
    }
}