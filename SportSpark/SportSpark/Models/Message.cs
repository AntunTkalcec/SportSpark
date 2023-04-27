using CommunityToolkit.Mvvm.Messaging.Messages;

namespace SportSpark.Models
{
    public class Message : ValueChangedMessage<string>
    {
        public Message(string value) : base(value)
        {
                
        }
    }
}
