using Bogus;
using FitQuest.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength = 10)
        {
            // Bogus irá criar um objeto do tipo de acordo com as regras, igual ao Fluent Validator
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
                
        }
    }
}
