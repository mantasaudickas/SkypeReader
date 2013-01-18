using System;
using System.Collections.Generic;

namespace SkypeReader
{
    public class Participants
    {
        private readonly int _id;
        private readonly List<string> _names = new List<string>();

        public Participants(int id)
        {
            _id = id;
        }

        public int Id { get { return _id; } }
        public List<string> Names { get { return _names; } }

        public override string ToString()
        {
            return string.Join(", ", Names);
        }
    }
}
