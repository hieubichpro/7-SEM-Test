namespace lab_03.BL.Models
{
    public class Match
    {
        public int Id {  get; set; }
        public int GoalHomeTeam {  get; set; }
        public int GoalGuestTeam { get; set; }
        public int IdLeague {  get; set; }
        public int IdHomeTeam { get; set; }
        public int IdGuestTeam { get; set; }
        public Match(int idLeague, int idHomeTeam, int idGuestTeam, int goalHomeTeam = -1, int goalGuestTeam = -1, int id = 1)
        {
            this.Id = id;
            this.GoalHomeTeam = goalHomeTeam;
            this.GoalGuestTeam = goalGuestTeam;
            this.IdLeague = idLeague;
            this.IdHomeTeam = idHomeTeam;
            this.IdGuestTeam = idGuestTeam;
        }
    }
}