using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class PlayerPrefsCleaner
    {
        [MenuItem("Edit/Clear PlayerPrefs + Save File")]
        private static void ClearAll()
        {
            if (!EditorUtility.DisplayDialog(
                    "Clear Save Data",
                    "Удалить ВСЕ PlayerPrefs и файл сохранения (save.json)?",
                    "Да, удалить",
                    "Отмена"))
                return;

            // 1) PlayerPrefs
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            // 2) JSON save file
            string savePath = Path.Combine(Application.persistentDataPath, "save.json");

            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                Debug.Log($"Save file deleted: {savePath}");
            }
            else
            {
                Debug.Log($"Save file not found: {savePath}");
            }

            Debug.Log("Очистка завершена.");
        }
    }
}