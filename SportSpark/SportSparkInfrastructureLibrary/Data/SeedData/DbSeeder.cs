using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Enums;
using SportSparkInfrastructureLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSparkInfrastructureLibrary.Data.SeedData
{
    public static class DbSeeder
    {
        public static readonly List<User> Users = new()
        {
            new User
            {
                UserName = "ffrankson",
                Password = HashHelper.Hash("frank@gmail.com", "Password1"),
                FirstName = "Frank",
                LastName = "Frankson",
                Email = "frank@gmail.com",
                Bio = "Hey! I'm Frank.",
                Rating = (decimal?)3.6,
                Age = 36
            },
            new User
            {
                UserName = "aalicia",
                Password = HashHelper.Hash("alice@gmail.com", "Password2"),
                FirstName = "Alice",
                LastName = "Alicia",
                Email = "alice@gmail.com",
                Bio = "Hey! I'm Alice.",
                Rating = (decimal?)4.6,
                Age = 28
            },
            new User
            {
                UserName = "rgreen",
                Password = HashHelper.Hash("rachel@gmail.com", "Password3"),
                FirstName = "Rachel",
                LastName = "Green",
                Email = "rachel@gmail.com",
                Age = 25
            },
            new User
            {
                UserName = "mgeller",
                Password = HashHelper.Hash("monica@gmail.com", "Password4"),
                FirstName = "Monica",
                LastName = "Geller",
                Email = "monica@gmail.com",
                Bio = "Hey! I'm Monica.",
                Age = 26
            },
            new User
            {
                UserName = "rgeller",
                Password = HashHelper.Hash("ross@gmail.com", "Password5"),
                FirstName = "Ross",
                LastName = "Geller",
                Email = "ross@gmail.com",
                Rating = (decimal?)4.9
            },
            new User
            {
                UserName = "jtribbiani",
                Password = HashHelper.Hash("joey@gmail.com", "Password6"),
                FirstName = "Joey",
                LastName = "Tribbiani",
                Email = "joey@gmail.com",
                Bio = "Hey! I'm Joey.",
                Rating = (decimal?)2.8,
                Age = 30
            },
        };

        public static readonly List<EventType> EventTypes = new()
        {
            new EventType
            {
                Name = "Football",
                Description = "Sport - football"
            },
            new EventType
            {
                Name = "Chess",
                Description = "Chess"
            },
            new EventType
            {
                Name = "Baseball",
                Description = "Sport - baseball"
            },
            new EventType
            {
                Name = "Other",
                Description = "Some other type of activity"
            },
        };

        public static readonly List<EventRepeatType> EventRepeatTypes = new()
        {
            new EventRepeatType
            {
                Description = "Daily"
            },
            new EventRepeatType
            {
                Description = "Every other day"
            },            
            new EventRepeatType
            {
                Description = "Once a week"
            },
            new EventRepeatType
            {
                Description = "Only once"
            },
        };

        public static readonly List<Event> Events = new()
        {
            new Event
            {
                Title = "Joey's football game",
                Description = "I need some people to join me and a friend so we can play a little bit of football this weekend",
                Lat = (decimal?)40.739102,
                Long = (decimal?)-74.004916,
                NumberOfParticipants = 4,
                Privacy = Privacy.Public,
                Active = true,
                UserId = 6,
                EventTypeId = 1,
                RepeatTypeId = 2
            },
            new Event
            {
                Title = "Joey's pizza eating tournament",
                Description = "Only real foodies!!",
                Lat = (decimal?)40.734986,
                Long = (decimal?)-74.007359,
                Time = DateTime.ParseExact("01.04.2023 20:30", RegexHelper.DateTimeFormat, CultureInfo.InvariantCulture),
                Duration = 120,
                NumberOfParticipants = 3,
                Privacy = Privacy.Friends,
                Active = true,
                UserId = 6,
                EventTypeId = 4,
                RepeatTypeId = 4
            },
            new Event
            {
                Title = "Paleontology quiz",
                Description = "Title self explanatory. PHDs only, please.",
                Lat = (decimal?)40.713598,
                Long = (decimal?)-73.987592,
                NumberOfParticipants = 5,
                Privacy = Privacy.Public,
                Active = false,
                UserId = 5,
                EventTypeId = 4,
                RepeatTypeId = 3
            },
        };

        public static readonly List<Friendship> Friendships = new()
        {
            new Friendship
            {
                UserId = 1,
                User2Id = 2,
                Status = FriendshipStatus.Confirmed
            },
            new Friendship
            {
                UserId = 3,
                User2Id = 4,
                Status = FriendshipStatus.Confirmed
            },
            new Friendship
            {
                UserId = 4,
                User2Id = 5,
                Status = FriendshipStatus.Confirmed
            },
            new Friendship
            {
                UserId = 5,
                User2Id = 6,
                Status = FriendshipStatus.Requested
            },
        };
    }
}
