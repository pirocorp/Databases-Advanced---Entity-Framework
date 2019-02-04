namespace PhotoShare.Models
{
    using System.Collections.Generic;

    public class Town
    {
        private ICollection<User> usersBornInTown;
        private ICollection<User> usersCurrentlyLivingInTown;

        public Town()
        {
            this.usersBornInTown = new HashSet<User>();
            this.usersCurrentlyLivingInTown = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public virtual ICollection<User> UsersBornInTown
        {
            get => this.usersBornInTown;
            set => this.usersBornInTown = value;
        }

        public virtual ICollection<User> UsersCurrentlyLivingInTown
        {
            get => this.usersCurrentlyLivingInTown;
            set => this.usersCurrentlyLivingInTown = value;
        }
    }
}
