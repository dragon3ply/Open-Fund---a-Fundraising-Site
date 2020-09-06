using CrowdFundT2.Core.Data;
using CrowdFundT2.Core.Model;
using CrowdFundT2.Core.Services.Interfaces;
using CrowdFundT2.Core.Services.Options;
using System;
using System.Linq;

namespace CrowdFundT2.Core.Services
{
    public class ClientService : IClientService
    {
        private CrowdFundT2DbContext context_;

        public ClientService(CrowdFundT2DbContext context)
        {
            context_ = context;
        }

        public ApiResult<Client> CreateClient(CreateClientOptions options)
        {

            if (options == null)
            {
                return ApiResult<Client>.Failed(StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Firstname))
            {
                return ApiResult<Client>.Failed(StatusCode.BadRequest, "Firstname is empty");
            }
            
            if (string.IsNullOrWhiteSpace(options.Lastname))
            {
                return ApiResult<Client>.Failed(StatusCode.BadRequest, "Lastname is empty");
            }
            
            if (string.IsNullOrWhiteSpace(options.Email))
            {
                return ApiResult<Client>.Failed(StatusCode.BadRequest, "Email is empty");
            }

            if (!Client.IsValidEmail(options.Email))
            {
                return ApiResult<Client>.Failed(StatusCode.BadRequest, "Email not valid");
            }
 
            if (string.IsNullOrWhiteSpace(options.Phone))
            {
                return ApiResult<Client>.Failed(StatusCode.BadRequest, "Phone is empty");
            }



            var client = new Client()
            {
                Firstname = options.Firstname,
                Lastname = options.Lastname,
                Email = options.Email,
                Phone = options.Phone,
                IsActive = true
                
            };

            context_.Add(client);

            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<Client>.Failed(
                        StatusCode.InternalServerError, "Client could not be created");
                }
            }
            catch (Exception ex)
            {
                return ApiResult<Client>.Failed(StatusCode.InternalServerError, ex.ToString());
            }

            return ApiResult<Client>.Successful(client);
        }

        public ApiResult<Client> GetClientById(int? id)
        {
            if (id == null)
            {
                return ApiResult<Client>.Failed(StatusCode.BadRequest, "Id is empty");
            }
            var options = new SearchClientOptions()
            {
                ClientId = id.Value
            };

            return ApiResult<Client>.Successful(SearchClient(options).Data.SingleOrDefault());
        }

        public ApiResult<IQueryable<Client>> SearchClient(SearchClientOptions options)
        {
            if (options == null)
            {
                return ApiResult<IQueryable<Client>>.Failed(StatusCode.BadRequest, "Null options");
            }

            var query = context_
                .Set<Client>()
                .AsQueryable();

            if (options.ClientId != null)
            {
                query = query.Where(c => c.ClientId == options.ClientId);
            }

            if (!string.IsNullOrWhiteSpace(options.Firstname))
            {
                query = query.Where(c => c.Firstname == options.Firstname);
            }

            if (!string.IsNullOrWhiteSpace(options.Lastname))
            {
                query = query.Where(c => c.Lastname == options.Lastname);
            }

            if (!string.IsNullOrWhiteSpace(options.Email))
            {
                query = query.Where(c => c.Email == options.Email);
            }

            if (!string.IsNullOrWhiteSpace(options.Phone))
            {
                query = query.Where(c => c.Phone == options.Phone);
            }

            if (options.CreatedFrom != null)
            {
                query = query.Where(c => c.Created >= options.CreatedFrom);
            }

            if (options.CreatedTo != null)
            {
                query = query.Where(c => c.Created < options.CreatedTo);
            }

            return ApiResult<IQueryable<Client>>.Successful(query.Take(500));
        } 

        public ApiResult<bool> UpdateClient(int? id, UpdateClientOptions options)
        {
            if (options == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "Null options");
            }

            if (id == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "Id is empty");
            }

            if (!Client.IsValidEmail(options.Email))
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "Email is not valid");
            }


            var updateClient = context_
                .Set<Client>()
                .Where(c => c.ClientId == id)
                .SingleOrDefault();

            if (updateClient == null)
            {
                return ApiResult<bool>.Failed(
                    StatusCode.NotFound, $"Could not retrieve Client with this {id}!");
            }

            if (options.Firstname != null)
            {
                updateClient.Firstname = options.Firstname;
            }

            if (options.Lastname != null)
            {
                updateClient.Lastname = options.Lastname;
            }

            if (options.Email != null)
            {
                updateClient.Email = options.Email;
            }

            if (options.Phone != null)
            {
                updateClient.Phone = options.Phone;
            }
            
            if (options.IsActive != null)
            {
                updateClient.IsActive = options.IsActive.Value;
            }


            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<bool>.Failed(
                        StatusCode.InternalServerError, "Client could not be updated");
                }
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Failed(StatusCode.InternalServerError, ex.ToString());
            }

            return ApiResult<bool>.Successful(true);
        }

        public ApiResult<bool> DeleteClient(int? id)
        {
            if (id == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "Client Id is empty");
            }

            var query = context_
                .Set<Client>()
                .AsQueryable();

            var client = query
                .Where(c => c.ClientId == id)
                .SingleOrDefault();

                context_.Remove(client);

            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<bool>.Failed(
                        StatusCode.InternalServerError, "Client could not be deleted");
                }
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Failed(StatusCode.InternalServerError, ex.ToString());
            }

            return ApiResult<bool>.Successful(true);
        }

    }

}
