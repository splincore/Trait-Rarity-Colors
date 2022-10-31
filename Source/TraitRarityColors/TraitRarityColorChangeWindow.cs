using System;
using UnityEngine;
using Verse;
using System.Globalization;

namespace TraitRarityColors
{
    public class TraitRarityColorChangeWindow : Window
    {
        public override Vector2 InitialSize
        {
            get
            {
                return this.WindowSize;
            }
        }
        protected Vector2 WindowSize = new Vector2(600f, 500f); // Border Size is 18

        private string rarity = "";
        private string rarityColor = "";
        private string defaultRarityColor = "";
        private float colorR;
        private float colorG;
        private float colorB;

        public TraitRarityColorChangeWindow(string tmpRarity, string tmpRarityColor, string tmpDefaultRarityColor)
        {
            rarity = tmpRarity;
            rarityColor = tmpRarityColor;
            defaultRarityColor = tmpDefaultRarityColor;
            SetRGBColors();
        }

        public override void DoWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label(string.Format("{0} {1}{2}</color>", "TraitRarityChangeColorLabel".Translate(), rarityColor, ("TraitRarity" + rarity.CapitalizeFirst()).Translate()));

            listingStandard.Gap(10f);
            listingStandard.Label("<color=#FF0000>Red</color>");
            Rect rectR = listingStandard.GetRect(22f);
            colorR = Widgets.HorizontalSlider(rectR, colorR, 0f, 255f, false, colorR.ToString(), "0", "255", 1f);

            listingStandard.Gap(10f);
            listingStandard.Label("<color=#00FF00>Green</color>");
            Rect rectG = listingStandard.GetRect(22f);
            colorG = Widgets.HorizontalSlider(rectG, colorG, 0f, 255f, false, colorG.ToString(), "0", "255", 1f);

            listingStandard.Gap(10f);
            listingStandard.Label("<color=#0000FF>Blue</color>");
            Rect rectB = listingStandard.GetRect(22f);
            colorB = Widgets.HorizontalSlider(rectB, colorB, 0f, 255f, false, colorB.ToString(), "0", "255", 1f);

            listingStandard.Gap(10f);
            if (listingStandard.ButtonText("TraitRarityResetColorLabel".Translate()))
            {
                rarityColor = defaultRarityColor;
                SetRGBColors();
            }
            listingStandard.End();

            rarityColor = "<color=#" + String.Format("{0:X2}{1:X2}{2:X2}", (int)colorR, (int)colorG, (int)colorB) + ">";

            Vector2 buttonSize = new Vector2(120f, 40f);            
            float positionButtonY = InitialSize.y - buttonSize.y - 36f;
            float positionOk = InitialSize.x / 2 - buttonSize.x - 5f - 18f;
            float positionCancel = positionOk + buttonSize.x + 10f;
            Rect rectOk = new Rect(positionOk, positionButtonY, buttonSize.x, buttonSize.y);
            Rect rectCancel = new Rect(positionCancel, positionButtonY, buttonSize.x, buttonSize.y);

            if (Widgets.ButtonText(rectOk, "OK".Translate()))
            {
                switch(rarity)
                {
                    case "mystic":
                        LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorMystic = rarityColor;
                        break;
                    case "legendary":
                        LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorLegendary = rarityColor;
                        break;
                    case "epic":
                        LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorEpic = rarityColor;
                        break;
                    case "rare":
                        LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorRare = rarityColor;
                        break;
                    case "uncommon":
                        LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorUncommon = rarityColor;
                        break;
                    case "common":
                        LoadedModManager.GetMod<TraitRarityColorsMod>().GetSettings<TraitRarityColorsModSettings>().colorCommon = rarityColor;
                        break;
                    default:
                        break;
                }
                TraitRarityColors.RefreshTraitDefs();
                this.Close(true);
                Event.current.Use();
            }
            if (Widgets.ButtonText(rectCancel, "CancelButton".Translate()))
            {
                this.Close(true);
                Event.current.Use();
            }
        }

        public void SetRGBColors()
        {
            string hexColor = rarityColor.Replace("<color=#", "").Replace(">", "");
            colorR = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            colorG = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            colorB = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
        }
    }
}
