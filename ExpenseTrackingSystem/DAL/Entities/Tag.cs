namespace DAL.Entities
{
    public class Tag : IPrivateKey
    {
        public Tag()
        {
            
        }

        public Tag(int tagId, string name, User user)
        {
            TagID = tagId;
            Name = name;
            User = user;
        }

        public virtual int TagID { protected set; get; }

        public virtual string Name { set; get; }

        public virtual User User { set; get; }

        public virtual int PrivateKey
        {
            get { return TagID; }
        }
    }
}
