using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundT2.Core
{
    public enum StatusCode
    {
        OK = 200,
        NotFound = 404,
        BadRequest = 400,
        InternalServerError = 500,
        Forbidden = 403,
        Conflict = 409,
        Created = 201,
        NoContent = 204,
        Unauthorized = 401,
        NotModified = 304
    }
}
