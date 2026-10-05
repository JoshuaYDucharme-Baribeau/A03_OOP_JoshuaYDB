
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
                        //how a member will be instantiated in a list
                        //the next thing that should happen is a GUID is generated,
                        //and the user is asked to fill in other details about the recently added member
                        //those details are then passed as the arguments for Member() in the line below
                        //members.Add(new Member()); //instantiate a new member with the passed details in the list
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
    }
}
