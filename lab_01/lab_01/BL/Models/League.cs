namespace lab_03.BL.Models
{
    public class League
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public int IdUser { get; set; }

        public League(string name, int idUser, int id = 1)
        {
            this.Id = id;
            this.Name = name;
            this.IdUser = idUser;
        }
    }
}