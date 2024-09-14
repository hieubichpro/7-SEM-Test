namespace lab_03.BL.Models
{
    public class ClubLeague
    {
        public int Id {  get; set; }
        public int IdClub { get; set; }
        public int IdLeague { get; set; }


        public ClubLeague(int idClub, int idLeague, int id = 1)
        {
            this.Id = id;
            this.IdClub = idClub;
            this.IdLeague = idLeague;
        }
    }
}
