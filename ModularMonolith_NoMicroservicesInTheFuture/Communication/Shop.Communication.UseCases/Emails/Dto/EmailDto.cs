namespace Shop.Communication.UseCases.Emails.Dto
{
    internal class EmailDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsSended { get; set; }
        public int Attempts { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
    }
}
