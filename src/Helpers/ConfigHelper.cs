﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipSearcher.Helpers
{
    public class ConfigHelper
    {
        public static async Task<T> LoadConfigAsync<T>(string path = null) where T : new()
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    path = GetDefaultPath<T>();

                var config = await JsonHelper.JsonDeserializeFromFileAsync<T>(path);
                if (config == null)
                    config = new T();
                return config;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static async Task SaveConfigAsync<T>(T data, string path = null) where T : new()
        {
            if (string.IsNullOrEmpty(path))
                path = GetDefaultPath<T>();

            var json = await JsonHelper.JsonSerializeAsync(data, path);
        }

        private static string GetDefaultPath<T>() where T : new()
        {
            //var temp = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            var rootDir = Environment.CurrentDirectory;
           
            //return $"{typeof(T).Name}.json";
            return $"{rootDir}\\Config\\{typeof(T).Name}.json";
        }

    }
}
