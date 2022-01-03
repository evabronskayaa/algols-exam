using System;

namespace HashTables
{
    public class UserData
    {
        public string Id { get; set; }
        public string RegDate { get; private set; }

        public static UserData RandomInstance() =>
            new UserData
            {
                Id = Generator.GenRndCharSeq(new Random().Next(3, 11)),
                RegDate = Generator.GenRndDate()
            };
    }
}