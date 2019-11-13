using System;
using System.ComponentModel;
using System.Linq;

namespace WS_Internet.v0
{
    [Serializable]
    public class Result<Entity>
    {
        public Entity Return { get; set; }
        public string Error { get; set; }
        public bool Ok { get; set; }

    }
}