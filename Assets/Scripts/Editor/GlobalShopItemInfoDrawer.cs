using Assets.Scripts.GlobalShop;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(GlobalShopItemInfo))]
    public class GlobalShopItemInfoDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int lines = 7; // базовые поля
            var isUpgrade = property.FindPropertyRelative("IsUpgradeType").boolValue;

            if (isUpgrade) lines += 5; // UpgradeIcon + UpgradePrice

            return (EditorGUIUtility.singleLineHeight + 2) * (lines + 1);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float y = position.y;
            float h = EditorGUIUtility.singleLineHeight;
            float space = 2f;

            // Foldout
            var foldRect = new Rect(position.x, y, position.width, h);
            property.isExpanded = EditorGUI.Foldout(foldRect, property.isExpanded, label, true);
            y += h + space;

            if (!property.isExpanded)
            {
                EditorGUI.EndProperty();
                return;
            }

            EditorGUI.indentLevel++;

            Draw("IconShopItem");
            Draw("IconDescriptionItem");
            Draw("Price");
            Draw("DescriptionItem");
            Draw("NameItem");
            Draw("Type");
            Draw("IsUpgradeType");

            bool isUpgrade = property.FindPropertyRelative("IsUpgradeType").boolValue;
            if (isUpgrade)
            {
                Draw("UpgradeIcon");
                Draw("UpgradeDescriptionIcon");
                Draw("UpgradePrice");
                Draw("UpgradedName");
                Draw("UpgradedDescription");
            }

            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();

            void Draw(string name)
            {
                var p = property.FindPropertyRelative(name);
                var r = new Rect(position.x, y, position.width, h);
                EditorGUI.PropertyField(r, p, true);
                y += h + space;
            }
        }
    }
}