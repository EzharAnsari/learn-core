using System;
using System.Collections.Generic;

namespace crud_api.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public bool? Status { get; set; }
    }
}
