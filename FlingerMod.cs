using Modding;
using UnityEngine;

namespace FlingerModMenu
{
    public class FlingerMod : Mod
    {
        public FlingerMod() : base("Flinger's Mod Menu") { }

        public override string GetVersion() => "1.0.0";

        public override void Initialize()
        {
            GameObject menuObj = new GameObject("FlingerMenuObj");
            menuObj.AddComponent<MenuManager>();
            GameObject.DontDestroyOnLoad(menuObj);
        }
    }
}
