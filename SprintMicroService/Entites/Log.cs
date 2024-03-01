﻿using System;
using Microsoft.Extensions.Logging;

namespace SprintMicroService.Entites
{
    public class Log
    {
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
        public string Service { get; set; }

        public string Method { get; set; }
    }
}
