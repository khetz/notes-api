using NotesAPI.Requests;
using ApplicationRegisterUserRequest = Application.Inputs.RegisterUserRequest;

namespace NotesAPI.Mappers;

internal static class UserMappers
{
    internal static ApplicationRegisterUserRequest ToApplicationRegisterUserRequest(this RegisterUserRequest registerUserRequest) => new()
    { 
        Username = registerUserRequest.Username,
        Password = registerUserRequest.Password
    };
}
