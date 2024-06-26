﻿namespace PCM.Server.APIQueryService.Models
{
    public class Root<T>
    {
        public List<T> Data { get; set; } = new List<T>();
    }

    public class RootSingleton<T>
    {
        public T? Data { get; set; }
    }
}
