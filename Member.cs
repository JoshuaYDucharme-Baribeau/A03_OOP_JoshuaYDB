
namespace A03_OOP_JoshuaYDB
{
    internal class Member
    {

        //declaring the properties, declaring as nullable but they will be filled by the constructor
        //definitions below
        private Guid member_id;
        private string? member_first_name;
        private string? member_last_name;
        private string? member_email;
        private DateOnly member_dob;

        //constructor with no params, will consider making it return an error message
        public Member()
        {

        }

        internal Member(Guid an_id, string a_first_name, string a_last_name, string an_email, DateOnly a_dob)
        {
            member_id = an_id;
            member_first_name = a_first_name;
            member_last_name = a_last_name;
            member_email = an_email;
            member_dob = a_dob;
        }



    }
}
