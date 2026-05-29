using System;
using Modding;
using UnityEngine;

namespace FlingerModMenu
{
    public class MenuManager : MonoBehaviour
    {
        private bool menuOpen = false;

        private Rect floatingBtnRect = new Rect(20f, 100f, 110f, 50f);
        private Rect menuRect = new Rect(20f, 40f, 380f, 520f);

        private bool godModeEnabled = false;
        private bool infiniteJumpEnabled = false;
        private bool oneShotEnabled = false;

        private string geoAmountText = "1000";

        private GUIStyle floatingBtnStyle;
        private GUIStyle menuWindowStyle;
        private GUIStyle titleStyle;
        private GUIStyle toggleStyle;
        private GUIStyle buttonStyle;
        private GUIStyle closeBtnStyle;
        private GUIStyle textFieldStyle;

        private bool stylesInitialized = false;

        void Start()
        {
            ModHooks.TakeHealthHook += OnTakeHealth;
            ModHooks.TakeDamageHook += OnTakeDamage;
        }

        void OnDestroy()
        {
            ModHooks.TakeHealthHook -= OnTakeHealth;
            ModHooks.TakeDamageHook -= OnTakeDamage;
        }

        private int OnTakeHealth(int damage)
        {
            if (godModeEnabled) return 0;
            return damage;
        }

        private int OnTakeDamage(ref int hazardType, int damage)
        {
            if (oneShotEnabled) return 9999;
            return damage;
        }

        void Update()
        {
            if (infiniteJumpEnabled)
            {
                bool jumpPressed = Input.GetButtonDown("Jump");
                bool touchJump = Input.touchCount > 0;

                if ((jumpPressed || touchJump) && HeroController.instance != null)
                {
                    Rigidbody2D rb = HeroController.instance.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 vel = rb.velocity;
                        vel.y = 0f;
                        rb.velocity = vel;
                    }
                    PlayerData.instance.hasDoubleJump = true;
                    PlayerData.instance.infiniteAirJump = true;
                }
            }
        }

        private void InitStyles()
        {
            if (stylesInitialized) return;
            stylesInitialized = true;

            floatingBtnStyle = new GUIStyle(GUI.skin.button);
            floatingBtnStyle.fontSize = 20;
            floatingBtnStyle.alignment = TextAnchor.MiddleCenter;
            floatingBtnStyle.fontStyle = FontStyle.Bold;
            floatingBtnStyle.normal.background = MakeTex(2, 2, new Color(0.35f, 0.35f, 0.35f, 0.92f));
            floatingBtnStyle.hover.background = MakeTex(2, 2, new Color(0.45f, 0.45f, 0.45f, 1f));
            floatingBtnStyle.border = new RectOffset(12, 12, 12, 12);

            menuWindowStyle = new GUIStyle(GUI.skin.box);
            menuWindowStyle.normal.background = MakeTex(2, 2, new Color(0.08f, 0.08f, 0.08f, 0.92f));

            titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.fontSize = 26;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleCenter;
            titleStyle.normal.textColor = new Color(0.9f, 0.7f, 0.1f);

            toggleStyle = new GUIStyle(GUI.skin.toggle);
            toggleStyle.fontSize = 22;
            toggleStyle.normal.textColor = Color.white;
            toggleStyle.active.textColor = Color.white;

            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 22;
            buttonStyle.fontStyle = FontStyle.Bold;
            buttonStyle.normal.background = MakeTex(2, 2, new Color(0.2f, 0.2f, 0.5f, 0.95f));
            buttonStyle.hover.background = MakeTex(2, 2, new Color(0.3f, 0.3f, 0.6f, 1f));
            buttonStyle.normal.textColor = Color.white;

            closeBtnStyle = new GUIStyle(GUI.skin.button);
            closeBtnStyle.fontSize = 22;
            closeBtnStyle.fontStyle = FontStyle.Bold;
            closeBtnStyle.normal.background = MakeTex(2, 2, new Color(0.7f, 0.1f, 0.1f, 0.95f));
            closeBtnStyle.hover.background = MakeTex(2, 2, new Color(0.9f, 0.2f, 0.2f, 1f));
            closeBtnStyle.normal.textColor = Color.white;

            textFieldStyle = new GUIStyle(GUI.skin.textField);
            textFieldStyle.fontSize = 22;
            textFieldStyle.alignment = TextAnchor.MiddleCenter;
        }

        void OnGUI()
        {
            InitStyles();

            if (!menuOpen)
            {
                floatingBtnRect = GUI.Window(1001, floatingBtnRect, DrawFloatingButton, "", new GUIStyle());
            }
            else
            {
                menuRect = GUI.Window(1002, menuRect, DrawMainMenu, "", new GUIStyle());
            }
        }

        private void DrawFloatingButton(int id)
        {
            if (GUI.Button(new Rect(0, 0, 110, 50), "smiler", floatingBtnStyle))
            {
                menuOpen = true;
            }
            GUI.DragWindow(new Rect(0, 0, 110, 50));
        }

        private void DrawMainMenu(int id)
        {
            GUI.Box(new Rect(0, 0, menuRect.width, menuRect.height), "", menuWindowStyle);

            float pad = 14f;
            float y = pad;
            float w = menuRect.width - pad * 2;

            GUI.Label(new Rect(pad, y, w - 50, 40), "Flinger's Mod Menu", titleStyle);

            if (GUI.Button(new Rect(menuRect.width - 50, y, 40, 40), "X", closeBtnStyle))
            {
                menuOpen = false;
            }
            y += 50f;

            godModeEnabled = GUI.Toggle(new Rect(pad, y, w, 40), godModeEnabled, "  God Mode", toggleStyle);
            y += 48f;

            infiniteJumpEnabled = GUI.Toggle(new Rect(pad, y, w, 40), infiniteJumpEnabled, "  Infinite Jump", toggleStyle);
            y += 48f;

            oneShotEnabled = GUI.Toggle(new Rect(pad, y, w, 40), oneShotEnabled, "  One Hit Kill", toggleStyle);
            y += 48f;

            if (GUI.Button(new Rect(pad, y, w, 44), "Desbloquear Movimientos", buttonStyle))
            {
                UnlockMovements();
            }
            y += 52f;

            if (GUI.Button(new Rect(pad, y, w, 44), "Amuletos Infinitos (99 slots)", buttonStyle))
            {
                if (PlayerData.instance != null)
                    PlayerData.instance.charmSlots = 99;
            }
            y += 52f;

            GUI.Label(new Rect(pad, y, w, 30), "Cantidad de Geo:", toggleStyle);
            y += 32f;
            geoAmountText = GUI.TextField(new Rect(pad, y, w - 160, 44), geoAmountText, textFieldStyle);
            if (GUI.Button(new Rect(pad + w - 150, y, 150, 44), "Give Geo", buttonStyle))
            {
                GiveGeo();
            }
            y += 52f;

            GUI.DragWindow(new Rect(0, 0, menuRect.width, menuRect.height));
        }

        private void UnlockMovements()
        {
            PlayerData pd = PlayerData.instance;
            if (pd == null) return;
            pd.hasDash = true;
            pd.hasWalljump = true;
            pd.hasDoubleJump = true;
            pd.hasSuperDash = true;
            pd.hasAcidArmour = true;
            pd.hasShadowDash = true;
            pd.hasDreamNail = true;
        }

        private void GiveGeo()
        {
            if (HeroController.instance == null) return;
            int amount;
            if (!int.TryParse(geoAmountText, out amount))
                amount = 1000;
            HeroController.instance.AddGeo(amount);
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}
