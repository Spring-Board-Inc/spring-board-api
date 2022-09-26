﻿using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface ICertificationService
    {
        Task<ApiBaseResponse> Create(Guid userInfoId, CertificationRequest request);
        Task<ApiBaseResponse> Update(Guid id, CertificationRequest request);
        Task<ApiBaseResponse> Delete(Guid id);
    }
}
