﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.BookUsers;
using ReadRealmBackend.BL.Notes;
using ReadRealmBackend.Models.Requests.BookAuthors;
using ReadRealmBackend.Models.Requests.BookUsers;
using ReadRealmBackend.Models.Requests.Notes;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/bookTracking")]
    [Authorize]
    public class BookTrackingController : Controller
    {
        private readonly IBookUserBL _bookUserBL;
        private readonly INoteBL _noteBL;
        private readonly IMapper _mapper;

        public BookTrackingController(IBookUserBL bookUserBL, INoteBL noteBL, IMapper mapper)
        {
            _bookUserBL = bookUserBL;
            _noteBL = noteBL;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> InsertBookUserAsync(InsertBookUserRequest req)
        {
            var mappedReq = _mapper.Map<InsertBookUserFullRequest>(req);
            mappedReq.UserId = HttpContext.Items["userId"] as string;
            return Ok(await _bookUserBL.InsertBookUserAsync(mappedReq));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookUserAsync(InsertBookUserRequest req)
        {
            var mappedReq = _mapper.Map<InsertBookUserFullRequest>(req);
            mappedReq.UserId = HttpContext.Items["userId"] as string;
            return Ok(await _bookUserBL.UpdateBookUserAsync(mappedReq));
        }

        [HttpPost("note")]
        public async Task<IActionResult> InsertNoteAsync(InsertNoteRequest req)
        {
            var mappedReq = _mapper.Map<InsertNoteFullRequest>(req);
            mappedReq.UserId = HttpContext.Items["userId"] as string;
            return Ok(await _noteBL.InsertNoteAsync(mappedReq));
        }
    }
}