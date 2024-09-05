namespace Web_Application.Models
{
    public class BorrowRecord
    {
        public int BorrowRecordId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Book Book { get; set; }
        public Member Member { get; set; }

    }
}
