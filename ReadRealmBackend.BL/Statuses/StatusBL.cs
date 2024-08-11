using AutoMapper;
using ReadRealmBackend.DAL.Statuses;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Statuses;
using ReadRealmBackend.Models.Responses.Statuses;

namespace ReadRealmBackend.BL.Statuses
{
    public class StatusBL: IStatusBL
    {
        private readonly IStatusDAL _statusDAL;
        private readonly IMapper _mapper;

        public StatusBL(IStatusDAL statusDAL, IMapper mapper)
        {
            _statusDAL = statusDAL;
            _mapper = mapper;
        }

        #region Get

        public async Task<GenericResponse<StatusResponse>> GetStatusAsync(int id)
        {
            var status = await _statusDAL.GetOneAsync(id);

            if (status == null)
            {
                return new GenericResponse<StatusResponse>
                {
                    Success = false,
                    Errors = new List<string> { "No status with such id!" }
                };
            }

            return new GenericResponse<StatusResponse>
            {
                Success = true,
                Data = _mapper.Map<Status, StatusResponse>(status)
            };
        }

        public async Task<GenericResponse<List<StatusResponse>>> GetStatusesAsync()
        {
            var statuses = await _statusDAL.GetAllAsync();

            return new GenericResponse<List<StatusResponse>>
            {
                Success = true,
                Data = _mapper.Map<List<Status>, List<StatusResponse>>(statuses)
            };
        }

        #endregion

        #region Insert

        public async Task<GenericResponse<string>> InsertStatusAsync(InsertStatusRequest req)
        {
            if (await _statusDAL.CheckStatusByNameAsync(req.Name))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Status already exists!" }
                };
            }

            await _statusDAL.InsertOneAsync(_mapper.Map<InsertStatusRequest, Status>(req));
            var success = await _statusDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted status!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }

        #endregion

        #region Update

        public async Task<GenericResponse<string>> UpdateStatusAsync(UpdateStatusRequest req)
        {
            if (!await _statusDAL.CheckStatusAsync(req.Id))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No status with such id!" }
                };
            }

            _statusDAL.UpdateOne(_mapper.Map<UpdateStatusRequest, Status>(req));
            var success = await _statusDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully updated status!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }

        #endregion

        #region Delete

        public async Task<GenericResponse<string>> DeleteStatusAsync(int id)
        {
            var toDelete = await _statusDAL.GetOneAsync(id);

            if (toDelete == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No status with such id!" }
                };
            }

            _statusDAL.DeleteOne(toDelete);
            var success = await _statusDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully deleted status!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }

        #endregion
    }
}