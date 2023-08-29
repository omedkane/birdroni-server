using MongoDB.Bson.Serialization.Attributes;

namespace Birdroni.Models;
public class Chat
{
    [BsonId]
    private Guid _id { get; }
    public User[] Chatmates = new User[2];
    public Message[] Messages { get; set; }

    public Chat(Guid? id, User[] chatMates, Message[] messages)
    {
        _id = id ?? Guid.NewGuid();
		Chatmates = chatMates;
		Messages = messages;
    }
}
