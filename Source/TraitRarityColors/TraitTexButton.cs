using UnityEngine;
using Verse;

namespace TraitRarityColors
{
    [StaticConstructorOnStartup]
    class TraitTexButton
    {
        public static readonly Texture2D ShowTraits = ContentFinder<Texture2D>.Get("UI/Buttons/ShowTraitRarity", true); // TODO fix blurry graphic
    }
}
