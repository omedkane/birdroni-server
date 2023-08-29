using MongoDB.Bson.Serialization.Attributes;

namespace Birdroni.Models;
public class Message
{
    [BsonId]
    private Guid _id { get; }
    public string Text { get; set; }
    public User Sentby { get; set; }
    public DateTime Datesent { get; set; }
    public DateTime Dateseen { get; set; }

    public Message(Guid? id, string text, User sentBy, DateTime dateSent, DateTime dateSeen)
    {
        _id = id ?? Guid.NewGuid();
        Text = text;
        Sentby = sentBy;
        Datesent = dateSent;
        Dateseen = dateSeen;
    }
}
