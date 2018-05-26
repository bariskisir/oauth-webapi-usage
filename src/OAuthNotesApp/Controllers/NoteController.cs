using OAuthNotesApp.Models.Db;
using OAuthNotesApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace OAuthNotesApp.Controllers
{
    public class NoteController : ApiController
    {
        [Route("api/Register")]
        [HttpPost]
        [AllowAnonymous]
        public bool Register(User user)
        {
            using (var db = new NotesModel())
            {
                var result = UserUtil.Register(user.Username, user.Password);
                return result;
            }
        }

        [Route("api/AddNote")]
        [HttpPost]
        [Authorize]
        public bool AddNote([FromBody]string pNote)
        {
            using (var db = new NotesModel())
            {
                // Get user information according to token
                var username = User?.Identity?.Name;
                //

                if (!string.IsNullOrWhiteSpace(username))
                {
                    var note = new Note();
                    note.NoteText = pNote;
                    note.Username = username;
                    db.Note.Add(note);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        [Route("api/GetNotes")]
        [HttpGet]
        [Authorize]
        public List<Note> GetNotes()
        {
            using (var db = new NotesModel())
            {
                var username = User?.Identity?.Name;
                if (!string.IsNullOrWhiteSpace(username))
                {
                    var noteList = db.Note.Where(x => x.Username == username).ToList();
                    return noteList;
                }
            }
            return null;
        }

        [Route("api/GetAllNotes")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public List<Note> GetAllNotes()
        {
            using (var db = new NotesModel())
            {
                var noteList = db.Note.ToList();
                return noteList;
            }
        }

    }
}
