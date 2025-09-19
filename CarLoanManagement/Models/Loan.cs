namespace CarLoanManagement.Models
{
    /// <summary>
    /// Abstract base class for all loan types.
    /// Demonstrates the Open/Closed Principle - open for extension (RetailLoan, LeaseLoan)
    /// but closed for modification.
    /// Uses Template Method pattern with abstract LoanType property and virtual MarkAsPaidOff method.
    /// </summary>
    public abstract class Loan
    {
        /// <summary>
        /// Unique identifier for the loan
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key reference to the associated car
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Original loan amount when the loan was first created
        /// </summary>
        public decimal OriginalAmount { get; set; }

        /// <summary>
        /// Current amount required to pay off the loan completely
        /// </summary>
        public decimal PayoffAmount { get; set; }

        /// <summary>
        /// Date when the loan was originated
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Flag indicating whether the loan has been fully paid off
        /// </summary>
        public bool IsPaidOff { get; set; }

        /// <summary>
        /// Name of the person who marked the loan as paid off
        /// </summary>
        public string? PaidOffBy { get; set; }

        /// <summary>
        /// Date when the loan was marked as paid off
        /// </summary>
        public DateTime? PaidOffDate { get; set; }

        /// <summary>
        /// Abstract property that must be implemented by derived classes.
        /// Demonstrates polymorphism - each loan type returns its specific type.
        /// </summary>
        public abstract string LoanType { get; }

        /// <summary>
        /// Virtual method that can be overridden by derived classes.
        /// Implements the core business logic for marking a loan as paid off.
        /// Follows the Template Method pattern.
        /// </summary>
        /// <param name="paidOffBy">Name of the person paying off the loan</param>
        public virtual void MarkAsPaidOff(string paidOffBy)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(paidOffBy))
                throw new ArgumentException("PaidOffBy name cannot be empty", nameof(paidOffBy));

            // Update loan status
            IsPaidOff = true;
            PaidOffBy = paidOffBy.Trim();
            PaidOffDate = DateTime.Now;
            PayoffAmount = 0; // Set balance to zero when paid off
        }
    }
}