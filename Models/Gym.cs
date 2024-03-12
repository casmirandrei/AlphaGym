namespace AlphaGym.Models
{
    public class Gym
    {
        public int GymId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Member>? Members { get; set; }
    }
}
