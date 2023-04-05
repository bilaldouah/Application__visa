namespace Application_visa.Models
{
    public class dateRepository
    {
        public int getAllyears { get; set; }
       public string monthName { get; set; }
       public static List<dateRepository> getMonthNames(int i, List<dateRepository> monthNames)
        {
            DateTime dateObj = new DateTime(2000, i, 1);
            string monthName = dateObj.ToString("MMMM");
            dateRepository date = new dateRepository();
            date.monthName = monthName;
            monthNames.Add(date);
            return monthNames;
        }

    }
}
