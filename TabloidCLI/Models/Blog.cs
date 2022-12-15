﻿using System.Collections.Generic;

namespace TabloidCLI.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return $"{Title} ({Url})";
        }
    }
}