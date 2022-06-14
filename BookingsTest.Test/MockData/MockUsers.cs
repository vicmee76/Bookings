using Bogus;
using Bookings.Models.DB;
using System.Collections.Generic;

namespace BookingsTest.Test.MockData
{
    public class MockUsers
    {
        public List<User> getUsers(int generate)
        {
            var users = new Faker<User>()
                .RuleFor(x => x.UsersId, x => x.Random.Int())
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.Phone, x => x.Person.Phone)
                .RuleFor(x => x.Address, x => x.Address.StreetAddress());

            return users.Generate(generate);
        }
    }
}
