using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    [Serializable]
    public class Saver<T>
    {
        public T dataSaver;

        Saver(T data) 
        {
            dataSaver = data;
        }

        public static void TryLoad(string filename, ref T data)
        {
            var path = FileHandler.Path(filename);

            if (File.Exists(path))
            {
                var dataString = File.ReadAllText(path);

                var saver = JsonUtility.FromJson<Saver<T>>(dataString);

                data = saver.dataSaver;
            }
        }

        public static void Save(string filename, T data)
        {
            var wrapper = new Saver<T>(data);

            var dataString = JsonUtility.ToJson(wrapper, true);

            File.WriteAllText(FileHandler.Path(filename), dataString);
        }
    }

    public static class FileHandler
    {
        public  static string Path(string fileName)
        {
            //Debug.Log($"{Application.persistentDataPath}/{fileName}");

            return $"{Application.persistentDataPath}/{fileName}";
        }

        public static void Reset(string filename)
        {
            var path = FileHandler.Path(filename);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool HasFile(string filename)
        {
            return File.Exists(Path(filename));
        }
    }

}