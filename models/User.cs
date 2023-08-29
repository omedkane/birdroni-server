class User
{
    private Guid _id { get; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Avatar { get; set; }

    public User(Guid? id, string firstname, string lastname, string avatar)
    {
        _id = id ?? Guid.NewGuid();
        Firstname = firstname;
        Lastname = lastname;
        Avatar = avatar;
    }
}
