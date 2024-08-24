using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.Common.Constants;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Notes;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.DAL.Notes
{
    public class NoteDAL : BaseDAL<Note>, INoteDAL
    {

        public NoteDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<GenericPaginationResponse<Note>> GetBookNotesAsync(BookNotePaginationRequest req, string userId)
        {
            var query = _set.Where(note => note.BookId == req.BookId).AsQueryable();
            var visibility = await _context.NoteVisibilities.FirstOrDefaultAsync(v => v.Id == req.Visibility);
            
            if (visibility.Name == StringConstants.PrivateVisibility)
            {
                query = query.Where(note => note.UserId == userId
                    && note.NoteVisibilityId == visibility.Id);
            }
            else if (visibility.Name == StringConstants.SharedVisibility)
            {
                var friends = await _context.Friends
                    .Where(f => f.FirstUserId == userId || f.SecondUserId == userId)
                    .Select(f => f.FirstUserId == userId ? f.SecondUserId : f.FirstUserId)
                    .Distinct()
                    .ToListAsync();

                var currentChapter = (await _context.BookUsers
                    .FirstOrDefaultAsync(
                        bu => bu.UserId == userId 
                        && bu.BookId == req.BookId)
                    ).CurrentChapter;

                query = query.Where(n => friends.Contains(n.UserId) 
                    && n.Chapter <= currentChapter
                    && n.NoteVisibilityId == visibility.Id);
            }
            else
            {
                query = query.Where(note => note.UserId != userId
                    && note.NoteVisibilityId == visibility.Id);
            }

            return new GenericPaginationResponse<Note>
            {
                Items = await query
                    .Skip((req.Page - 1) * req.ItemCount)
                    .Take(req.ItemCount)
                    .ToListAsync(),
                TotalItemCount = await query.CountAsync(),
            };
        }
    }
}