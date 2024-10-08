﻿using ReadRealmBackend.Models.Responses.Statuses;
using ReadRealmBackend.Models.Requests.Statuses;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.Statuses
{
    public interface IStatusBL
    {
        Task<GenericResponse<StatusResponse>> GetStatusAsync(int id);
        Task<GenericResponse<List<StatusResponse>>> GetStatusesAsync();
        Task<GenericResponse<string>> InsertStatusAsync(InsertStatusRequest req);
        Task<GenericResponse<string>> UpdateStatusAsync(UpdateStatusRequest req);
        Task<GenericResponse<string>> DeleteStatusAsync(int id);
    }
}