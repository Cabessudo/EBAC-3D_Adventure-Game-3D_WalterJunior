using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Singleton;
using DG.Tweening;

namespace Texts
{
    public enum TextType
    {
        CHEST,
        ROCKET,
        LAST_PLANET,
        CHECKPOINT,
        PLANET_LOCKED,
        CLOTH_LOCKED
    }

    public class TextManagerUI : Singleton<TextManagerUI>
    {
        public TextMeshProUGUI textWarn;
        public List<TextSetup> textSetup;

        void Start()
        {
            textWarn.SetText("");
        }

        public void SetTextByType(TextType currType)
        {
            var cuttText = textSetup.Find(i => i.type == currType);
            textWarn.alpha = 1;
            textWarn.SetText(cuttText.text);
            Invoke(nameof(FadeText), 1);
        }

        void FadeText()
        {
            textWarn.DOFade(0, 1);
        }

        [System.Serializable]
        public class TextSetup
        {
            public TextType type;
            public string text;
        }
    }
}
