namespace Grupp_upgift_Grupp4.Models.Entities
{
    public class Bud
    {
        public int BudId { get; set; }
        public decimal BudSumma { get; set; }

        public Bud(int budId, decimal budSumma)
        {
            BudId = budId;
            BudSumma = budSumma;
        }
    }
}
