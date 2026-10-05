
namespace A03_OOP_JoshuaYDB
{
    internal class Run
    {

        public int RunProgram()
        {

            Boolean ready = true; //flag for the menu loop, will terminate the loop when False
            ConsoleKeyInfo user_input; //using the ConsoleKeyInfo object/struct to store the key stroke for menu input


            //create a collection to store the list of members
            List<Member> members = new List<Member>();


            //loop for main program/menu
            while (ready)
            {
                UserInterface.Display_menu(); //menu display

                user_input = Console.ReadKey();
                switch (user_input.Key)
                {
                    //add a member
                    case ConsoleKey.A:
                        
                        members.Add(new Member());
                        //get the size of the list to find the last member
                        int ix_last_member = (members.Count)-1;
                        members[ix_last_member].Member_id = Create_ID();
                        members[ix_last_member].Member_first_name = Get_First();
                        members[ix_last_member].Member_last_name = Get_Last();
                        members[ix_last_member].Member_email = Get_Email();
                        members[ix_last_member].Member_dob = Get_DOB();

                        UserInterface.Clear_screen();
                        UserInterface.Display_message($"{members[ix_last_member].Member_first_name} {members[ix_last_member].Member_last_name} was successfully added to the club!");
                        UserInterface.Block_program("Be sure to welcome them!\nPress any key to return to the menu...");

                        break;

                    //display data
                    case ConsoleKey.L:
                        //method to display all members will run a loop through the list and
                        //call Display_one_member() each time iteration
                                //UserInterface.Display_one_member();
                        break;

                    //remove a member
                    case ConsoleKey.D:

                        //method call to remove a member
                            //asks for a member ID,
                            //checkks it exists,
                                //  asks for confirmation befor deletionn
                                //if not exist error message return to menu

                        break;

                    case ConsoleKey.S:
                        //method to save members to file
                        break;

                    case ConsoleKey.O:
                        //method to load members from file
                        break;

                    case ConsoleKey.X:

                        UserInterface.Clear_screen();
                        UserInterface.Display_message("Are you sure you want to exit (Y/N)?");
                        user_input = Console.ReadKey();

                        if (user_input.Key == ConsoleKey.Y)
                        {
                            ready = false; //change the flag and exit the loop
                            UserInterface.Clear_screen();
                        }
                        // i.e. if input is not Y or y
                        else
                        {
                            //do nothing to continue looping to the menu
                        }
                        break;

                    default:
                        //no menu option selected, display an error
                        UserInterface.Display_error_message
                            (
                                "Invalid selection. Choose an operation based on the characters in brackets <>.",
                                "Press any key to return to the menu..."
                            );
                        break;

                }

            }


            //add member method will simply instantiate a member and add the member to the list
            //I can set default params in the mean time while 

            //method to display all members will run a loop through the list and
            //call Display_one_member() each time iteration


            return 0;
        }


        //____Methods that gather input for a new member____

        //create a Guid
        internal Guid Create_ID()
        {
            Guid unique_id = new Guid();
            return unique_id;
        }

        //gather member first name
        internal string Get_First()
        {
            Boolean invalid_name = true;
            string first_name = "x";
            string? input = null;
            
            while(invalid_name)
            {
                UserInterface.Clear_screen();
                UserInterface.Display_message
                    (
                        "**Adding new member (Step 1/4)**\n" +
                        "Enter the first name of the new member:\n"
                    );
                input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input))
                {
                    UserInterface.Display_error_message
                        (
                            "The first name may not be blank",
                            "Press any key to try again..."
                        );
                }
                else if(!String.IsNullOrWhiteSpace(input))
                {
                    first_name = input;
                    invalid_name = false;
                }
            }
            return first_name;
            
        }



        //gather member last name
        internal string Get_Last()
        {
            Boolean invalid_name = true;
            string last_name = "x";
            string? input = null;

            while (invalid_name)
            {
                UserInterface.Clear_screen();
                UserInterface.Display_message
                    (
                        "**Adding new member (Step 2/4)**\n" +
                        "Enter the last name of the new member:\n"
                    );
                input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input))
                {
                    UserInterface.Display_error_message
                        (
                            "The last name may not be blank",
                            "Press any key to try again..."
                        );
                }
                else if(!String.IsNullOrWhiteSpace(input))
                {
                    last_name = input;
                    invalid_name = false;
                }
            }
            return last_name;

        }


        //gather member last name
        internal string Get_Email()
        {
            Boolean invalid_email = true;
            string? input;
            string email = "x";

            while (invalid_email)
            {
                UserInterface.Clear_screen();
                UserInterface.Display_message
                    (
                        "**Adding new member (Step 3/4)**\n" +
                        "Enter the email address of the new member:\n"
                    );
                input = Console.ReadLine();
                //ideally, this would have its own email address validation, and more complex systems would require validation with a link
                if (String.IsNullOrWhiteSpace(input))
                {
                    UserInterface.Display_error_message
                        (
                            "The email may not be blank",
                            "Press any key to try again..."
                        );
                }
                else if (!String.IsNullOrWhiteSpace(input))
                {
                    email = input;
                    invalid_email = false;
                }
            }
            return email;

        }


        //get the DOB of the new member
        internal DateOnly Get_DOB()
        {
            Boolean invalid_dob = true;
            string? input;
            DateOnly dob = new DateOnly(1900, 01, 01);

            while (invalid_dob)
            {
                UserInterface.Clear_screen();
                UserInterface.Display_message
                    (
                        "**Adding new member (Step 4/4)**\n" +
                        "Enter the Date of birth of the new member YYYY-MM-DD:\n"
                    );
                input = Console.ReadLine();
                //if the date is not parsable
                if (!DateOnly.TryParse(input, out dob))
                {
                    UserInterface.Display_error_message
                        (
                            "The date of birth entered was invalid.\nIt may not be blank and should be formated as YYYY-MM-DD.",
                            "Press any key to try again..."
                        );
                }
                //if parse successful
                else if (DateOnly.TryParse(input, out dob))
                {
                    invalid_dob = false;
                }
            }
            return dob;
        }





    }
}
