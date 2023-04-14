namespace ContactBookLibrary
{
    public class Contact
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public long PersonNumber { get; set; }
        public string PersonEmail { get; set; }

        public Contact(int id, string name, long number, string email)
        {
            this.PersonId = id;
            this.PersonName = name;
            this.PersonNumber = number;
            this.PersonEmail = email;
        }

        public Contact(string name, long number, string email)
        {
            this.PersonName=name;
            this.PersonNumber = number;
            this.PersonEmail=email;
        }

        public override string ToString()
        {
            return "Id:"+PersonId+", Name:" + this.PersonName + ", PhNum:" + this.PersonNumber + ", Email:" + PersonEmail ;
        }
    }
}
