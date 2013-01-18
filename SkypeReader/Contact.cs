using System;

namespace SkypeReader
{
    public class Contact
    {
        private readonly string _skypeName;
        private readonly string _displayName;
        private readonly string _givenDisplayName;

        public Contact(string skypeName, string displayName, string givenDisplayName)
        {
            _skypeName = skypeName;
            _displayName = displayName;
            _givenDisplayName = givenDisplayName;
        }

        public string SkypeName { get { return _skypeName; } }
        public string Name
        {
            get { return string.IsNullOrEmpty(_givenDisplayName) ? _displayName: _givenDisplayName; }
        }
    }
}
