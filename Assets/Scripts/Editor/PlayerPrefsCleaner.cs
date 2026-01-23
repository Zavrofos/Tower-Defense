using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class PlayerPrefsCleaner
    {
        [MenuItem("Edit/Clear PlayerPrefs")]
        private static void ClearAllPlayerPrefs()
        {
            if (EditorUtility.DisplayDialog(
                    "Clear PlayerPrefs",
                    "Удалить ВСЕ PlayerPrefs для этого проекта на этом устройстве?",
                    "Да, удалить",
                    "Отмена"))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                Debug.Log("PlayerPrefs: удалены все значения.");
            }
        }
    }
}