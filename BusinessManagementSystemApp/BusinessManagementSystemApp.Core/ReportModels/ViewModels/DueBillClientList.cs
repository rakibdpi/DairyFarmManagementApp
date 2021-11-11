namespace BMSA.App.ViewModels
{
    public class DueBillClientList
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public decimal? BillAmount { get; set; }   
    }
}