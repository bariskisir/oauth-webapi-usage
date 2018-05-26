namespace OAuthNotesApp.Models.Db
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NotesModel : DbContext
    {
        public NotesModel()
            : base("name=NotesEntities")
        {
        }

        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
