using System;

namespace SkypeReader
{
    public class ChatInfo
    {
        public ChatInfo(string chatName)
        {
            ChatName = chatName;
        }

        public string ChatName { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
