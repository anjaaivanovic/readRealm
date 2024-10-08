﻿using AutoMapper;
using ReadRealmBackend.DAL.Books;
using ReadRealmBackend.DAL.BookUsers;
using ReadRealmBackend.DAL.Statuses;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Requests.BookUsers;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.BookUsers
{
    public class BookUserBL: IBookUserBL
    {
        private readonly IBookUserDAL _bookUserDAL;
        private readonly IBookDAL _bookDAL;
        private readonly IStatusDAL _statusDAL;
        private readonly IMapper _mapper;

        public BookUserBL(IBookUserDAL bookUserDAL, IBookDAL bookDAL, IStatusDAL statusDAL, IMapper mapper)
        {
            _bookUserDAL = bookUserDAL;
            _bookDAL = bookDAL;
            _statusDAL = statusDAL;
            _mapper = mapper;
        }

        private async Task<string?> ValidateBookRequest(InsertBookUserFullRequest req) 
        {
            var book = await _bookDAL.GetOneAsync(req.BookId);

            if (book == null)
            {
                return  "No book with such id!";
            }

            if (book.ChapterCount < req.CurrentChapter)
            {
                return "Invalid chapter value!";
            }

            if (!await _statusDAL.CheckStatusAsync(req.StatusId))
            {
                return "No status with such id!";
            }

            return null;
        }

        public async Task<GenericResponse<GenericPaginationResponse<UsersBookResponse>>> GetUsersBooksAsync(UsersBookPaginationRequest req, string userId)
        {
            if (!await _statusDAL.CheckStatusAsync(req.StatusId))
            {
                return new GenericResponse<GenericPaginationResponse<UsersBookResponse>>
                {
                    Success = false,
                    Errors = new List<string> { "No status with such id!" }
                };
            }

            return new GenericResponse<GenericPaginationResponse<UsersBookResponse>>
            {
                Success = true,
                Data = _mapper.Map<GenericPaginationResponse<UsersBookResponse>>(await _bookDAL.GetUsersBooksAsync(req, userId))
            };
        }

        public async Task<GenericResponse<string>> InsertBookUserAsync(InsertBookUserFullRequest req)
        {
            #region Validation

            var error = await ValidateBookRequest(req);

            if (error != null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { error }
                };
            }

            if (await _bookUserDAL.CheckBookUserAsync(req.BookId, req.UserId))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Book already tracked! Use the update method." }
                };
            }

            #endregion

            await _bookUserDAL.InsertOneAsync(_mapper.Map<InsertBookUserFullRequest, BookUser>(req));
            var success = await _bookUserDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully tracked progress!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }

        public async Task<GenericResponse<string>> UpdateBookUserAsync(InsertBookUserFullRequest req)
        {
            #region Validation

            var error = await ValidateBookRequest(req);

            if (error != null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { error }
                };
            }

            #endregion

            _bookUserDAL.UpdateOne(_mapper.Map<InsertBookUserFullRequest, BookUser>(req));

            var success = await _bookUserDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully updated progress!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }
    }
}