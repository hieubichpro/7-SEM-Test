namespace lab_03.BL.Models
{
    public class Club
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public Club(string name, int id = 1)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}